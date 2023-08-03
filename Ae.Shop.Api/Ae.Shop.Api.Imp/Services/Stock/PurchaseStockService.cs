using ApolloErp.Login.Auth;
using ApolloErp.Web.WebApi;
using Ae.Shop.Api.Client.Clients;
using Ae.Shop.Api.Core.Interfaces;
using Ae.Shop.Api.Core.Model;
using Ae.Shop.Api.Core.Request;
using Ae.Shop.Api.Dal.Repositorys.Company;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Shop.Api.Imp.Services
{
    public class PurchaseStockService : IPurchaseStockService
    {
        #region
        private readonly IPurchaseClient purchaseClient;
        private readonly IIdentityService _identityService;
        private readonly ICompanyRepository companyRepository;
        private readonly IWmsClient wmsClient;
        public PurchaseStockService(IPurchaseClient purchaseClient, IIdentityService _identityService,
             ICompanyRepository companyRepository, IWmsClient wmsClient)
        {
            this.purchaseClient = purchaseClient;
            this._identityService = _identityService;
            this.companyRepository = companyRepository;
            this.wmsClient = wmsClient;
        }
        #endregion

        /// <summary>
        /// 获取供应商自己的产品
        /// </summary>
        /// <returns></returns>
        public async Task<ICollection<VenderProductForAppVo>> SearchVenderProductListForApp()
        {
            var orgId = _identityService.GetOrganizationId();
            var orgType = _identityService.GetOrgType();

            var companyInfo = await companyRepository.GetAsync(orgId);
            if (companyInfo.VenderId > 0)
            {
                var result = await purchaseClient.SearchVenderProductListForApp(new SearchVenderProductListForAppRequest
                {
                    VenderId = companyInfo.VenderId,
                    PageIndex = 1,
                    PageSize = 10000
                });

                if (result.Code == ResultCode.Success)
                {
                    return result.Data.Items;
                }
            }
            return new List<VenderProductForAppVo>();
        }

        /// <summary>
        /// 初始化库存
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<string>> SubmitVenderStock(VenderStockInitRequest request)
        {
            if (!request.Stocks.Any() || !request.Stocks.Any(r => r.Num > 0))
            {
                return new ApiResult<string>
                {
                    Code = ResultCode.Exception,
                    Message = "请选择产品!"
                };
            }
            var productStocks = request.Stocks.Where(r => r.Num > 0).ToList();
            request.Stocks = productStocks;
            request.UpdateBy = _identityService.GetUserName();
            request.CompanyId = Convert.ToInt64(_identityService.GetOrganizationId());

            return await wmsClient.SubmitVenderStock(request);
        }

        #region 供应商收发货功能

        /// <summary>
        /// 公司取消发货
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<string>> CancelCompanySendStock(CancelTaskRequest request)
        {
            request.UpdateBy = _identityService.GetUserName();
            var result = await wmsClient.CancelCompanySendStock(request);
            return result;
        }

        /// <summary>
        /// 公司/供应商发货  批量接口
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<string>> CompanySendStock(AcceptCompanyStockRequest request)
        {
            request.UpdateBy = _identityService.GetUserName();
            request.LocationId = Convert.ToInt64(_identityService.GetOrganizationId());
            request.LocationType = Convert.ToInt32(_identityService.GetOrgType());

            var result = await wmsClient.CompanySendStock(request);
            return result;
        }

        /// <summary>
        /// 获取要货列表(待发货:1  已发货:2  待收货:3  已收货:4)
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<List<VenderStockDTO>>> GetCompanyInStocks(GetCompanyStocksRequest request)
        {
            request.LocationId = Convert.ToInt64(_identityService.GetOrganizationId());
            request.LocationType = Convert.ToInt32(_identityService.GetOrgType());
            var result = await wmsClient.GetCompanyInStocks(request);
            return result;
        }

        /// <summary>
        /// 获取公司/供应商库存
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<VenderStockResponse>> GetCompanyStocks(GetCompanyStocksRequest request)
        {
            request.LocationId = Convert.ToInt64(_identityService.GetOrganizationId());
            request.LocationType = Convert.ToInt32(_identityService.GetOrgType());

            var result = await wmsClient.GetCompanyStocks(request);
            return result;
        }

        /// <summary>
        /// 提交要货 ->公司和门店使用
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<string>> SubmitCompanyInStock(AcceptCompanyStockRequest request)
        {
            if (!request.Products.Any() || !request.Products.Any(r => r.Num > 0))
            {
                return new ApiResult<string>
                {
                    Code = ResultCode.Exception,
                    Message = "请选择产品!"
                };
            }
            var productStocks = request.Products.Where(r => r.Num > 0).ToList();
            request.Products = productStocks;

            request.UpdateBy = _identityService.GetUserName();
            request.LocationId = Convert.ToInt64(_identityService.GetOrganizationId());
            request.LocationType = Convert.ToInt32(_identityService.GetOrgType());
            var result = await wmsClient.SubmitCompanyInStock(request);
            return result;
        }

        /// <summary>
        /// 获取所有的产品
        /// </summary>
        /// <returns></returns>
        public async Task<ICollection<VenderProductForAppVo>> SearchVenderProductList()
        {
            var locationType = Convert.ToInt32(_identityService.GetOrgType());
            if (locationType == 4)
            {
                var companyInfo = await companyRepository.GetAsync(_identityService.GetOrganizationId());
                //供应商
                if (companyInfo.ParentId <= 0)
                {
                    return new List<VenderProductForAppVo>();
                }
            }

            //获取所有的产品
            var result = await purchaseClient.SearchVenderProductListForApp(new SearchVenderProductListForAppRequest
            {
                PageIndex = 1,
                PageSize = 10000
            });
            if (result.Code == ResultCode.Success)
            {
                return result.Data.Items;
            }
            return new List<VenderProductForAppVo>();
        }

        #endregion
    }
}
