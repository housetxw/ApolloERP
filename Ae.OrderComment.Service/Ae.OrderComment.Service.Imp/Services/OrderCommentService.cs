using AutoMapper;
using ApolloErp.Log;
using ApolloErp.Web.WebApi;
using Ae.OrderComment.Service.Client.Clients;
using Ae.OrderComment.Service.Core.Interfaces;
using Ae.OrderComment.Service.Core.Model;
using Ae.OrderComment.Service.Core.Request;
using Ae.OrderComment.Service.Dal.Repositorys.OrderComment;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Ae.OrderComment.Service.Common.CustomAttribute;
using Ae.OrderComment.Service.Core.Enums;
using System.ComponentModel;
using System.Linq;
using Ae.OrderComment.Service.Common;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Ae.OrderComment.Service.Core.Response;
using Ae.OrderComment.Service.Client.Request;
using Ae.OrderComment.Service.Client.Interface;
using Ae.OrderComment.Service.Dal.Model;
using System.Transactions;
using Ae.OrderComment.Service.Client.Model;
using Newtonsoft.Json;
using Ae.OrderComment.Service.Common.Exceptions;

namespace Ae.OrderComment.Service.Imp.Services
{
    public class OrderCommentService : IOrderCommentService
    {
        private IConfiguration Configuration { get; }
        private readonly IMapper mapper;
        private readonly ApolloErpLogger<OrderCommentService> logger;
        private readonly IOrderCommentRepository orderCommentRepository;
        private readonly IShopManageClient shopManageClient;
        private readonly IOrderClient orderClient;
        private readonly ICommentLabelConfigRepository commentLabelConfigRepository;
        private readonly IUserClient userClient;
        private readonly ICommentDetailShopRepository commentDetailShopRepository;
        private readonly ICommentDetailProductRepository commentDetailProductRepository;
        private readonly ICommentImageRepository commentImageRepository;
        private readonly ICommentLabelSelectedRepository commentLabelSelectedRepository;
        private readonly ICommentRewardRuleConfigRepository commentRewardRuleConfigRepository;
        private readonly IVehicleClient vehicleClient;
        private readonly ICommentReplyRepository commentReplyRepository;
        private readonly IProductManageClient productManageClient;

        private static string defaultUser = "系统";

        public OrderCommentService(IConfiguration configuration,
            IMapper mapper,
            ApolloErpLogger<OrderCommentService> logger,
            IOrderCommentRepository orderCommentRepository,
            IShopManageClient shopManageClient,
            IOrderClient orderClient,
            ICommentLabelConfigRepository commentLabelConfigRepository,
            IUserClient userClient,
            ICommentDetailShopRepository commentDetailShopRepository,
            ICommentDetailProductRepository commentDetailProductRepository,
            ICommentImageRepository commentImageRepository,
            ICommentLabelSelectedRepository commentLabelSelectedRepository,
            ICommentRewardRuleConfigRepository commentRewardRuleConfigRepository,
            IVehicleClient vehicleClient,
            ICommentReplyRepository commentReplyRepository,
            IProductManageClient productManageClient
            )
        {
            Configuration = configuration;
            this.mapper = mapper;
            this.logger = logger;
            this.orderCommentRepository = orderCommentRepository;
            this.shopManageClient = shopManageClient;
            this.orderClient = orderClient;
            this.commentLabelConfigRepository = commentLabelConfigRepository;
            this.userClient = userClient;
            this.commentDetailShopRepository = commentDetailShopRepository;
            this.commentDetailProductRepository = commentDetailProductRepository;
            this.commentImageRepository = commentImageRepository;
            this.commentLabelSelectedRepository = commentLabelSelectedRepository;
            this.commentRewardRuleConfigRepository = commentRewardRuleConfigRepository;
            this.vehicleClient = vehicleClient;
            this.commentReplyRepository = commentReplyRepository;
            this.productManageClient = productManageClient;
        }

        public async Task<int> CheckOrderComment(CheckOrderCommentRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.UpdateBy))
            {
                request.UpdateBy = defaultUser;
            }
            await orderCommentRepository.CheckOrderComment(request);
            return 1;
        }

        public async Task<List<CommentImageDTO>> GetCommentImages(GetCommentImageRequest request)
        {
            var result = await orderCommentRepository.GetCommentImages(request);

            var vo = mapper.Map<List<CommentImageDTO>>(result);
            return vo;

        }

        /// <summary>
        /// 产品评论 TODO:只查客户点评和客户追评
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiPagedResultData<GetOrderCommentForProductDTO>> GetOrderCommentForProductList(GetOrderCommentForProductRequest request)
        {
            if (request.CreateTime != null && request.CreateTime.Any())
            {
                request.CreateStartTime = Convert.ToDateTime(request.CreateTime[0]);
                request.CreateEndTime = Convert.ToDateTime(request.CreateTime[1]); ;
            }

            var result = await orderCommentRepository.GetOrderCommentForProductList(request);
            var res = new ApiPagedResultData<GetOrderCommentForProductDTO>();
            var productCommentList = new List<GetOrderCommentForProductDTO>();
            //var vo = mapper.Map<ApiPagedResultData<GetOrderCommentForProductDTO>>(result);
            if (result.Items != null && result.Items.Any())
            {
                #region 获取评论图片
                var commentImageList = await orderCommentRepository.GetCommentImages(new GetCommentImageRequest
                {
                    CommentIds = result.Items.Select(r => r.Id).Distinct().ToList(),
                    RelationType = 1
                });

                var commentImageDic = new Dictionary<long, List<string>>();
                if (commentImageList.Any())
                {
                    foreach (var item in commentImageList)
                    {
                        if (commentImageDic.ContainsKey(item.CommentId))
                        {

                            commentImageDic[item.CommentId].Add(GetImageUrl(item.ImageUrl));
                        }
                        else
                        {
                            commentImageDic[item.CommentId] = new List<string>();
                            commentImageDic[item.CommentId].Add(GetImageUrl(item.ImageUrl));
                        }
                    }
                }
                #endregion

                foreach (var orderCommentItem in result.Items.GroupBy(r => new { r.Id }))
                {
                    var commentVo = orderCommentItem.First();
                    var productCommentVo = new GetOrderCommentForProductDTO
                    {
                        Id = commentVo.Id,
                        OrderId = commentVo.OrderId,
                        OrderNo = commentVo.OrderNo,
                        Channel = commentVo.Channel,
                        ChannelStr = ((ChannelEnum)commentVo.Channel).GetEnumDescription(),
                        OrderType = commentVo.OrderType,
                        OrderTypeStr = ((OrderTypeEnum)commentVo.OrderType).GetEnumDescription(),
                        UserId = commentVo.UserId,
                        UserNickName = commentVo.UserNickName,
                        IsAnonymous = commentVo.IsAnonymous,
                        CarId = commentVo.CarId,
                        CheckComment = commentVo.CheckComment,
                        IsDeleted = commentVo.IsDeleted,
                        CheckStatus = commentVo.CheckStatus,
                        CheckStatusStr = ((CheckStatusEnum)commentVo.CheckStatus).GetEnumDescription(),
                        Content = commentVo.Content,
                        CreateBy = commentVo.CreateBy,
                        CreateTime = commentVo.CreateTime,
                        IsBest = commentVo.IsBest,
                        IsCheck = commentVo.CheckStatus == 0 ? false : true,
                        IsTop = commentVo.IsTop,
                        LikeNum = commentVo.LikeNum,
                        Score = commentVo.Score,
                        ShopId = commentVo.ShopId,
                        ShopName = commentVo.ShopName
                    };
                    if (commentImageDic.ContainsKey(commentVo.Id))
                    {
                        productCommentVo.ImageList = commentImageDic[commentVo.Id];
                    }
                    var productList = new List<OrderCommentProductDTO>();
                    foreach (var item in orderCommentItem)
                    {
                        var productInfo = new OrderCommentProductDTO
                        {
                            Id = item.Id,
                            ProductDisplayName = item.ProductDisplayName,
                            ProductId = item.ProductId,
                            ProductImageUrl = GetImageUrl(item.ProductImageUrl),
                            Score = item.Score
                        };
                        productList.Add(productInfo);
                    }
                    productCommentVo.Products = productList;

                    productCommentList.Add(productCommentVo);
                }
            }
            res.Items = productCommentList;
            res.TotalItems = result.TotalItems;
            return res;
        }

        /// <summary> 
        /// 门店评论 TODO:只查客户点评和客户追评
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiPagedResultData<GetOrderCommentForShopDTO>> GetOrderCommentForShopList(GetOrderCommentForShopRequest request)
        {
            if (request.CreateTime != null && request.CreateTime.Any())
            {
                request.CreateStartTime = Convert.ToDateTime(request.CreateTime[0]);
                request.CreateEndTime = Convert.ToDateTime(request.CreateTime[1]); ;
            }

            var result = await orderCommentRepository.GetOrderCommentForShopList(request);

            var vo = mapper.Map<ApiPagedResultData<GetOrderCommentForShopDTO>>(result);
            if (vo.Items != null && vo.Items.Any())
            {
                #region 获取评论图片
                var commentImageList = await orderCommentRepository.GetCommentImages(new GetCommentImageRequest
                {
                    CommentIds = vo.Items.Select(r => r.Id).Distinct().ToList(),
                    RelationType = 1
                });

                var commentImageDic = new Dictionary<long, List<string>>();
                if (commentImageList.Any())
                {
                    foreach (var item in commentImageList)
                    {
                        if (commentImageDic.ContainsKey(item.CommentId))
                        {

                            commentImageDic[item.CommentId].Add(GetImageUrl(item.ImageUrl));
                        }
                        else
                        {
                            commentImageDic[item.CommentId] = new List<string>();
                            commentImageDic[item.CommentId].Add(GetImageUrl(item.ImageUrl));
                        }
                    }
                }
                #endregion

                foreach (var item in vo.Items)
                {
                    item.ChannelStr = ((ChannelEnum)item.Channel).GetEnumDescription();
                    item.CheckStatusStr = ((CheckStatusEnum)item.CheckStatus).GetEnumDescription();
                    item.OrderTypeStr = ((OrderTypeEnum)item.OrderType).GetEnumDescription();
                    item.TypeStr = ((CommentTypeEnum)item.Type).GetEnumDescription();
                    item.IsCheck = item.CheckStatus == 0 ? false : true;
                    item.ShopImageUrl = GetImageUrl(item.ShopImageUrl);
                    if (commentImageDic.ContainsKey(item.Id))
                    {
                        item.ImageList = commentImageDic[item.Id];
                    }
                }
            }
            return vo;
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

            return Configuration["BaseImageUrl"] + url;
        }

        /// <summary>
        /// 技师评论 TODO:只查客户点评和客户追评
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiPagedResultData<GetOrderCommentForTechDTO>> GetOrderCommentForTechList(GetOrderCommentForTechRequest request)
        {
            if (request.CreateTime != null && request.CreateTime.Any())
            {
                request.CreateStartTime = Convert.ToDateTime(request.CreateTime[0]);
                request.CreateEndTime = Convert.ToDateTime(request.CreateTime[1]); ;
            }

            var result = await orderCommentRepository.GetOrderCommentForTechList(request);

            var vo = mapper.Map<ApiPagedResultData<GetOrderCommentForTechDTO>>(result);
            if (vo.Items != null && vo.Items.Any())
            {
                #region 获取评论图片
                var commentImageList = await orderCommentRepository.GetCommentImages(new GetCommentImageRequest
                {
                    CommentIds = vo.Items.Select(r => r.Id).Distinct().ToList(),
                    RelationType = 1
                });

                var commentImageDic = new Dictionary<long, List<string>>();
                if (commentImageList.Any())
                {
                    foreach (var item in commentImageList)
                    {
                        if (commentImageDic.ContainsKey(item.CommentId))
                        {

                            commentImageDic[item.CommentId].Add(GetImageUrl(item.ImageUrl));
                        }
                        else
                        {
                            commentImageDic[item.CommentId] = new List<string>();
                            commentImageDic[item.CommentId].Add(GetImageUrl(item.ImageUrl));
                        }
                    }
                }
                #endregion

                foreach (var item in vo.Items)
                {
                    item.ChannelStr = ((ChannelEnum)item.Channel).GetEnumDescription();
                    item.CheckStatusStr = ((CheckStatusEnum)item.CheckStatus).GetEnumDescription();
                    item.OrderTypeStr = ((OrderTypeEnum)item.OrderType).GetEnumDescription();
                    item.TypeStr = ((CommentTypeEnum)item.Type).GetEnumDescription();
                    item.IsCheck = item.CheckStatus == 0 ? false : true;

                    item.TechLevelStr = ((TechLevelEnum)item.TechLevel).GetEnumDescription();
                    item.IsCheck = item.CheckStatus == 0 ? false : true;
                    if (commentImageDic.ContainsKey(item.Id))
                    {
                        item.ImageList = commentImageDic[item.Id];
                    }

                }
            }
            return vo;
        }


        public async Task<List<BasicInfoDTO>> GetBasicInfoDTOs<T>(int type = 0)
        {
            var list = new List<BasicInfoDTO>();

            await Task.Run(() =>
            {
                if (type == 0)
                {
                    foreach (var value in Enum.GetValues(typeof(T)))
                    {
                        var m = new BasicInfoDTO();
                        m.Key = Convert.ToInt32(value).ToString();
                        m.Value = value.ToString();
                        if (m.Value != "未设置")
                        {
                            list.Add(m);
                        }
                    }
                }
                else
                {
                    foreach (var value in Enum.GetValues(typeof(T)))
                    {
                        string enumName = Enum.GetName(value.GetType(), value);  //获取对应的枚举名
                        if (!string.IsNullOrEmpty(enumName))
                        {
                            FieldInfo field = value.GetType().GetField(enumName);
                            var enumCustomAttribute = field.GetCustomAttribute<EnumDescriptionAttribute>();

                            var m = new BasicInfoDTO();
                            m.Key = Convert.ToInt32(value).ToString();
                            m.Value = enumCustomAttribute.Name;
                            if (m.Value != "未设置")
                            {
                                list.Add(m);
                            }
                        }
                    }
                }
            });

            return list;
        }

        /// <summary>
        /// 获取基础信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<List<BasicInfoDTO>> GetBaseInfos(BasicInfoRequest request)
        {
            var basicInfoList = new List<BasicInfoDTO>();
            switch (request.RequestType)
            {
                //TODO:返回客户点评和客户追评的字段!
                case 1:
                    basicInfoList.Add(new BasicInfoDTO { Key = "1", Value = "客户点评" });
                    basicInfoList.Add(new BasicInfoDTO { Key = "3", Value = "客户追评" });
                    break;
                case 2:
                    basicInfoList = await GetBasicInfoDTOs<CheckStatusEnum>();
                    break;
                case 3:
                    basicInfoList = await GetBasicInfoDTOs<ChannelEnum>(1);
                    break;
                case 4:
                    basicInfoList = await GetBasicInfoDTOs<OrderTypeEnum>(1);
                    break;
                case 5:
                    basicInfoList = await GetBasicInfoDTOs<TechLevelEnum>();
                    break;
                case 6:
                    basicInfoList.Add(new BasicInfoDTO { Key = "1", Value = "回复点评" });
                    basicInfoList.Add(new BasicInfoDTO { Key = "2", Value = "客户追评" });
                    basicInfoList.Add(new BasicInfoDTO { Key = "3", Value = "回复追评" });

                    break;
            }
            return basicInfoList;
        }

        public async Task<ApiPagedResultData<GetOrderCommentForReplyDTO>> GetOrderCommentForReplyList(GetOrderCommentForReplyRequest request)
        {
            if (request.CreateTime != null && request.CreateTime.Any())
            {
                request.CreateStartTime = Convert.ToDateTime(request.CreateTime[0]);
                request.CreateEndTime = Convert.ToDateTime(request.CreateTime[1]); ;
            }

            var result = await orderCommentRepository.GetOrderCommentForReplyList(request);

            var vo = mapper.Map<ApiPagedResultData<GetOrderCommentForReplyDTO>>(result);
            if (vo.Items != null && vo.Items.Any())
            {
                #region 获取评论图片
                var commentImageList = await orderCommentRepository.GetCommentImages(new GetCommentImageRequest
                {
                    CommentIds = vo.Items.Select(r => r.Id).Distinct().ToList(),
                    RelationType = 2
                });

                var commentImageDic = new Dictionary<long, List<string>>();
                if (commentImageList.Any())
                {
                    foreach (var item in commentImageList)
                    {
                        if (commentImageDic.ContainsKey(item.CommentId))
                        {

                            commentImageDic[item.CommentId].Add(GetImageUrl(item.ImageUrl));
                        }
                        else
                        {
                            commentImageDic[item.CommentId] = new List<string>();
                            commentImageDic[item.CommentId].Add(GetImageUrl(item.ImageUrl));
                        }
                    }
                }
                #endregion

                foreach (var item in vo.Items)
                {
                    item.ChannelStr = ((ChannelEnum)item.Channel).GetEnumDescription();
                    item.CheckStatusStr = ((CheckStatusEnum)item.CheckStatus).GetEnumDescription();
                    item.OrderTypeStr = ((OrderTypeEnum)item.OrderType).GetEnumDescription();
                    item.ReplyTypeStr = ((CommentTypeEnum)item.ReplyType).GetEnumDescription();

                    item.IsCheck = item.CheckStatus == 0 ? false : true;
                    if (commentImageDic.ContainsKey(item.Id))
                    {
                        item.ImageList = commentImageDic[item.Id];
                    }

                }

            }
            return vo;
        }

        /// <summary>
        /// 获取商品评论数量
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<List<ProductCommentTotalDTO>> GetProductCommentTotal(GetProductCommentTotal request)
        {
            var response = await orderCommentRepository.GetProductCommentTotal(request);
            return mapper.Map<List<ProductCommentTotalDTO>>(response);
        }

        /// <summary>
        /// 加载评价晒单视图
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<OrderCommentResponse>> OrderComment(OrderCommentRequest request)
        {
            var result = Result.Failed<OrderCommentResponse>();
            try
            {
                var response = new OrderCommentResponse();
                var orderResponse = await orderClient.GetOrderDetail(new GetOrderDetailClientRequest() { OrderNo = request.OrderNo, UserId = request.UserId });
                var userInfo = await userClient.GetUserInfo(new GetUserInfoClientRequest() { UserId = request.UserId });
                if (orderResponse.Data != null && userInfo.Data != null)
                {
                    var shopInfo = await shopManageClient.GetShopSimpleInfoAsync(new GetShopSimpleInfoClientRequest() { ShopId = orderResponse.Data.ShopId });
                    if (shopInfo.IsNotNullSuccess())
                    {
                        var Shop = new OrderCommentDetailShopFormDTO()
                        {
                            ShopId = shopInfo.Data.Id,
                            ShopImageUrl = shopInfo.Data.ShopImageUrl,
                            ShopName = shopInfo.Data.SimpleName
                        };
                        response.ShopName = orderResponse.Data.ShopName;
                        response.Shop = Shop;
                    }
                    response.CarName = orderResponse.Data.DisplayVehicleName;
                    //遍历商品列表
                    List<OrderCommentDetailProductFormDTO> Products = new List<OrderCommentDetailProductFormDTO>();
                    foreach (var item in orderResponse.Data.Products)
                    {
                        if (item.PackageOrProduct != null)
                        {

                            if (item.PackageOrProduct.ProductAttribute == 1)
                            {
                                foreach (var packageProduct in item.PackageProducts)
                                {
                                    var orderCommentDetailProductFormDTO = new OrderCommentDetailProductFormDTO();
                                    orderCommentDetailProductFormDTO.ProductDisplayName = packageProduct.DisplayName;
                                    orderCommentDetailProductFormDTO.ProductImageUrl = packageProduct.ImageUrl;
                                    orderCommentDetailProductFormDTO.OrderProductId = packageProduct.Id;
                                    Products.Add(orderCommentDetailProductFormDTO);
                                }
                            }
                            else
                            {
                                var orderCommentDetailProductFormDTO = new OrderCommentDetailProductFormDTO();
                                orderCommentDetailProductFormDTO.ProductDisplayName = item.PackageOrProduct.DisplayName;
                                orderCommentDetailProductFormDTO.ProductImageUrl = item.PackageOrProduct.ImageUrl;
                                orderCommentDetailProductFormDTO.OrderProductId = item.PackageOrProduct.Id;
                                Products.Add(orderCommentDetailProductFormDTO);
                            }
                        }
                    }
                    foreach (var item in orderResponse.Data.Services)
                    {
                        OrderCommentDetailProductFormDTO orderCommentDetailProductFormDTO = new OrderCommentDetailProductFormDTO()
                        {
                            ProductDisplayName = item.DisplayName,
                            ProductImageUrl = item.ImageUrl,
                            OrderProductId = item.Id
                        };
                        Products.Add(orderCommentDetailProductFormDTO);
                    }
                    //评价奖励
                    var ruleInfo = await commentRewardRuleConfigRepository.GetCommentRewardRuleInfoAsync(userInfo.Data.MemberGrade, 3);
                    if (ruleInfo != null)
                    {
                        response.RewardRuleName = ruleInfo.Name;
                    }
                    response.Products = Products;
                    result = Result.Success(response, "加载评价成功");
                }
            }
            catch (Exception ex)
            {

                logger.Error($"OrderComment", ex);
                result = Result.Exception<OrderCommentResponse>("加载评价失败");
            }
            return result;
        }

        public async Task<int> CheckOrderReply(CheckOrderCommentRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.UpdateBy))
            {
                request.UpdateBy = defaultUser;
            }
            await orderCommentRepository.CheckOrderReply(request);
            return 1;

        }

        /// <summary>
        /// 获取点评标签
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<List<GetCommentLabelsResponse>> GetCommentLabels(GetCommentLabelsRequest request)
        {
            var conditon = "where is_deleted = 0 AND comment_detail_type = @CommentDetailType AND score= @Score";
            var paras = new
            {
                CommentDetailType = request.CommentDetailType,
                Score = request.Score
            };
            var list = await commentLabelConfigRepository.GetListAsync(conditon.ToString(), paras);
            return mapper.Map<List<GetCommentLabelsResponse>>(list);
        }

        /// <summary>
        /// 提交评价晒单
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult> SubmitOrderComment(SubmitOrderCommentRequest request)
        {
            bool autoCheck = Convert.ToInt32(Configuration["SwitchConfig:CommentAutoCheck"]) > 0; //自动审核 
            var result = Result.Failed();
            try
            {
                var comments = await orderCommentRepository.GetListAsync("where order_no=@OrderNo", request);
                if (comments != null && comments.Any())
                {
                    throw new CustomException("该订单已评价无需重复提交");
                }

                long commentId = 0;
                var orderCommentResponse = new OrderCommentResponse();
                var orderResponse = orderClient.GetOrderDetail(new GetOrderDetailClientRequest()
                { OrderNo = request.OrderNo, UserId = request.UserId });
                var userInfo = userClient.GetUserInfo(new GetUserInfoClientRequest() { UserId = request.UserId });
                var carInfo =
                    orderClient.GetCarByOrderNo(new GetCarByOrderNoClientRequest() { OrderNo = request.OrderNo });
                var shopInfo = shopManageClient.GetShopSimpleInfoAsync(new GetShopSimpleInfoClientRequest()
                { ShopId = request.Shop.ShopId });

                var labelIds = new List<long>();
                if (request.Shop != null && request.Shop.SelectedLabelIds != null &&
                    request.Shop.SelectedLabelIds.Any())
                {
                    labelIds.AddRange(request.Shop.SelectedLabelIds);
                }

                if (request.Products != null)
                {
                    foreach (var item in request.Products)
                    {
                        labelIds.AddRange(item.SelectedLabelIds);
                    }
                }

                var labelList = commentLabelConfigRepository.GetCommentLabelListByIds(labelIds);

                await Task.WhenAll(orderResponse, userInfo, carInfo, shopInfo, labelList);
                var carResult = carInfo.Result.Data;

                if (orderResponse.Result.Data != null && userInfo.Result.Data != null && shopInfo.Result.Data != null &&
                    labelList.Result != null)
                {
                    DateTime dt = DateTime.Now;
                    //主表
                    var commentDO = new CommentDO();
                    commentDO.OrderNo = request.OrderNo;
                    commentDO.Channel = orderResponse.Result.Data.OrderChannel;
                    commentDO.OrderType = orderResponse.Result.Data.OrderType;
                    commentDO.UserId = request.UserId;
                    commentDO.UserNickName = userInfo.Result.Data.NickName;
                    commentDO.HeadUrl = userInfo.Result.Data.HeadUrl;
                    commentDO.CarId = carResult?.CarId ?? Guid.Empty.ToString();
                    commentDO.Vehicle = carResult?.Vehicle ?? string.Empty;
                    commentDO.ShopId = orderResponse.Result.Data.ShopId;
                    commentDO.ShopName = orderResponse.Result.Data.ShopName;
                    commentDO.IsAnonymous = Convert.ToSByte(request.IsAnonymous);
                    commentDO.Content = request.Connent;
                    commentDO.CreateBy = request.CreateBy;
                    commentDO.CreateTime = dt;

                    //门店评论数据
                    var commentDetailShopDO = new CommentDetailShopDO()
                    {
                        ShopId = orderResponse.Result.Data.ShopId,
                        ShopImageUrl = shopInfo.Result.Data.ShopImageUrl,
                        Score = request.Shop.Score,
                        CreateBy = userInfo.Result.Data.UserName,
                        CreateTime = dt
                    };
                    //评论图片
                    List<CommentImageDO> CommentImageList = new List<CommentImageDO>();
                    request.ImageUrls.ForEach(o => CommentImageList.Add(new CommentImageDO()
                    { ImageUrl = o, RelationType = 1, CreateBy = userInfo.Result.Data.UserName, CreateTime = dt }));


                    //using (TransactionScope ts = new TransactionScope())
                    {
                        //添加主评论
                        commentId = await orderCommentRepository.InsertAsync(commentDO);

                        //添加门店评论
                        commentDetailShopDO.CommentId = commentId;
                        var commentShopId = await commentDetailShopRepository.InsertAsync(commentDetailShopDO);
                        if (commentShopId > 0)
                        {
                            //门店评论标签
                            List<CommentLabelSelectedDO> labelShopList = new List<CommentLabelSelectedDO>();
                            //request.Shop.SelectedLabelIds.ForEach(o => labelShopList.Add(new CommentLabelSelectedDO() { LabelId = o, CreateBy = userInfo.Result.Data.UserName, CreateTime = dt }));
                            foreach (var item in request.Shop.SelectedLabelIds)
                            {
                                foreach (var labelItem in labelList.Result)
                                {
                                    if (item == labelItem.Id)
                                    {
                                        var labelSelect = new CommentLabelSelectedDO()
                                        {
                                            CommentId = commentId,
                                            CommentDetailId = commentShopId,
                                            LabelId = labelItem.Id,
                                            LabelName = labelItem.LabelName,
                                            CommentDetailType = 2,
                                            CreateBy = userInfo.Result.Data.UserName,
                                            CreateTime = dt
                                        };

                                        labelShopList.Add(labelSelect);
                                    }
                                }
                            }

                            //门店评论标签
                            labelShopList.ForEach(o => commentLabelSelectedRepository.InsertAsync(o));
                        }

                        //添加评论图片
                        CommentImageList.ForEach(o => o.CommentId = commentId);
                        CommentImageList.ForEach(o => commentImageRepository.InsertAsync(o));



                        //添加商品评论
                        //List<CommentDetailProductDO> Products = new List<CommentDetailProductDO>();
                        foreach (var item in orderResponse.Result.Data.Products)
                        {
                            if (item.PackageOrProduct != null)
                            {
                                int productCommentId = 0;

                                if (item.PackageOrProduct.ProductAttribute == 1)
                                {
                                    foreach (var packageProduct in item.PackageProducts)
                                    {
                                        foreach (var procutItem in request.Products)
                                        {
                                            if (packageProduct.Id == procutItem.OrderProductId)
                                            {
                                                var commentDetailProductDO = new CommentDetailProductDO();
                                                commentDetailProductDO.CommentId = commentId;
                                                commentDetailProductDO.OrderProductId = packageProduct.Id;
                                                commentDetailProductDO.ProductId = packageProduct.ProductId;
                                                commentDetailProductDO.ProductDisplayName = packageProduct.DisplayName;
                                                commentDetailProductDO.ProductImageUrl = packageProduct.ImageUrl;
                                                commentDetailProductDO.Price = packageProduct.Price;
                                                commentDetailProductDO.Number = packageProduct.Number;
                                                commentDetailProductDO.Unit = packageProduct.Unit;
                                                commentDetailProductDO.Score = procutItem.Score;
                                                commentDetailProductDO.IsAnonymous =
                                                    Convert.ToSByte(request.IsAnonymous);
                                                commentDetailProductDO.CreateBy = userInfo.Result.Data.UserName;
                                                commentDetailProductDO.CreateTime = dt;

                                                //添加商品评论
                                                productCommentId =
                                                    await commentDetailProductRepository.InsertAsync(
                                                        commentDetailProductDO);
                                                if (productCommentId > 0)
                                                {
                                                    foreach (var itemLabel in procutItem.SelectedLabelIds)
                                                    {
                                                        foreach (var labelItem in labelList.Result)
                                                        {
                                                            if (itemLabel == labelItem.Id)
                                                            {
                                                                var productLabelDO = new CommentLabelSelectedDO()
                                                                {

                                                                    LabelId = itemLabel,
                                                                    CommentId = commentId,
                                                                    CommentDetailId = productCommentId,
                                                                    CommentDetailType = 3,
                                                                    LabelName = labelItem.LabelName,
                                                                    CreateBy = userInfo.Result.Data.UserName,
                                                                    CreateTime = dt,
                                                                };
                                                                await commentLabelSelectedRepository.InsertAsync(
                                                                    productLabelDO);
                                                            }
                                                        }

                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    foreach (var procutItem in request.Products)
                                    {
                                        if (item.PackageOrProduct.Id == procutItem.OrderProductId)
                                        {
                                            var commentDetailProductDO = new CommentDetailProductDO();
                                            commentDetailProductDO.CommentId = commentId;
                                            commentDetailProductDO.OrderProductId = item.PackageOrProduct.Id;
                                            commentDetailProductDO.ProductId = item.PackageOrProduct.ProductId;
                                            commentDetailProductDO.ProductDisplayName =
                                                item.PackageOrProduct.DisplayName;
                                            commentDetailProductDO.ProductImageUrl = item.PackageOrProduct.ImageUrl;
                                            commentDetailProductDO.Price = item.PackageOrProduct.Price;
                                            commentDetailProductDO.Number = item.PackageOrProduct.Number;
                                            commentDetailProductDO.Unit = item.PackageOrProduct.Unit;
                                            commentDetailProductDO.Score = procutItem.Score;
                                            commentDetailProductDO.IsAnonymous = Convert.ToSByte(request.IsAnonymous);
                                            commentDetailProductDO.CreateBy = userInfo.Result.Data.UserName;
                                            commentDetailProductDO.CreateTime = dt;

                                            //添加商品评论
                                            productCommentId =
                                                await commentDetailProductRepository
                                                    .InsertAsync(commentDetailProductDO);
                                            if (productCommentId > 0)
                                            {
                                                foreach (var itemLabel in procutItem.SelectedLabelIds)
                                                {
                                                    foreach (var labelItem in labelList.Result)
                                                    {
                                                        if (itemLabel == labelItem.Id)
                                                        {
                                                            var productLabelDO = new CommentLabelSelectedDO()
                                                            {

                                                                LabelId = itemLabel,
                                                                CommentId = commentId,
                                                                CommentDetailId = productCommentId,
                                                                CommentDetailType = 3,
                                                                LabelName = labelItem.LabelName,
                                                                CreateBy = userInfo.Result.Data.UserName,
                                                                CreateTime = dt,
                                                            };
                                                            await commentLabelSelectedRepository.InsertAsync(
                                                                productLabelDO);
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }

                        if (orderResponse.Result.Data.Services != null && orderResponse.Result.Data.Services.Any())
                        {
                            int productCommentId = 0;
                            foreach (var packageProduct in orderResponse.Result.Data.Services)
                            {
                                foreach (var procutItem in request.Products)
                                {
                                    if (packageProduct.Id == procutItem.OrderProductId)
                                    {
                                        var commentDetailProductDO = new CommentDetailProductDO();
                                        commentDetailProductDO.CommentId = commentId;
                                        commentDetailProductDO.OrderProductId = packageProduct.Id;
                                        commentDetailProductDO.ProductId = packageProduct.ProductId;
                                        commentDetailProductDO.ProductDisplayName = packageProduct.DisplayName;
                                        commentDetailProductDO.ProductImageUrl = packageProduct.ImageUrl;
                                        commentDetailProductDO.Price = packageProduct.Price;
                                        commentDetailProductDO.Number = packageProduct.Number;
                                        commentDetailProductDO.Unit = packageProduct.Unit;
                                        commentDetailProductDO.Score = procutItem.Score;
                                        commentDetailProductDO.IsAnonymous = Convert.ToSByte(request.IsAnonymous);
                                        commentDetailProductDO.CreateBy = userInfo.Result.Data.UserName;
                                        commentDetailProductDO.CreateTime = dt;

                                        //添加商品评论
                                        productCommentId =
                                            await commentDetailProductRepository.InsertAsync(commentDetailProductDO);
                                        if (productCommentId > 0)
                                        {
                                            foreach (var itemLabel in procutItem.SelectedLabelIds)
                                            {
                                                foreach (var labelItem in labelList.Result)
                                                {
                                                    if (itemLabel == labelItem.Id)
                                                    {
                                                        var productLabelDO = new CommentLabelSelectedDO()
                                                        {

                                                            LabelId = itemLabel,
                                                            CommentId = commentId,
                                                            CommentDetailId = productCommentId,
                                                            CommentDetailType = 3,
                                                            LabelName = labelItem.LabelName,
                                                            CreateBy = userInfo.Result.Data.UserName,
                                                            CreateTime = dt,
                                                        };
                                                        await commentLabelSelectedRepository
                                                            .InsertAsync(productLabelDO);
                                                    }
                                                }
                                            }

                                        }
                                    }
                                }
                            }
                        }


                        //ts.Complete();
                    }

                    if (autoCheck) //自动审核
                    {
                        await CheckOrderComment(new CheckOrderCommentRequest()
                        {
                            CommentId = commentId,
                            CheckComment = "自动审核",
                            CheckStatus = 1
                        });
                    }

                    await orderClient.UpdateOrderStatus(new UpdateOrderStatusClientRequest()
                    {
                        OrderNos = new List<string>() { request.OrderNo },
                        UpdateBy = request.CreateBy,
                        UpdateStatusType = UpdateOrderStatusTypeEnum.CustomerComment
                    });

                    result = Result.Success("提交成功");
                }
            }
            catch (CustomException)
            {
                throw;
            }
            catch (Exception ex)
            {
                logger.Error($"OrderComment", ex);
                result = Result.Exception("提交评价失败");
            }

            return result;
        }

        /// <summary>
        /// 添加门店评论
        /// </summary>
        /// <param name="commentDetailShopDO"></param>
        /// <returns></returns>
        public async Task<int> AddShopCommentAsync(CommentDetailShopDO commentDetailShopDO)
        {
            return await commentDetailShopRepository.InsertAsync(commentDetailShopDO);
        }
        /// <summary>
        /// 查询我的评价列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiPagedResultData<GetMyCommentListResponse>> GetMyCommentList(GetMyCommentListRequest request)
        {
            var response = await orderCommentRepository.GetMyCommentList(request);
            var result = mapper.Map<ApiPagedResultData<GetMyCommentListResponse>>(response);
            if (result != null && result.Items.Any())
            {
                List<long> commentId = result.Items.ToList().Select(o => o.CommentId).ToList();
                var imgList = await GetCommentImgList(commentId, 1);
                var productList = await GetCommentDetailProductList(commentId);
                foreach (var item in result.Items)
                {
                    var carInfo = await vehicleClient.GetUserVehicleByCarIdAsync(new UserVehicleByCarIdClientRequest() { UserId = item.UserId, CarId = item.CarId });
                    if (carInfo.IsNotNullSuccess() && carInfo.Data != null)
                    {
                        item.CarName = carInfo.Data.Vehicle;
                    }
                    //评论图片
                    item.ImageUrls = imgList.Where(o => o.CommentId == item.CommentId)
                        .Select(p => GetImageUrl(p.ImageUrl)).ToList();
                    //商品
                    var commentProducts = productList.Where(o => o.CommentId == item.CommentId).ToList();
                    item.ProductNum = commentProducts.Count;
                    List<CommentProductOrPackageSimpleDTO> commentProductList = new List<CommentProductOrPackageSimpleDTO>();
                    commentProducts.ForEach(o => commentProductList.Add(
                        new CommentProductOrPackageSimpleDTO()
                        {
                            ProductId = o.ProductId,
                            DisplayName = o.ProductDisplayName,
                            ImageUrl = o.ProductImageUrl,
                            Price = o.Price,
                            Number = o.Number,
                            Unit = o.Unit
                        })
                    );
                    item.ProductOrPackages = commentProductList;

                    //回评
                    List<CommentListReplyInfoDTO> ReplyInfos = new List<CommentListReplyInfoDTO>();
                    var replyList = await orderCommentRepository.GetCommentReplyListByCommentId(item.CommentId);
                    foreach (var replyItem in replyList)
                    {
                        var replyInfo = new CommentListReplyInfoDTO();
                        replyInfo.CommentId = item.CommentId;
                        replyInfo.CommentType = (CommentTypeEnum)Enum.Parse(typeof(CommentTypeEnum), replyItem.ReplyType.ToString());
                        replyInfo.ReplyContent = replyItem.Content;
                        replyInfo.DisplayTitle = ((CommentTypeEnum)replyItem.ReplyType).GetEnumDescription();
                        replyInfo.CreateTime = replyItem.CreateTime;

                        if (replyItem.ReplyType == 2)
                        {
                            var list = await commentImageRepository.GetCommentImgListByCommentId(replyItem.Id, replyItem.ReplyType);
                            replyInfo.ImgUrl = list.Select(p => GetImageUrl(p.ImageUrl)).ToList();
                        }
                        ReplyInfos.Add(replyInfo);
                    }
                    item.ReplyInfos = ReplyInfos;

                    if (replyList.Where(o => o.ReplyType == 2).Count() == 0)
                    {
                        item.ReplyButtons = new CommentReplyButtonDTO()
                        {
                            ReplyToCommentId = item.CommentId,
                            Name = "追加评论"
                        };
                    }

                }
            }
            return result;
        }
        /// <summary>
        /// 根据commentIds获取评论图片列表
        /// </summary>
        /// <param name="commentIds"></param>
        /// <param name="relationType">1 客户初评 2回评</param>
        /// <returns></returns>
        public async Task<List<CommentImageDO>> GetCommentImgList(List<long> commentIds, int relationType)
        {
            var condition = "where is_deleted = 0 AND relation_type = @RelationType AND comment_id in @CommentIds";
            var paras = new
            {
                CommentIds = commentIds,
                RelationType = relationType
            };
            var list = await commentImageRepository.GetListAsync(condition, paras);
            return list.ToList();
        }
        /// <summary>
        /// 获取评论商品列表
        /// </summary>
        /// <param name="commentIds"></param>
        /// <returns></returns>
        public async Task<List<CommentDetailProductDO>> GetCommentDetailProductList(List<long> commentIds)
        {
            var condition = "where is_deleted = 0 AND comment_id in @CommentIds";
            var paras = new
            {
                CommentIds = commentIds
            };
            var list = await commentDetailProductRepository.GetListAsync(condition, paras);
            return list.ToList();
        }

        /// <summary>
        /// 获取评论回评列表
        /// </summary>
        /// <param name="commentIds"></param>
        /// <returns></returns>
        public async Task<List<CommentDetailProductDO>> GetCommentReplyList(List<long> commentIds)
        {
            var condition = "where is_deleted = 0 AND comment_id in @CommentIds";
            var paras = new
            {
                CommentIds = commentIds
            };
            var list = await commentDetailProductRepository.GetListAsync(condition, paras);
            return list.ToList();
        }

        /// <summary>
        /// 点赞喜欢点评
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> LikeComment(LikeCommentRequest request)
        {
            var commentInfo = await orderCommentRepository.GetAsync(request.CommentId);
            var number = 0;
            if (commentInfo != null)
            {
                commentInfo.LikeNum += 1;
                number = await orderCommentRepository.UpdateAsync(commentInfo, new[] { "LikeNum" });
            }
            return number > 0;
        }
        /// <summary>
        /// 获取商品好评率、评论数量
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<BaseCommentNumAndApplauseRateResponse> GetProductCommentNumAndApplauseRate(BaseGetProductCommentRequest request)
        {
            int total = await commentDetailProductRepository.GetProductCommentTotal(request.ProductId, 0);
            //decimal googTotal = await commentDetailProductRepository.GetProductCommentTotal(request.ProductId, 5);
            BaseCommentNumAndApplauseRateResponse result = new BaseCommentNumAndApplauseRateResponse()
            {
                CommentNum = total
            };
            return result;
        }

        /// <summary>
        /// 获取商品评价标签列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiPagedResultData<BaseCommentLabelListResponse>> GetProductCommentLabelList(GetProductCommentListRequest request)
        {
            var data = await commentLabelSelectedRepository.GetProductCommentLabelList(request);
            var result = mapper.Map<ApiPagedResultData<BaseCommentLabelListResponse>>(data);
            return result;
        }
        /// <summary>
        /// 查询商品评价列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiPagedResultData<BaseCommentListResponse>> GetProductCommentList(GetProductCommentListRequest request)
        {
            var data = await commentDetailProductRepository.GetProductCommentList(request);
            var result = mapper.Map<ApiPagedResultData<BaseCommentListResponse>>(data);
            foreach (var item in result.Items)
            {
                var userInfo = await userClient.GetUserInfo(new GetUserInfoClientRequest() { UserId = item.UserId });
                //var carInfo = await vehicleClient.GetUserVehicleByCarIdAsync(new UserVehicleByCarIdClientRequest() { UserId = item.UserId, CarId = item.CarId });
                List<long> commentId = new List<long>() { item.CommentId };
                var imgList = await GetCommentImgList(commentId, 1);
                // if (carInfo.IsNotNullSuccess() && carInfo.Data != null)
                // {
                //     item.CarName = carInfo.Data.Vehicle;
                // }
                if (userInfo.IsNotNullSuccess() && userInfo.Data != null)
                {
                    item.MemberGrade = userInfo.Data.MemberGrade;
                    item.MemberLevelDisplayName = userInfo.Data.MemberLevel;
                }
                if (imgList != null && imgList.Any())
                {
                    List<string> ImageUrls = new List<string>();
                    foreach (var imgItem in imgList)
                    {
                        string imageUrl = GetImageUrl(imgItem.ImageUrl);
                        ImageUrls.Add(imageUrl);
                    }
                    item.ImageUrls = ImageUrls;
                }
                //评论回复集合
                List<CommentListReplyInfoDTO> ReplyInfos = new List<CommentListReplyInfoDTO>();
                var replyList = await orderCommentRepository.GetCommentReplyListByCommentId(item.CommentId);
                foreach (var replyItem in replyList)
                {
                    var replyInfo = new CommentListReplyInfoDTO();
                    replyInfo.CommentId = item.CommentId;
                    replyInfo.CommentType = (CommentTypeEnum)Enum.Parse(typeof(CommentTypeEnum), replyItem.ReplyType.ToString());
                    replyInfo.ReplyContent = replyItem.Content;
                    replyInfo.DisplayTitle = ((CommentTypeEnum)replyItem.ReplyType).GetEnumDescription();
                    ReplyInfos.Add(replyInfo);
                }
                item.ReplyInfos = ReplyInfos;
            }
            return result;
        }

        /// <summary>
        /// 获取门店好评率、评论数量
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<BaseCommentNumAndApplauseRateResponse> GetShopCommentNumAndApplauseRate(
            BaseGetShopCommentRequest request)
        {
            var totalTask = commentDetailShopRepository.GetShopCommentTotal(request.ShopId, 0);
            var goodTotalTask = commentDetailShopRepository.GetShopCommentTotal(request.ShopId, 4);
            await Task.WhenAll(totalTask, goodTotalTask);
            int total = totalTask.Result;
            var goodTotal = goodTotalTask.Result;
            if (total == 0)
            {
                return new BaseCommentNumAndApplauseRateResponse();
            }
            decimal goodRate = (decimal)goodTotal / total;
            BaseCommentNumAndApplauseRateResponse result = new BaseCommentNumAndApplauseRateResponse()
            {
                CommentNum = total,
                ApplauseRate = Math.Round(goodRate, 2, MidpointRounding.AwayFromZero) * 100
            };
            return result;
        }

        /// <summary>
        /// 查询门店评价列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiPagedResultData<BaseCommentListResponse>> GetShopCommentList(
            GetShopCommentListRequest request)
        {
            var data = await commentDetailShopRepository.GetShopCommentList(request);
            var commentIds = data.Items.Select(_ => _.CommentId).ToList();
            List<CommentImageDO> images = new List<CommentImageDO>();
            if (commentIds.Any())
            {
                images = await commentImageRepository.GetCommentImgList(commentIds);
            }

            var result = mapper.Map<ApiPagedResultData<BaseCommentListResponse>>(data);
            foreach (var item in result.Items)
            {
                if (item.IsAnonymous)
                {
                    item.NickName = "匿名用户";
                    item.HeadUrl = $"{Configuration["BaseImageUrl"]}mini-app-main/mine.png";
                }

                var defaultImage = images.Where(_ => _.CommentId == item.CommentId).ToList();
                item.ImageUrls = defaultImage.Where(_ => _.RelationType == 1).Select(_ => GetImageUrl(_.ImageUrl))
                    .ToList();
                //回评
                List<CommentListReplyInfoDTO> ReplyInfos = new List<CommentListReplyInfoDTO>();
                var replyList = await orderCommentRepository.GetCommentReplyListByCommentId(item.CommentId);
                foreach (var replyItem in replyList)
                {
                    var replyInfo = new CommentListReplyInfoDTO();
                    replyInfo.CommentId = item.CommentId;
                    replyInfo.CommentType =
                        (CommentTypeEnum)Enum.Parse(typeof(CommentTypeEnum), replyItem.ReplyType.ToString());
                    replyInfo.ReplyContent = replyItem.Content;
                    replyInfo.DisplayTitle = ((CommentTypeEnum)replyItem.ReplyType).GetEnumDescription();
                    replyInfo.CreateTime = replyItem.CreateTime;

                    if (replyItem.ReplyType == 2)
                    {
                        //var list = await commentImageRepository.GetCommentImgListByCommentId(replyItem.Id, replyItem.ReplyType);
                        replyInfo.ImgUrl = defaultImage.Where(_ => _.RelationType == 2)
                            .Select(p => GetImageUrl(p.ImageUrl)).ToList();
                    }

                    ReplyInfos.Add(replyInfo);
                }

                item.ReplyInfos = ReplyInfos;
            }

            return result;
        }

        /// <summary>
        /// 获取门店评价标签列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiPagedResultData<BaseCommentLabelListResponse>> GetShopCommentLabelList(
            GetShopCommentListRequest request)
        {
            List<BaseCommentLabelListResponse> data = new List<BaseCommentLabelListResponse>();
            var labelEnumTask = commentLabelConfigRepository.GetAllCommentLabelList();
            var shopLabelTask = commentLabelSelectedRepository.GetShopLabel(request.ShopId);
            await Task.WhenAll(labelEnumTask, shopLabelTask);
            var labelEnum = labelEnumTask.Result ?? new List<CommentLabelConfigDO>();
            var shopLabel = shopLabelTask.Result ?? new List<ShopLabelStatisticsDo>();
            labelEnum = labelEnum.Where(_ => _.CommentDetailType == 2).ToList();
            shopLabel.ForEach(_ =>
            {
                var defaultLabel = labelEnum.FirstOrDefault(t => t.Id == _.LabelId);
                if (defaultLabel != null)
                {
                    data.Add(new BaseCommentLabelListResponse()
                    {
                        Id = defaultLabel.Id,
                        LabelName = defaultLabel.LabelName,
                        Num = _.Count
                    });
                }
            });

            return new ApiPagedResultData<BaseCommentLabelListResponse>()
            {
                Items = data,
                TotalItems = data.Count
            };
        }

        /// <summary>
        /// 加载追加点评视图
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<AppendCommentResponse> AppendComment(AppendCommentRequest request)
        {
            if (request.CommentId <= 0)
            {
                var userUnReplyComments = await orderCommentRepository.GetListAsync("where order_no=@OrderNo", request);
                if (userUnReplyComments == null || !userUnReplyComments.Any() || userUnReplyComments.Count() > 1)
                {
                    throw new CustomException("评论信息异常");
                }
                var userUnReplyComment = userUnReplyComments.FirstOrDefault();
                if (userUnReplyComment.UserReplyType == 1)
                {
                    throw new CustomException("该评论已追评");
                }
                request.CommentId = userUnReplyComment.Id;
            }
            var commentInfo = await orderCommentRepository.GetAsync(request.CommentId);
            var response = new AppendCommentResponse();
            if (commentInfo != null)
            {
                response.CommentId = commentInfo.Id;
                response.CommentContent = commentInfo.Content;
                response.CreateTime = commentInfo.CreateTime.ToString();
                response.ShopName = commentInfo.ShopName;
                response.LikeNum = commentInfo.LikeNum;
                var carInfo = await vehicleClient.GetUserVehicleByCarIdAsync(new UserVehicleByCarIdClientRequest() { UserId = commentInfo.UserId, CarId = commentInfo.CarId });
                if (carInfo.IsNotNullSuccess() && carInfo.Data != null)
                {
                    response.CarName = carInfo.Data.Vehicle;
                }
                //评论图片
                List<long> commentId = new List<long>() { request.CommentId };
                var imgList = await GetCommentImgList(commentId, 1);
                List<string> ImageUrls = new List<string>();
                foreach (var imgItem in imgList)
                {
                    string imageUrl = GetImageUrl(imgItem.ImageUrl);
                    ImageUrls.Add(imageUrl);
                }
                response.Imgs = ImageUrls;
                //商品列表
                List<OrderCommentDetailProductFormDTO> Products = new List<OrderCommentDetailProductFormDTO>();
                var productList = await GetCommentDetailProductList(commentId);
                productList.ForEach(o => Products.Add(new OrderCommentDetailProductFormDTO() { OrderProductId = o.OrderProductId, ProductDisplayName = o.ProductDisplayName, ProductImageUrl = o.ProductImageUrl }));
                response.Products = Products;
            }
            return response;
        }
        /// <summary>
        /// 提交追评
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> SubmitAppendComment(SubmitAppendCommentRequest request)
        {
            bool autoCheck = Convert.ToInt32(Configuration["SwitchConfig:CommentAutoCheck"]) > 0; //自动审核 
            var row = 0;
            var commentInfo = await orderCommentRepository.GetAsync(request.CommentId);
            if (commentInfo != null)
            {
                DateTime dt = DateTime.Now;
                var commentReplyDO = new CommentReplyDO()
                {
                    ReplyId = request.CommentId,
                    ReplyPartType = 0,
                    ReplyType = 2,
                    ShopId = commentInfo.ShopId,
                    ShopName = commentInfo.ShopName,
                    Channel = 1,
                    OrderType = commentInfo.OrderType,
                    OrderNo = commentInfo.OrderNo,
                    CheckStatus = 0,
                    CheckComment = request.Connent,
                    IsDeleted = 0,
                    CreateBy = request.CreateBy,
                    CreateTime = dt
                };
                using (TransactionScope ts = new TransactionScope())
                {

                    row = await commentReplyRepository.InsertAsync(commentReplyDO);
                    if (row > 0)
                    {
                        //修改主表客户追评论
                        commentInfo.UserReplyType = 1;
                        await orderCommentRepository.UpdateAsync(commentInfo, new[] { "UserReplyType" });
                        if (request.ImageUrls != null && request.ImageUrls.Any())
                        {
                            //评论图片
                            List<CommentImageDO> CommentImageList = new List<CommentImageDO>();
                            request.ImageUrls.ForEach(o => CommentImageList.Add(new CommentImageDO() { CommentId = row, ImageUrl = o, RelationType = 2, CreateBy = request.CreateBy, CreateTime = dt }));
                            //添加评论图片
                            CommentImageList.ForEach(o => commentImageRepository.InsertAsync(o));
                        }
                    }
                    ts.Complete();
                }

                if (row > 0)
                {
                    if (autoCheck)//自动审核
                    {
                        await CheckOrderReply(new CheckOrderCommentRequest()
                        {
                            CommentId = row,
                            CheckComment = "自动审核",
                            CheckStatus = 1
                        });
                    }
                }
            }
            return row > 0;
        }

        /// <summary>
        /// 获取门店评分
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<List<ShopScoreVo>> GetShopScore(ShopScoreRequest request)
        {
            List<ShopScoreVo> data = new List<ShopScoreVo>();
            var shopIds = request.ShopIdList;
            if (shopIds == null || !shopIds.Any())
            {
                return new List<ShopScoreVo>();
            }

            var result = await commentDetailShopRepository.GetShopScore(request.ShopIdList);

            shopIds.ForEach(_ =>
            {
                decimal score = 0;
                int totalNum = 0;
                var defaultScore = result.FirstOrDefault(t => t.ShopId == _);
                if (defaultScore != null)
                {
                    score = Math.Round(defaultScore.Score, 2, MidpointRounding.AwayFromZero);
                    totalNum = defaultScore.TotalNum;
                }

                data.Add(new ShopScoreVo()
                {
                    ShopId = _,
                    Score = score,
                    TotalNum = totalNum
                });
            });

            return data;
        }

        /// <summary>
        /// 每日评论统计
        /// </summary>
        /// <returns></returns>
        public async Task<ApiResult> DailyCommentStatistics()
        {
            var result = Result.Failed("执行异常");
            try
            {
                result = Result.Success("任务请求已收到并执行中...请勿重复提交");
                _ = Task.Factory.StartNew(async () =>
                  {
                      try
                      {
                          await RunDailyCommentStatistics();
                      }
                      catch (Exception ex)
                      {
                          logger.Error("RunDailyCommentStatisticsEx", ex);
                      }
                  }, TaskCreationOptions.LongRunning);
            }
            catch (Exception ex)
            {
                logger.Error("DailyCommentStatisticsEx", ex);
            }
            return result;
        }

        /// <summary>
        /// 异步执行每日评论统计
        /// </summary>
        /// <returns></returns>
        private async Task RunDailyCommentStatistics()
        {
            //商品好评率计算逻辑=(商品好评个数/总点评数)X100%，好评率精确到小数点后1位，需要四舍五入。4分以上（包含4分）确定为好评。
            //默认商品好评率100%，默认门店好评率100%，默认技师好评率100%
            //订单整体好评率=（在线订单得分总和/在线订单应得分总和）x100%
            var distinctCommentDetailShopIds = await commentDetailShopRepository.GetDistinctShopIds(DateTime.Today.AddDays(-30), DateTime.Today);
            if (distinctCommentDetailShopIds != null && distinctCommentDetailShopIds.Any())
            {
                //门店好评率计算逻辑=（门店好评个数/总点评数）X100%，好评率精确到小数点后1位，需要四舍五入。
                var pushList = new Dictionary<long, CommentStatisticsDTO>();
                int pushRate = 50;//推送频率
                for (int i = 0; i < distinctCommentDetailShopIds.Count; i++)
                {
                    var shopId = distinctCommentDetailShopIds[i];
                    int count = await commentDetailShopRepository.GetCountByShopId(shopId);
                    int goodCount = await commentDetailShopRepository.GetGoodCountByShopId(shopId);
                    if (count < 0)
                    {
                        continue;
                    }
                    decimal goodRate = (decimal.Parse(goodCount.ToString()) / count) * 100;
                    goodRate = decimal.Round(goodRate, 1, MidpointRounding.AwayFromZero);
                    pushList.TryAdd(shopId, new CommentStatisticsDTO() { Count = count, GoodCount = goodCount, GoodRate = goodRate });

                    //批量推送并清空任务列表
                    int time = i + 1;//次数
                    if (time % pushRate == 0 || time == distinctCommentDetailShopIds.Count)
                    {
                        //执行推送
                        var updateShopScoreByShopIdsRequest = new UpdateShopScoreByShopIdsRequest()
                        {
                            ShopScoreList = new List<ShopScoreDTO>()
                        };
                        var shopIds = new List<long>();
                        foreach (var item in pushList)
                        {
                            if (!shopIds.Contains(item.Key))
                            {
                                shopIds.Add(item.Key);
                            }
                        }
                        var batchGetOrderCountByShopIdRequest = new BatchGetOrderCountByShopIdRequest() { ShopIds = shopIds };
                        var batchGetOrderCountByShopIdResult = await orderClient.BatchGetOrderCountByShopId(batchGetOrderCountByShopIdRequest);
                        var orderCounts = batchGetOrderCountByShopIdResult.GetSuccessData();
                        foreach (var item in pushList)
                        {
                            var shopScoreDTO = new ShopScoreDTO()
                            {
                                ShopId = item.Key,
                                Score = item.Value.Count,
                                ApplauseRate = item.Value.GoodRate
                            };
                            if (orderCounts != null)
                            {
                                var findOrderCount = orderCounts.Find(_ => _.ShopId == item.Key);
                                if (findOrderCount != null)
                                {
                                    shopScoreDTO.TotalOrder = findOrderCount.OrderCount;
                                }
                            }
                            updateShopScoreByShopIdsRequest.ShopScoreList.Add(shopScoreDTO);
                        }
                        var updateShopScoreAsyncResult = await shopManageClient.UpdateShopScoreAsync(updateShopScoreByShopIdsRequest);
                        logger.Info($"DailyCommentStatistics time={time} UpdateShopScoreAsync updateShopScoreByShopIdsRequest={JsonConvert.SerializeObject(updateShopScoreByShopIdsRequest)} updateShopScoreAsyncResult={JsonConvert.SerializeObject(updateShopScoreAsyncResult)}");
                        pushList.Clear();
                    }
                }
            }
            var distinctCommentDetailProductIds = await commentDetailProductRepository.GetDistinctCommentDetailProductIds(DateTime.Today.AddDays(-10), DateTime.Today);
            if (distinctCommentDetailProductIds != null && distinctCommentDetailProductIds.Any())
            {
                //商品好评率计算逻辑=(商品好评个数/总点评数)X100%，好评率精确到小数点后1位，需要四舍五入。4分以上（包含4分）确定为好评。
                var pushList = new Dictionary<string, CommentStatisticsDTO>();
                int pushRate = 50;//推送频率
                for (int i = 0; i < distinctCommentDetailProductIds.Count; i++)
                {
                    var pid = distinctCommentDetailProductIds[i];
                    int count = await commentDetailProductRepository.GetCountByProductId(pid);
                    int goodCount = await commentDetailProductRepository.GetGoodCountByProductId(pid);
                    if (count < 0)
                    {
                        continue;
                    }
                    decimal goodRate = (decimal.Parse(goodCount.ToString()) / count) * 100;
                    goodRate = decimal.Round(goodRate, 1, MidpointRounding.AwayFromZero);
                    pushList.TryAdd(pid, new CommentStatisticsDTO() { Count = count, GoodCount = goodCount, GoodRate = goodRate });

                    //批量推送并清空任务列表
                    int time = i + 1;//次数
                    if (time % pushRate == 0 || time == distinctCommentDetailProductIds.Count)
                    {
                        //执行推送
                        var batchUpdateProductRequest = new BatchUpdateProductRequest()
                        {
                            UserId = "DailyCommentStatistics",
                            UpdateFilds = new List<string>() { "FavorableRate" },
                            Products = new List<ProductBaseInfoDTO>()
                        };
                        foreach (var item in pushList)
                        {
                            var productBaseInfoDTO = new ProductBaseInfoDTO()
                            {
                                ProductCode = item.Key,
                                FavorableRate = item.Value.GoodRate
                            };
                            batchUpdateProductRequest.Products.Add(productBaseInfoDTO);
                        }
                        var batchUpdateProductResult = await productManageClient.BatchUpdateProduct(batchUpdateProductRequest);
                        logger.Info($"DailyCommentStatistics time={time} BatchUpdateProduct batchUpdateProductRequest={JsonConvert.SerializeObject(batchUpdateProductRequest)} batchUpdateProductResult={JsonConvert.SerializeObject(batchUpdateProductResult)}");
                        pushList.Clear();
                    }
                }
            }
        }
    }
}
