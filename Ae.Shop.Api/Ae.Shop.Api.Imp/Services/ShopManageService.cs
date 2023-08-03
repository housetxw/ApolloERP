using AutoMapper;
using ApolloErp.Web.WebApi;
using System;
using System.Collections.Generic;
using System.Text;
using ApolloErp.Login.Auth;
using System.Threading.Tasks;
using Ae.Shop.Api.Core.Request;
using Ae.Shop.Api.Core.Response;
using Ae.Shop.Api.Core.Interfaces;
using Ae.Shop.Api.Client.Clients;
using Ae.Shop.Api.Client.Request;
using Ae.Shop.Api.Dal.Repositorys.Company;

namespace Ae.Shop.Api.Imp.Services
{
   public class ShopManageService: IShopManageService
    {
        private readonly IMapper _mapper;
        private readonly IShopMangeClient shopMangeClient;
        private IIdentityService identityService;
        private readonly IStockManageService _stockManageService;
        private readonly ICompanyRepository _companyRepository;


        public ShopManageService(IMapper mapper, IIdentityService identityService, IStockManageService stockManageService, ICompanyRepository companyRepository,
          IShopMangeClient shopManageClient)
        {
            this._mapper = mapper;
            this.shopMangeClient = shopManageClient;
            this.identityService = identityService;
            this._stockManageService = stockManageService;
            this._companyRepository = companyRepository;
        }

        public async Task<ApiPagedResult<ShopSimpleInfoResponse>> GetShopListAsync(GetShopListRequest request)
        {
            var res = await shopMangeClient.GetShopListAsync(request);

            var vo = _mapper.Map<ApiPagedResult<ShopSimpleInfoResponse>>(res);
            return vo;
        }
        public async Task<ApiPagedResult<ShopSimpleInfoResponse>> GetCompanyShopList(GetShopListRequest request)
        {
            var orgType = identityService.GetOrgType();
            long companyId = Convert.ToInt64(identityService.GetOrganizationId());
            if (orgType == "1") //门店
            {
                var shopResult = await shopMangeClient.GetShopById(new GetShopClientRequest
                {
                    ShopId = companyId
                });
                if (shopResult.Code == ResultCode.Success)
                {
                    companyId = shopResult.Data.CompanyId;
                }
            }
            request.CompanyId = companyId;
            request.ShopTypes = new List<int> { 2 };

            var res = await shopMangeClient.GetShopListAsync(request);

            var vo = _mapper.Map<ApiPagedResult<ShopSimpleInfoResponse>>(res);
            return vo;
        }
        public async Task<List<ShopSimpleInfoResponse>> GetShopWareHouseList(GetShopListRequest request)
        {
            var vo = new List<ShopSimpleInfoResponse>();
            var orgType = identityService.GetOrgType();
            if (orgType == "1") //门店
            {
                var shopInfo = await _stockManageService.GetShopById();
                var s = new ShopSimpleInfoResponse
                {
                    Id = shopInfo?.ShopId ?? 0,
                    SimpleName = shopInfo?.SimpleName ?? ""
                };
                vo.Add(s);
            }
            else
            {
                if (request.QueryType == 0)
                {
                    request.ShopTypes = new List<int> { -1, 2 };
                }
                else
                {
                    request.ShopTypes = new List<int> { request.QueryType };
                }
                long companyId = Convert.ToInt64(identityService.GetOrganizationId());
                request.CompanyId = companyId;
                //var res = await shopMangeClient.GetShopWareHouseListAsync(request);

                var result = await _companyRepository.GetShopWareHouseListAsync(request);

                vo = _mapper.Map<List<ShopSimpleInfoResponse>>(result);
            }
            return vo;
        }
    }
}
