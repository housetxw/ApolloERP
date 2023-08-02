using AutoMapper;
using ApolloErp.Login.Auth;
using ApolloErp.Web.WebApi;
using Ae.B.Product.Api.Client.Interfaces;
using Ae.B.Product.Api.Client.Model.Promotion;
using Ae.B.Product.Api.Client.Request.Promotion;
using Ae.B.Product.Api.Core.Interfaces;
using Ae.B.Product.Api.Core.Model.Coupon;
using Ae.B.Product.Api.Core.Model.Promotion;
using Ae.B.Product.Api.Core.Model.ShopProduct;
using Ae.B.Product.Api.Core.Request.Coupon;
using Ae.B.Product.Api.Core.Request.Promotion;
using Ae.B.Product.Api.Core.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ae.B.Product.Api.Imp.Services
{
    public class PromotionService : IPromotionService
    {
        private readonly IPromotionClient _promotionClient;
        private readonly IMapper _mapper;
        private readonly IIdentityService _identityService;
        private readonly ICouponClient _couponClient;

        public PromotionService(IPromotionClient promotionClient, IMapper mapper, IIdentityService identityService, ICouponClient couponClient)
        {
            _promotionClient = promotionClient;
            _mapper = mapper;
            _identityService = identityService;
            _couponClient = couponClient;
        }

        /// <summary>
        /// 促销活动列表搜索
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiPagedResultData<PromotionActivityListVo>> SearchPromotionActivity(SearchPromotionActivityRequest request)
        {
            ApiPagedResultData<PromotionActivityListVo> result = new ApiPagedResultData<PromotionActivityListVo> { Items = new List<PromotionActivityListVo>() };
            var promotionData = await _promotionClient.SearchPromotionActivity(_mapper.Map<SearchPromotionActivityClientRequest>(request));
            result.TotalItems = promotionData.TotalItems;
            if (promotionData.Items != null && promotionData.Items.Any())
            {
                result.Items = _mapper.Map<List<PromotionActivityListVo>>(promotionData.Items);
            }
            return result;
        }

        /// <summary>
        /// 新增促销活动
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<string> AddPromotionActivity(AddPromotionActivityRequest request)
        {
            DateTime.TryParse(request.StartTime, out var startTime);
            DateTime.TryParse(request.EndTime, out var endTime);
            AddPromotionActivityClientRequest clientRequest = new AddPromotionActivityClientRequest
            {
                Name = request.Name,
                Subtitle = request.Subtitle,
                Description = request.Description,
                ThumbUrl = request.ThumbUrl,
                ActivityType = request.ActivityType,
                PromotionType = request.PromotionType,
                Label = request.Label,
                StartTime = startTime,
                EndTime = endTime.AddDays(1),
                ApplyChannel = request.ApplyChannel,
                ShopIds = request.ShopIds,
                CreateBy = _identityService.GetUserName(),
                ActivityProduct = request.ActivityProduct.Select(_ => new ActivityProductClientRequest
                {
                    ProductCode = _.ProductCode,
                    ProductName = _.ProductName,
                    PromotionPrice = _.PromotionPrice,
                    LimitQuantity = _.LimitQuantity
                }).ToList()
            };

            var result = await _promotionClient.AddPromotionActivity(clientRequest);

            return result;
        }

        /// <summary>
        /// 促销详情
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<PromotionDetailVo> GetPromotionDetail(PromotionDetailRequest request)
        {
            var result = await _promotionClient.GetPromotionActivityDetail(new PromotionActivityDetailClientRequest
            {
                ActivityId = request.ActivityId
            });
            if (result != null)
            {
                return _mapper.Map<PromotionDetailVo>(result);
            }
            return null;
        }

        /// <summary>
        /// 审核通过
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> AuditPromotionActivity(AuditPromotionActivityRequest request)
        {
            var result = await _promotionClient.AuditPromotionActivity(new AuditPromotionActivityClientRequest
            {
                ActivityId = request.ActivityId,
                SubmitBy = _identityService.GetUserName()
            });

            return result;
        }

        /// <summary>
        /// 查询门店商品by编码
        /// </summary>
        /// <param name="shopProductCode"></param>
        /// <returns></returns>
        public async Task<ShopProductVo> GetShopProductByCode(string shopProductCode)
        {
            var shopProductDto = await _promotionClient.GetShopProductByCode(shopProductCode);
            if (shopProductDto != null)
            {
                return _mapper.Map<ShopProductVo>(shopProductDto);
            }
            return null;
        }

        /// <summary>
        /// 结束活动
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> FinishPromotion(FinishPromotionRequest request)
        {
            var result = await _promotionClient.FinishPromotion(new FinishPromotionClientRequest
            {
                ActivityId = request.ActivityId,
                SubmitBy = _identityService.GetUserName()
            });

            return result;
        }

        public async Task<ApiPagedResult<GetCouponActivityListForShopResponse>> GetCouponActivityListForShop(ApiRequest<GetCouponActivityListForShopRequest> request)
        {
            return await _couponClient.GetCouponActivityListForShop(request);
        }

        public async Task<ApiResult<bool>> SaveShopGrantCoupon(ApiRequest<ShopGrantCouponDTO> request)
        {
            request.Data.CreateBy = _identityService.GetUserName();
            request.Data.CreateTime = DateTime.Now;
            request.Data.UpdateBy = _identityService.GetUserName();
            request.Data.UpdateTime = DateTime.Now;
            return await _couponClient.SaveShopGrantCoupon(request);
        }
    }
}
