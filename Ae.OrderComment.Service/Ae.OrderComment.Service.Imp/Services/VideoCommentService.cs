using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using ApolloErp.Log;
using ApolloErp.Redis;
using ApolloErp.Web.WebApi;
using Ae.OrderComment.Service.Client.Interface;
using Ae.OrderComment.Service.Client.Model;
using Ae.OrderComment.Service.Common.Exceptions;
using Ae.OrderComment.Service.Core.Interfaces;
using Ae.OrderComment.Service.Core.Model.VideoComment;
using Ae.OrderComment.Service.Core.Request.VideoComment;
using Ae.OrderComment.Service.Dal.Model;
using Ae.OrderComment.Service.Dal.Model.Condition;
using Ae.OrderComment.Service.Dal.Model.Extend;
using Ae.OrderComment.Service.Dal.Repositorys.VideoComment;
using StackExchange.Redis;

namespace Ae.OrderComment.Service.Imp.Services
{
    public class VideoCommentService : IVideoCommentService
    {
        private readonly IVideoCommentRepository _videoCommentRepository;
        private readonly IVideoCommentFavourRepository _videoCommentFavourRepository;
        private readonly IMapper _mapper;
        private readonly ApolloErpLogger<VideoCommentService> _logger;
        private readonly IUserClient _userClient;
        private readonly IConfiguration _configuration;
        private readonly RedisClient _redisClient;
        //static RedisValue _token = Environment.MachineName;

        public VideoCommentService(IVideoCommentRepository videoCommentRepository,
            IVideoCommentFavourRepository videoCommentFavourRepository, IMapper mapper,
            ApolloErpLogger<VideoCommentService> logger, IUserClient userClient, IConfiguration configuration,
            RedisClient redisClient)
        {
            _videoCommentRepository = videoCommentRepository;
            _videoCommentFavourRepository = videoCommentFavourRepository;
            _mapper = mapper;
            _logger = logger;
            _userClient = userClient;
            _configuration = configuration;
            _redisClient = redisClient;
        }

        /// <summary>
        /// 获取视频的评论列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiPagedResultData<VideoCommentListVo>> GetVideoCommentPageList(
            VideoCommentPageListRequest request)
        {
            ApiPagedResultData<VideoCommentListVo> result = new ApiPagedResultData<VideoCommentListVo>()
            {
                Items = new List<VideoCommentListVo>()
            };

            var comment = await _videoCommentRepository.GetVideoCommentPageList(new VideoCommentPageListCondition()
            {
                VideoId = request.VideoId,
                VideoType = request.VideoType,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize
            });

            if (comment != null)
            {
                result.TotalItems = comment.TotalItems;
                if (comment.Items != null && comment.Items.Any())
                {
                    var userIdList = comment.Items.Select(_ => _.UserId.ToString()).Distinct().ToList();
                    List<long> commentIds = comment.Items.Select(_ => _.Id).ToList();
                    var commentFavourTask =
                        _videoCommentFavourRepository.GetVideoCommentFavourByCommentIds(commentIds);
                    var userTask = _userClient.GetUsersByUserIds(userIdList);
                    var replyCountTask = _videoCommentRepository.GetReplyCount(commentIds);
                    await Task.WhenAll(commentFavourTask, userTask, replyCountTask);
                    var commentFavour = commentFavourTask.Result ?? new List<VideoCommentFavourDO>();
                    var user = userTask.Result ?? new List<UserBaseInfoDto>();
                    var replyCount = replyCountTask.Result ?? new List<ReplyCountDo>();
                    foreach (VideoCommentDO _ in comment.Items)
                    {
                        List<VideoCommentFavourDO> defaultF = commentFavour.Where(t => t.CommentId == _.Id).ToList();
                        UserBaseInfoDto defaultU = user.FirstOrDefault(t => t.UserId == _.UserId.ToString());
                        VideoCommentListVo itemV = new VideoCommentListVo()
                        {
                            CommentId = _.Id,
                            VideoId = _.VideoId,
                            Content = _.Content,
                            UserId = _.UserId.ToString(),
                            UserNickName = GetUserNickName(defaultU),
                            UserHeadUrl = defaultU?.HeadUrl ?? string.Empty,
                            LikeNum = defaultF.Count,
                            IsLike = IsLike(defaultF, request.UserId),
                            IsOwner = _.ShopId.Equals(defaultU?.RelationShopId),
                            CommentTime = _.CreateTime,
                            ReplyCount = replyCount.FirstOrDefault(t => t.ParentId == _.Id)?.Count ?? 0
                        };
                        result.Items.Add(itemV);
                    }
                }
            }

            return result;
        }

        private string GetUserNickName(UserBaseInfoDto user)
        {
            if (user == null)
            {
                return string.Empty;
            }

            if (!string.IsNullOrEmpty(user.NickName))
            {
                return user.NickName;
            }

            if (!string.IsNullOrEmpty(user.UserName))
            {
                return user.UserName;
            }

            return user.UserTel;
        }

        private bool IsLike(List<VideoCommentFavourDO> favour, string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return false;
            }

            if (Guid.TryParse(userId, out Guid guid))
            {
                return favour.Exists(_ => _.UserId == guid);
            }

            return false;
        }

        /// <summary>
        /// 获取评论回复列表
        /// </summary>
        /// <returns></returns>
        public async Task<List<VideoCommentListVo>> GetChildVideoCommentList(ChildVideoCommentListRequest request)
        {
            List<VideoCommentListVo> result = new List<VideoCommentListVo>();

            var comment = await _videoCommentRepository.GetVideoCommentByParentId(request.CommentId);
            if (comment.Any())
            {
                var userIdList = comment.Select(_ => _.UserId.ToString()).Distinct().ToList();
                var commentFavourTask =
                    _videoCommentFavourRepository.GetVideoCommentFavourByCommentIds(comment.Select(_ => _.Id)
                        .ToList());
                var userTask = _userClient.GetUsersByUserIds(userIdList);
                await Task.WhenAll(commentFavourTask, userTask);
                var commentFavour = commentFavourTask.Result ?? new List<VideoCommentFavourDO>();
                var user = userTask.Result ?? new List<UserBaseInfoDto>();

                List<VideoCommentDO> first = comment.Where(_ => _.TargetId == request.CommentId || _.TargetId == 0)
                    .OrderByDescending(_ => _.Id).ToList();
                first.ForEach(_ =>
                {
                    result.AddRange(HandelReplyComment(_, comment, commentFavour, user, request.UserId,
                        request.CommentId));
                });
            }

            return result;
        }


        private List<VideoCommentListVo> HandelReplyComment(VideoCommentDO comment, List<VideoCommentDO> allComment,
            List<VideoCommentFavourDO> commentFavour, List<UserBaseInfoDto> user, string userId, long commentId)
        {
            List<VideoCommentListVo> result = new List<VideoCommentListVo>();
            List<VideoCommentFavourDO> defaultF = commentFavour.Where(t => t.CommentId == comment.Id).ToList();
            UserBaseInfoDto defaultU = user.FirstOrDefault(t => t.UserId == comment.UserId.ToString());
            VideoCommentListVo itemF = new VideoCommentListVo()
            {
                CommentId = comment.Id,
                VideoId = comment.VideoId,
                Content = comment.Content,
                UserId = comment.UserId.ToString(),
                UserNickName = GetUserNickName(defaultU),
                UserHeadUrl = defaultU?.HeadUrl ?? string.Empty,
                LikeNum = defaultF.Count,
                IsLike = IsLike(defaultF, userId),
                IsOwner = comment.ShopId.Equals(defaultU?.RelationShopId),
                CommentTime = comment.CreateTime
            };
            if (comment.TargetId != 0 && comment.TargetId != commentId)
            {
                VideoCommentDO target = allComment.FirstOrDefault(x => x.Id == comment.TargetId);
                if (target != null)
                {
                    var targetUser = user.FirstOrDefault(t => t.UserId == target.UserId.ToString());
                    itemF.TargetComment = new VideoCommentListVo()
                    {
                        CommentId = target.Id,
                        VideoId = target.VideoId,
                        Content = target.Content,
                        CommentTime = target.CreateTime,
                        UserId = target.UserId.ToString(),
                        UserNickName = GetUserNickName(targetUser),
                        UserHeadUrl = targetUser?.HeadUrl ?? string.Empty,
                    };
                }
            }

            result.Add(itemF);
            var child = allComment.Where(_ => _.TargetId == comment.Id).OrderByDescending(_ => _.Id).ToList();
            if (child.Any())
            {
                child.ForEach(_ =>
                {
                    result.AddRange(HandelReplyComment(_, allComment, commentFavour, user, userId, commentId));
                });
            }

            return result;
        }

        /// <summary>
        /// 是否自动审核
        /// </summary>
        /// <returns></returns>
        private bool IsAutoCheckComment()
        {
            bool autoCheck = false; //自动审核 
            string autoCheckStr = _configuration["SwitchConfig:VideoCommentAutoCheck"];
            if (!string.IsNullOrEmpty(autoCheckStr))
            {
                if (Int32.TryParse(autoCheckStr, out int autoCheckInt))
                {
                    if (autoCheckInt > 0)
                    {
                        autoCheck = true;
                    }
                }
            }

            return autoCheck;
        }

        /// <summary>
        /// 新增评论
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<long> AddVideoComment(AddVideoCommentRequest request)
        {

            bool autoCheck = IsAutoCheckComment(); //自动审核 
            var result = await _videoCommentRepository.InsertAsync<long>(new VideoCommentDO()
            {
                VideoId = request.VideoId,
                VideoType = request.VideoType,
                Content = request.Content,
                UserId = Guid.Parse(request.UserId),
                ShopId = request.ShopId,
                TerminalType = (sbyte)request.TerminalType,
                ParentId = request.ParentId,
                TargetId = request.TargetId,
                CreateBy = request.UserId,
                CreateTime = DateTime.Now
            });

            if (autoCheck)
            {
                await CheckVideoComment(result, 1, "自动审核", request.UserId);
            }

            return result;
        }

        /// <summary>
        /// 审核
        /// </summary>
        /// <param name="commentId"></param>
        /// <param name="checkStatus"></param>
        /// <param name="checkComment"></param>
        /// <param name="checkBy"></param>
        /// <returns></returns>
        public async Task<bool> CheckVideoComment(long commentId, sbyte checkStatus, string checkComment,
            string checkBy)
        {
            await _videoCommentRepository.UpdateAsync(new VideoCommentDO()
            {
                Id = commentId,
                CheckStatus = checkStatus,
                CheckComment = checkComment,
                CheckBy = checkBy,
                CheckTime = DateTime.Now,
                UpdateBy = checkBy,
                UpdateTime = DateTime.Now
            }, new List<string>()
            {
                "CheckStatus", "CheckComment", "CheckBy", "CheckTime", "UpdateBy", "UpdateTime"
            });

            return true;
        }

        /// <summary>
        /// 点赞评论
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> LikeVideoComment(LikeVideoCommentRequest request)
        {
            string redisKey = $"LikeVideoComment_{request.CommentId}_{request.UserId}";
            try
            {
                var result = true;
                //result = await _redisClient.Redis.LockTakeAsync(redisKey, _token, new TimeSpan(0, 0, 0, 10));
                if (result)
                {
                    var likeComment =
                        await _videoCommentFavourRepository.GetVideoCommentFavourByCommentId(request.CommentId,
                            request.UserId, false);
                    if (request.Status == 1) //点赞
                    {
                        if (!likeComment.Any())
                        {
                            await LikeVideoComment1(request);
                        }
                    }
                    else if (request.Status == 0) //取消点赞
                    {
                        if (likeComment.Any())
                        {
                            await LikeVideoComment0(likeComment, request.CommentId, request.UserId);
                        }
                    }
                }
                else
                {
                    throw new CustomException("请求过于频繁，请稍后再试");
                }
            }
            catch (CustomException ex)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.Info($"LikeVideoComment_Error {JsonConvert.SerializeObject(ex)}");
                throw;
            }
            finally
            {
                //await _redisClient.Redis.LockReleaseAsync(redisKey, _token);
            }

            return true;
        }

        /// <summary>
        /// 点赞
        /// </summary>
        /// <returns></returns>
        private async Task<bool> LikeVideoComment1(LikeVideoCommentRequest request)
        {
            using (TransactionScope ts = new TransactionScope())
            {
                await _videoCommentFavourRepository.InsertAsync(new VideoCommentFavourDO()
                {
                    CommentId = request.CommentId,
                    UserId = Guid.Parse(request.UserId),
                    Status = request.Status,
                    CreateBy = request.UserId,
                    CreateTime = DateTime.Now
                });
                await _videoCommentRepository.UpdateLikeNum(request.CommentId, 1, request.UserId);

                ts.Complete();
            }

            return true;
        }

        /// <summary>
        /// 取消点赞
        /// </summary>
        /// <param name="favour"></param>
        /// <param name="commentId"></param>
        /// <param name="updateBy"></param>
        /// <returns></returns>
        private async Task<bool> LikeVideoComment0(List<VideoCommentFavourDO> favour, long commentId, string updateBy)
        {
            using (TransactionScope ts = new TransactionScope())
            {
                foreach (var _ in favour)
                {
                    await _videoCommentFavourRepository.UpdateAsync(new VideoCommentFavourDO()
                    {
                        Id = _.Id,
                        Status = 0,
                        UpdateBy = updateBy,
                        UpdateTime = DateTime.Now
                    }, new List<string>()
                    {
                        "Status", "UpdateBy", "UpdateTime"
                    });
                }

                await _videoCommentRepository.UpdateLikeNum(commentId, -favour.Count, updateBy);

                ts.Complete();
            }

            return true;
        }

        /// <summary>
        /// 获取视频最新的一条评论
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<List<VideoCommentListVo>> GetLastCommentByVideoIds(LastCommentByVideoIdsRequest request)
        {
            if (request.VideoIdList != null && request.VideoIdList.Any())
            {
                var result = await _videoCommentRepository.GetLastComment(request.VideoIdList);

                return result.Select(_ => new VideoCommentListVo
                {
                    CommentId = _.Id,
                    VideoId = _.VideoId,
                    Content = _.Content,
                    LikeNum = _.LikeNum,
                    CommentTime = _.CreateTime
                }).ToList();
            }

            return new List<VideoCommentListVo>();
        }

        /// <summary>
        /// 删除评论
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> DeleteVideoComment(DeleteVideoCommentRequest request)
        {
            var result = await _videoCommentRepository.UpdateAsync(new VideoCommentDO()
            {
                Id = request.CommentId,
                IsDeleted = 1,
                UpdateBy = request.SubmitBy,
                UpdateTime = DateTime.Now
            }, new List<string>()
            {
                "IsDeleted", "UpdateBy", "UpdateTime"
            });

            return result > 0;
        }

        public async Task<List<VideoCommentCountRes>> GetVideoCommentCount(GetVideoCommentCountRequest request)
        {
            var res = new List<VideoCommentCountRes>();
            try
            {
                var commentRes = await _videoCommentRepository.GetVideoCommentCount(request.VideoIds, request.VideoType);
                commentRes.ForEach(r =>
                {
                    res.Add(new VideoCommentCountRes
                    {
                        Num = r.LikeNum,
                        VideoId = r.VideoId
                    });
                });
            }
            catch (Exception ex)
            {
                _logger.Error($"GetVideoCommentCount_Error Data:{JsonConvert.SerializeObject(request)}", ex);
            }

            return res;
        }
    }
}
