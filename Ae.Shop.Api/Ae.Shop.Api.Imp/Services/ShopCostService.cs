using AutoMapper;
using Microsoft.Extensions.Configuration;
using ApolloErp.Common.BrandLogoHelper;
using ApolloErp.Log;
using ApolloErp.Login.Auth;
using ApolloErp.Web.WebApi;
using Ae.Shop.Api.Common.Exceptions;
using Ae.Shop.Api.Core.Enums;
using Ae.Shop.Api.Core.Interfaces;
using Ae.Shop.Api.Core.Model.ShopCost;
using Ae.Shop.Api.Core.Request.ShopCost;
using Ae.Shop.Api.Core.Response.ShopCost;
using Ae.Shop.Api.Dal.Model.ShopCost;
using Ae.Shop.Api.Dal.Repositorys.ShopCost;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Shop.Api.Imp.Services
{
    public class ShopCostService: IShopCostService
    {
        private readonly ApolloErpLogger<ShopCostService> logger;
        private readonly IShopCostRepository shopcostRepository;
        private readonly IConfiguration configuration;
        private readonly IMapper mapper;
        private readonly IIdentityService identityService;

        public ShopCostService(ApolloErpLogger<ShopCostService> logger,
            IShopCostRepository shopcostRepository, IMapper mapper,
            IConfiguration configuration, IIdentityService identityService)
        {
            this.configuration = configuration;
            this.logger = logger;
            this.shopcostRepository = shopcostRepository;
            this.mapper = mapper;
            this.identityService = identityService;
        }

        /// <summary>
        /// 费用列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiPagedResult<GetShopCostResponse>> GetShopCostList(ShopCostRequest request)
        {
            var organizationId = identityService.GetOrganizationId();
            long.TryParse(organizationId, out long shopId);
            if (shopId <= 0)
            {
                throw new CustomException("登录信息异常");
            }
            request.ShopId = shopId;
            var dalResponse = await shopcostRepository.GetShopCostList(request);
            List<GetShopCostResponse> items = mapper.Map<List<GetShopCostResponse>>(dalResponse.Items);
            return Result.Success(items, dalResponse.TotalItems);
        }

        /// <summary>
        /// 费用条件列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<GetShopCostTypeListResponse>> GetShopCostTypeListCondition()
        {
            GetShopCostTypeListResponse response = new GetShopCostTypeListResponse();
            var dalResponse = await shopcostRepository.GetShopCostTypeListCondition();
            List<ShopCostTypeVO> items = mapper.Map<List<ShopCostTypeVO>>(dalResponse);
            response.TypeList = items;
            return Result.Success(response);
        }

        /// <summary>
        /// 费用详情
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<GetShopCostResponse>> GetShopCostDetail(GetShopCostDetailRequest request)
        {
            GetShopCostResponse response = new GetShopCostResponse();
            var dalResponse = await shopcostRepository.GetShopCostDetail(request.Id);
            response = mapper.Map<GetShopCostResponse>(dalResponse);
            return Result.Success(response);
        }


        /// <summary>
        /// 费用新增
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<bool>> AddShopCost(AddShopCostRequest request)
        {
            var req = mapper.Map<AddShopCostDo>(request);
            req.CreateBy = req.UpdateBy= identityService.GetUserName()??"";
            req.CreateTime =req.UpdateTime= DateTime.Now.ToString();
           
            bool Ise = long.TryParse(identityService.GetOrganizationId(), out var shopId);
            req.ShopId = Ise ? shopId : 0;
            var dalResponse = await shopcostRepository.AddShopCost(req);
            return dalResponse > 0 ? Result.Success<bool>(true) : Result.Failed<bool>();
        }

        /// <summary>
        /// 费用编辑
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<bool>> UpdateShopCost(AddShopCostRequest request)
        {
            var req = mapper.Map<AddShopCostDo>(request);
            req.UpdateBy = identityService.GetUserName()??"";
            req.UpdateTime = DateTime.Now.ToString();
            var dalResponse = await shopcostRepository.UpdateShopCost(req);
            return dalResponse ? Result.Success<bool>(true) : Result.Failed<bool>();
        }

        /// <summary>
        /// 费用删除
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<bool>> DeleteShopCost(AddShopCostRequest request)
        {
            var req = mapper.Map<AddShopCostDo>(request);
            req.UpdateBy = identityService.GetUserName()??"";
            req.IsDeleted = 1;
            req.UpdateTime = DateTime.Now.ToString();
            var dalResponse = await shopcostRepository.DeleteShopCost(req);
            return dalResponse ? Result.Success<bool>(true) : Result.Failed<bool>();
        }

    }
}
