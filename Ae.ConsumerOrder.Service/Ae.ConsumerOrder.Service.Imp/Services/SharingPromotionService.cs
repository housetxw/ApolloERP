using AutoMapper;
using ApolloErp.Log;
using ApolloErp.Web.WebApi;
using Ae.ConsumerOrder.Service.Client.Clients;
using Ae.ConsumerOrder.Service.Common.Extension;
using Ae.ConsumerOrder.Service.Core.Enums;
using Ae.ConsumerOrder.Service.Core.Interfaces;
using Ae.ConsumerOrder.Service.Core.Model;
using Ae.ConsumerOrder.Service.Core.Request.SharingPromotion;
using Ae.ConsumerOrder.Service.Core.Response.SharingPromotion;
using Ae.ConsumerOrder.Service.Dal.Repository;
using Ae.ConsumerOrder.Service.Dal.Repository.SharingPromtion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ae.ConsumerOrder.Service.Imp.Services
{
    public class SharingPromotionService : ISharingPromotionService
    {
        private readonly IMapper _mapper;
        private readonly ApolloErpLogger<SharingPromotionService> _logger;
        private readonly IOrderUserShareRepository _orderUserShareRepository;
        private readonly IOrderUserShareDetailRepository _orderUserShareDetailRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderProductRepository _orderProductRepository;
        private readonly ICouponClient _couponClient;

        public SharingPromotionService(IMapper mapper, ApolloErpLogger<SharingPromotionService> ApolloErpLogger, IOrderUserShareRepository orderUserShareRepository, IOrderUserShareDetailRepository orderUserShareDetailRepository, IOrderRepository orderRepository,
            IOrderProductRepository orderProductRepository, ICouponClient couponClient)
        {
            _mapper = mapper;
            _logger = ApolloErpLogger;
            _orderUserShareRepository = orderUserShareRepository;
            _orderUserShareDetailRepository = orderUserShareDetailRepository;
            _orderRepository = orderRepository;
            _orderProductRepository = orderProductRepository;
            _couponClient = couponClient;
        }

        public async Task<ApiResult<GetSharingCouponResponse>> GetSharingCoupon(GetSharingCouponRequest request)
        {
            var getRecommendCouponList = await _couponClient.GetRecommendCourteousCouponList(new Client.Request.Coupon.RecommendCourteousCouponListRequest()
            {
                UserId = request.UserId
            });

            var userCouponPageResByUserIdDTO = getRecommendCouponList?.Data?.Where(_ => _.Status == UserCouponStatus.Unused)?.FirstOrDefault();

            if (userCouponPageResByUserIdDTO != null)
            {
                return new ApiResult<GetSharingCouponResponse>()
                {
                    Code = ResultCode.Success,
                    Data = _mapper.Map<GetSharingCouponResponse>(userCouponPageResByUserIdDTO)
                };

            }
            var haveduserCouponPageResByUserIdDTO = getRecommendCouponList?.Data?.FirstOrDefault();
            if (haveduserCouponPageResByUserIdDTO != null)
            {

                return new ApiResult<GetSharingCouponResponse>()
                {
                    Code = ResultCode.Success,
                    Data = _mapper.Map<GetSharingCouponResponse>(haveduserCouponPageResByUserIdDTO)
                };
            }

            return new ApiResult<GetSharingCouponResponse>()
            {
                Code = ResultCode.Success,
                Data = null
            };
        }

        public async Task<ApiPagedResult<GetSharingOrdersResponse>> GetSharingOrders(GetSharingOrdersRequest request)
        {
            var getOrderUserShareDetail = await _orderUserShareDetailRepository.GetOrderUserShareDetail(request);

            if (getOrderUserShareDetail?.Items?.Count > 0)
            {
                var orderNos = getOrderUserShareDetail?.Items?.ToList()?.Select(_ => _.OrderNo)?.ToList();

                var getOrders = await _orderRepository.GetOrders(orderNos);

                var getOrderProducts = await _orderProductRepository.GetOrderProducts(orderNos);

                List<GetSharingOrdersResponse> getSharingOrdersResponses = new List<GetSharingOrdersResponse>(10);

                getOrders?.ForEach(_ =>
                {
                    GetSharingOrdersResponse getSharingOrdersResponse = new GetSharingOrdersResponse();
                    getSharingOrdersResponse.OrderNo = _.OrderNo;
                    getSharingOrdersResponse.OrderStatus = ((OrderStatusEnum)_.OrderStatus).GetDescription();
                    getSharingOrdersResponse.OrderTime = _.OrderTime.ToString("yyyy-MM-dd HH:mm:ss");
                    getSharingOrdersResponse.UserName = _.ContactName;
                    getSharingOrdersResponse.Telephone = _.ContactPhone;
                    getSharingOrdersResponse.ListTotalProductNum = _.TotalProductNum + _.ServiceNum;
                    var products = getOrderProducts?.Where(item => item.OrderNo == _.OrderNo)?.Where(item => item.ParentOrderPackagePid == 0)?.ToList();
                    getSharingOrdersResponse.Products = _mapper.Map<List<OrderDetailProductDTO>>(products);
                    getSharingOrdersResponses.Add(getSharingOrdersResponse);

                });

                return new ApiPagedResult<GetSharingOrdersResponse>()
                {
                    Code = ResultCode.Success,
                    Data = new ApiPagedResultData<GetSharingOrdersResponse>()
                    {
                        TotalItems = getOrderUserShareDetail?.TotalItems ?? 0,
                        Items = getSharingOrdersResponses
                    }
                };

            }
            return new ApiPagedResult<GetSharingOrdersResponse>()
            {
                Code = ResultCode.Success,
                Data = new ApiPagedResultData<GetSharingOrdersResponse>()
                {
                    TotalItems = 0,
                    Items = new List<GetSharingOrdersResponse>()
                }
            };
        }

        public async Task<ApiResult<GetSharingSummaryResponse>> GetSharingSummary(GetSharingSummaryRequest request)
        {
            var getOrderUserShare = await _orderUserShareRepository.GetOrderUserShare(request.UserId);

            GetSharingSummaryResponse getSharingSummaryResponse = new GetSharingSummaryResponse()
            {
                NumberOfDiscountNumPushed = getOrderUserShare?.ExchangedCouponNum ?? 0,
                NumberOfSharedUsers = getOrderUserShare?.ExchangeSumOrderNum ?? 0,
                UserName = getOrderUserShare?.UserName ?? string.Empty
            };

            return Result.Success(getSharingSummaryResponse);
        }
    }
}
