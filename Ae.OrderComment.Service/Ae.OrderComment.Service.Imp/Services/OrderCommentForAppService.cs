using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.Formatters.Internal;
using Microsoft.Extensions.Configuration;
using ApolloErp.Log;
using ApolloErp.Web.WebApi;
using Ae.OrderComment.Service.Client.Interface;
using Ae.OrderComment.Service.Client.Request;
using Ae.OrderComment.Service.Common;
using Ae.OrderComment.Service.Common.Constant;
using Ae.OrderComment.Service.Core.Enums;
using Ae.OrderComment.Service.Core.Interfaces;
using Ae.OrderComment.Service.Core.Model.OrderCommentForApp;
using Ae.OrderComment.Service.Core.Request;
using Ae.OrderComment.Service.Core.Request.OrderCommentForApp;
using Ae.OrderComment.Service.Core.Response.OrderCommentForApp;
using Ae.OrderComment.Service.Dal.Model;
using Ae.OrderComment.Service.Dal.Repositorys.OrderComment;

namespace Ae.OrderComment.Service.Imp.Services
{
    /// <summary>
    /// 订单评论服务ForApp
    /// </summary>
    public class OrderCommentForAppService : IOrderCommentForAppService
    {
        private readonly IMapper _mapper;
        private readonly ApolloErpLogger<OrderCommentForAppService> _logger;
        private readonly IOrderCommentRepository _orderCommentRepository;
        private readonly IShopManageClient _shopManageClient;
        private readonly IConfiguration _configuration;
        private readonly IOrderCommentService _orderCommentService;

        public OrderCommentForAppService(IMapper mapper, ApolloErpLogger<OrderCommentForAppService> logger,
            IOrderCommentRepository orderCommentRepository, IShopManageClient shopManageClient,
            IConfiguration configuration, IOrderCommentService orderCommentService)
        {
            _orderCommentRepository = orderCommentRepository;
            _mapper = mapper;
            _logger = logger;
            _shopManageClient = shopManageClient;
            _configuration = configuration;
            _orderCommentService = orderCommentService;
        }

        /// <summary>
        /// 得到评论列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<GetCommentListResponse>> GetCommentList(GetCommentListRequest request)
        {
            var getCommentListData = new GetCommentListResponse();

            getCommentListData.OrderTypeSelectItems = GetOrderTypeSelectItems();
            getCommentListData.ScoreLevelSelectItems = GetScoreLevelSelectItems();
            getCommentListData.ReplyStatusSelectItems = GetReplyStatusSelectItems();
            getCommentListData.Items = new List<GetCommentListVO>();

            // var shopInfo = await _shopManageClient.GetShopSimpleInfoAsync(new GetShopSimpleInfoClientRequest()
            // {
            //     ShopId = request.ShopId
            // });
            //
            // var shopAgaveScore = shopInfo?.Data?.Score ?? 0;
            var shopScore = await _orderCommentService.GetShopScore(new ShopScoreRequest()
            {
                ShopIdList = new List<int>() { (int)request.ShopId }
            });

            decimal shopAgaveScore = shopScore.FirstOrDefault()?.Score ?? 0;

            getCommentListData.ShopAvgScore = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("门店评价", shopAgaveScore.ToString("F2"))
            };

            var getCommentListDalResponse = await _orderCommentRepository.GetCommentList(request);
            if (getCommentListDalResponse?.Items?.Any() ?? false)
            {
                getCommentListData.Items = _mapper.Map<List<GetCommentListVO>>(getCommentListDalResponse.Items);
                getCommentListData.TotalItems = getCommentListDalResponse.TotalItems;

                var getCommentImageList = await _orderCommentRepository.GetCommentImageList(
                    getCommentListDalResponse?.Items?.Select(_ => _.Id)?.ToList(),
                    RelationTypeEnum.FirstComment.ToInt());

                getCommentListData.Items?.ForEach(_ =>
                {
                    _.HeadUrl = GetImageUrl(_.HeadUrl);
                    _.ImgList = new List<ImgVO>();
                    getCommentImageList?.Where(item => item.CommentId == _.Id)?.ToList()?.ForEach(
                        item =>
                        {
                            _.ImgList.Add(new ImgVO()
                            {
                                Url = GetImageUrl(item.ImageUrl)
                            });
                        });
                });

                var geCommentReplyList = await _orderCommentRepository.GetCommentReplyList(
                     getCommentListDalResponse?.Items?.Select(_ => _.Id)?.ToList(), request.ShopId);

                getCommentListData.Items?.ForEach(_ =>
                {

                    _.ReplyInfos = new List<CommentListReplyInfoVO>();
                    //客户追评的数据先展示
                    geCommentReplyList?.Where(item => item.ReplyId == _.CommentId && item.CheckStatus == CheckStatusEnum.审核通过.ToInt())?.ToList()?.ForEach(item =>
                      {
                          if (item.ReplyType == ReplyTypeEnum.CustomerAppendComment.ToInt())
                          {
                              _.ReplyInfos.Add(new CommentListReplyInfoVO()
                              {
                                  CommentId = item.Id,
                                  CommentType = CommentTypeEnum.CustomerAppendComment,
                                  DisplayTitle = CommentTypeEnum.CustomerAppendComment.GetEnumDescription(),
                                  ReplyContent = item.Content
                              });
                          }
                      });
                    //商户评价
                    geCommentReplyList?.Where(item => item.ReplyId == _.CommentId && item.CheckStatus == CheckStatusEnum.审核通过.ToInt())?.ToList()?.ForEach(item =>
                    {
                        if (item.ReplyType == ReplyTypeEnum.ReplyComment.ToInt())
                        {
                            int minuteDay = item.CreateTime.Day - _.CreateTime.Day;
                            string contnet=minuteDay>0?$"{minuteDay} 天后":"当日";
                            _.ReplyInfos.Add(new CommentListReplyInfoVO()
                            {
                                CommentId = item.Id,
                                CommentType = CommentTypeEnum.ReplyComment,
                                DisplayTitle = $"{CommentTypeEnum.ReplyComment.GetEnumDescription()}({contnet})",
                                ReplyContent = item.Content
                            });
                        }
                    });
                    //商户追评
                    geCommentReplyList?.Where(item => item.ReplyType == ReplyTypeEnum.ReplyAppendComment.ToInt()
                                                      && item.CheckStatus == CheckStatusEnum.审核通过.ToInt())?.ToList()?.ForEach(item =>
                    {
                        geCommentReplyList?.Where(x => x.Id == item.ReplyId)?.Where(x => x.ReplyId == _.CommentId)?.ToList()?.ForEach(
                            x =>
                            {
                               
                                int minuteDay = item.CreateTime.Day - _.CreateTime.Day;
                                string contnet = minuteDay > 0 ? $"{minuteDay} 天后" : "当日";
                                _.ReplyInfos.Add(new CommentListReplyInfoVO()
                                {
                                    CommentId = item.Id,
                                    CommentType = CommentTypeEnum.ReplyAppendComment,
                                    DisplayTitle = $"{CommentTypeEnum.ReplyAppendComment.GetEnumDescription()}({contnet})",
                                    ReplyContent = item.Content
                                });
                            });

                    });

                    _.ReplyButtons = new List<UserOperationVO>();
                    if (!geCommentReplyList?.Where(item => item.ReplyType == ReplyTypeEnum.ReplyComment.ToInt()&&item.ReplyId==_.CommentId)?.Any() ?? true)
                    {
                        _.ReplyButtons.Add(new UserOperationVO
                        {
                            Function = ReplyTypeEnum.ReplyComment.ToString(),
                            ShowFunctionName = ReplyTypeEnum.ReplyComment.GetDescription(),
                            IsImportance = true,
                            Sort = 1,
                            CommentId = _.Id
                        });
                    }

                    var customerAppendDataCommentId =
                        geCommentReplyList
                            ?.Where(item =>
                                item.ReplyType == ReplyTypeEnum.CustomerAppendComment.ToInt() &&
                                item.ReplyId == _.CommentId)?.Select(item => item.Id)?.FirstOrDefault() ?? 0;
                 

                    var isReplayAppendData =
                        geCommentReplyList?.Where(item => item.ReplyType == ReplyTypeEnum.ReplyAppendComment.ToInt()&&item.ReplyId== customerAppendDataCommentId)?.Any() ??
                        false;

                    if (customerAppendDataCommentId > 0 && isReplayAppendData == false)
                    {
                        _.ReplyButtons.Add(new UserOperationVO
                        {
                            Function = ReplyTypeEnum.ReplyAppendComment.ToString(),
                            ShowFunctionName = ReplyTypeEnum.ReplyAppendComment.GetDescription(),
                            IsImportance = true,
                            Sort = 1,
                            CommentId = _.ReplyInfos?.Where(x=>x.CommentType==CommentTypeEnum.CustomerAppendComment)?.FirstOrDefault()?.CommentId??0
                        });
                    }
                });

                return new ApiResult<GetCommentListResponse>()
                {
                    Code = ResultCode.Success,
                    Data = getCommentListData
                };
            }


            return new ApiResult<GetCommentListResponse>()
            {
                Code = ResultCode.Success,
                Data = getCommentListData,
            };
        }

        /// <summary>
        /// 得到订单类型选项ForCommentList
        /// </summary>
        /// <returns></returns>
        private List<CommentSelectVO> GetOrderTypeSelectItems()
        {
            return new List<CommentSelectVO>()
            {
                new CommentSelectVO()
                {
                    Name = "全部分类",
                    Code = "All",
                    Checked = true
                },
                new CommentSelectVO()
                {
                    Name = "轮胎",
                    Code = "Tire",
                    Checked = false,
                },
                new CommentSelectVO()
                {
                    Name = "保养",
                    Code = "BaoYang",
                    Checked = false
                },
                new CommentSelectVO()
                {
                    Name = "美容",
                    Code = "Beauty",
                    Checked = false
                }
            };
        }

        /// <summary>
        /// 得到分值选项
        /// </summary>
        /// <returns></returns>
        private List<CommentSelectVO> GetScoreLevelSelectItems()
        {
            return new List<CommentSelectVO>()
            {
                new CommentSelectVO()
                {
                    Name = "全部评价",
                    Code = "All",
                    Checked = true
                },
                new CommentSelectVO()
                {
                    Name = "门店好评",
                    Code = "ShopGood",
                    Checked = false
                },
                new CommentSelectVO()
                {
                    Name = "门店差评",
                    Code = "ShopNegative",
                    Checked = false
                }
            };
        }

        /// <summary>
        /// 得到回复状态
        /// </summary>
        /// <returns></returns>
        private List<CommentSelectVO> GetReplyStatusSelectItems()
        {
            return new List<CommentSelectVO>()
            {
                new CommentSelectVO()
                {
                    Name = "全部回复",
                    Code = "All",
                    Checked = true
                },
                new CommentSelectVO()
                {
                    Name = "门店已回",
                    Code = "ShopReply",
                    Checked = false
                },
                new CommentSelectVO()
                {
                    Name = "门店未回",
                    Code = "ShopNotReply",
                    Checked = false
                }
            };
        }

        private string GetImageUrl(string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                return string.Empty;
            }

            if (url.StartsWith("http://") || url.StartsWith("https://"))
            {
                return url;
            }

            return _configuration["BaseImageUrl"] + url;
        }

        /// <summary>
        /// 点评明细
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<ReplyDetailResponse>> ReplyDetail(ReplyDetailRequest request)
        {
            var data = new ReplyDetailResponse();

            var commentDOs = await _orderCommentRepository.GetCommentListByCommentIdsForScore(new List<long>() { request.CommentId },
                request.ShopId, CheckStatusEnum.审核通过.ToInt());

            data = _mapper.Map<ReplyDetailResponse>(commentDOs?.FirstOrDefault()) ?? new ReplyDetailResponse();

            var getCommentImageList = await _orderCommentRepository.GetCommentImageList(
               new List<long>() { data?.CommentId ?? 0 },
                RelationTypeEnum.FirstComment.ToInt());

            data.ImgList = new List<ImgVO>();
            data.HeadUrl = GetImageUrl(data.HeadUrl);
            getCommentImageList?.ForEach(
                item =>
                {
                    data.ImgList.Add(new ImgVO()
                    {
                        Url = GetImageUrl(item.ImageUrl)
                    });
                });

            var geCommentReplyList = await _orderCommentRepository.GetCommentReplyList(
                new List<long>() { data?.CommentId ?? 0 }, request.ShopId);

            data.ReplyInfos = new List<CommentListReplyInfoVO>();
            //客户追评的数据先展示
            geCommentReplyList?.Where(item => item.ReplyId == data.CommentId)?.ToList()?.ForEach(item =>
            {
                if (item.ReplyType == ReplyTypeEnum.CustomerAppendComment.ToInt())
                {
                    data.ReplyInfos.Add(new CommentListReplyInfoVO()
                    {
                        CommentId = item.Id,
                        CommentType = CommentTypeEnum.CustomerAppendComment,
                        DisplayTitle = CommentTypeEnum.CustomerAppendComment.GetEnumDescription(),
                        ReplyContent = item.Content
                    });
                }
            });
            //商户评价
            geCommentReplyList?.Where(item => item.ReplyId == data.CommentId)?.ToList()?.ForEach(item =>
            {
                if (item.ReplyType == ReplyTypeEnum.ReplyComment.ToInt())
                {
                  //  TimeSpan minuteDay = item.CreateTime - data.CreateTime;
                    int minuteDay = item.CreateTime.Day - data.CreateTime.Day;
                    string contnet = minuteDay > 0 ? $"{minuteDay} 天后" : "当日";
                    data.ReplyInfos.Add(new CommentListReplyInfoVO()
                    {
                        CommentId = item.Id,
                        CommentType = CommentTypeEnum.ReplyComment,
                        DisplayTitle = $"{CommentTypeEnum.ReplyComment.GetEnumDescription()}({contnet})",
                        ReplyContent = item.Content
                    });
                }
            });
            //商户追评
            geCommentReplyList?.Where(item => item.ReplyId == data.CommentId)?.ToList()?.ForEach(item =>
            {
                if (item.ReplyType == ReplyTypeEnum.ReplyAppendComment.ToInt())
                {
                    //TimeSpan minuteDay = item.CreateTime - data.CreateTime;
                    int minuteDay = item.CreateTime.Day - data.CreateTime.Day;
                    string contnet = minuteDay > 0 ? $"{minuteDay} 天后" : "当日";
                    data.ReplyInfos.Add(new CommentListReplyInfoVO()
                    {
                        CommentId = item.Id,
                        CommentType = CommentTypeEnum.ReplyAppendComment,
                        DisplayTitle = $"{CommentTypeEnum.ReplyAppendComment.GetEnumDescription()}({contnet})",
                        ReplyContent = item.Content
                    });
                }
            });

            return new ApiResult<ReplyDetailResponse>()
            {
                Code = ResultCode.Success,
                Data = data
            };

        }

        /// <summary>
        /// 提交评论
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult> ReplyComment(ReplyCommentRequest request)
        {
            bool autoCheck = Convert.ToInt32(_configuration["SwitchConfig:CommentAutoCheck"]) > 0; //自动审核 
            var getCommentReplyListData =
                await _orderCommentRepository.GetCommentReplyList(new List<long>() { request.ReplyToCommentId },
                    request.ShopId);

            //验证是否评价过
            switch (request.ReplyType)
            {
                case CommentTypeEnum.ReplyComment when getCommentReplyListData?.Select(x => x.ReplyType == ReplyTypeEnum.ReplyComment.ToInt() && x.ReplyPartType == ReplyPartTypeEnum.门店商家.ToInt())?.Contains(true) ?? false:
                    return new ApiResult()
                    {
                        Code = ResultCode.Failed,
                        Message = CommonConstant.ShopCommentIsExist
                    };
                case CommentTypeEnum.ReplyAppendComment when getCommentReplyListData?.Select(x => x.ReplyType == ReplyTypeEnum.ReplyAppendComment.ToInt() && x.ReplyPartType == ReplyPartTypeEnum.门店商家.ToInt())?.Contains(true) ?? false:
                    return new ApiResult()
                    {
                        Code = ResultCode.Failed,
                        Message = CommonConstant.ShopAppendCommentIsExist
                    };
            }

            List<CommentDO> getCommentInfo = null;
            //验证评价的数据是否存在
            switch (request.ReplyType)
            {
                case CommentTypeEnum.ReplyComment:
                    {
                        getCommentInfo = await _orderCommentRepository.GetCommentListByCommentIds(new List<long>() { request.ReplyToCommentId },
                            shopId: request.ShopId, CheckStatusEnum.审核通过.ToInt());
                        if (getCommentInfo == null || !getCommentInfo.Any())
                            return new ApiResult()
                            {
                                Code = ResultCode.Failed,
                                Message = CommonConstant.CommentIsNotExist
                            };
                        break;
                    }
                case CommentTypeEnum.ReplyAppendComment:
                    {
                        getCommentInfo =
                            await _orderCommentRepository.GetAppendCommentDo(request.ReplyToCommentId, request.ShopId);
                        if (getCommentInfo == null || !getCommentInfo.Any())
                            return new ApiResult()
                            {
                                Code = ResultCode.Failed,
                                Message = CommonConstant.CustomeAppendIsNotExist
                            };

                        break;
                    }
            }

            //var commentId = getCommentReplyListData?.Where(item => item.ReplyType == ReplyTypeEnum.CustomerAppendComment.ToInt())
            //      ?.Select(item => item.ReplyId)?.FirstOrDefault() ?? 0;
            //if (getCommentInfo == null || !getCommentInfo.Any())
            //    getCommentInfo = await _orderCommentRepository.GetCommentListByCommentIds(new List<long>() { commentId },
            //        shopId: request.ShopId, CheckStatusEnum.审核通过.ToInt());

            var id = getCommentInfo?.FirstOrDefault()?.Id ?? 0;
            CommentDO commentDo = new CommentDO()
            {
                Id = id
            };
            CommentReplyDO commentReplyDo = new CommentReplyDO()
            {
                ReplyId = request.ReplyToCommentId,
                ReplyPartType = (sbyte)ReplyPartTypeEnum.门店商家.ToInt(),
                ReplyType = (request.ReplyType == CommentTypeEnum.ReplyComment)
                    ? (sbyte)ReplyTypeEnum.ReplyComment.ToInt()
                    : (sbyte)ReplyTypeEnum.ReplyAppendComment.ToInt(),
                ShopId = getCommentInfo?.FirstOrDefault()?.ShopId ?? 0,
                ShopName = getCommentInfo?.FirstOrDefault()?.ShopName,
                Channel = getCommentInfo?.FirstOrDefault()?.Channel ?? 0,
                OrderType = getCommentInfo?.FirstOrDefault()?.OrderType ?? 0,
                OrderNo = getCommentInfo?.FirstOrDefault()?.OrderNo,
                CheckStatus = (sbyte)CheckStatusEnum.待审核.ToInt(),
                CheckComment = string.Empty,
                Content = request.ReplyContent.Trim(),
                IsDeleted = 0,
                CreateTime = DateTime.Now,
                UpdateBy = request.UserName,
                CreateBy = request.UserName,
                UpdateTime = DateTime.Now
            };

            var saveResult = await _orderCommentRepository.SaveCommentReplyInfo(commentDo, commentReplyDo, request.UserName);

            if (saveResult > 0)
            {
                if (autoCheck)//自动审核
                {
                    await _orderCommentService.CheckOrderReply(new CheckOrderCommentRequest()
                    {
                        CommentId = saveResult,
                        CheckComment = "自动审核",
                        CheckStatus = 1
                    });
                }

                return new ApiResult()
                {
                    Code = ResultCode.Success,
                    Message = CommonConstant.CommentSubmitSuccess
                };
            }

            return new ApiResult()
            {
                Code = ResultCode.Failed,
                Message = CommonConstant.CommentSubmitFailure
            };
        }

        /// <summary>
        /// 今日点评统计
        /// </summary>
        /// <param name="shopId"></param>
        /// <returns></returns>
        public async Task<ApiResult<GetTodayCommentResponse>> GetTodayCommentCount(int shopId)
        {
            var getTodayComment = await _orderCommentRepository.GetTodayCommentCount(shopId);
            var shopInfo = await _shopManageClient.GetShopSimpleInfoAsync(new GetShopSimpleInfoClientRequest()
            {
                ShopId = shopId
            });
            getTodayComment.Score = shopInfo?.Data?.Score ?? 0;
            return new ApiResult<GetTodayCommentResponse>()
            {
                Code = ResultCode.Success,
                Data = getTodayComment
            };
        }
    }
}
