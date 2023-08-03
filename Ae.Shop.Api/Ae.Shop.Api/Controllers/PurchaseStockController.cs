using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ApolloErp.Web.WebApi;
using Ae.Shop.Api.Core.Interfaces;
using Ae.Shop.Api.Core.Model;
using Ae.Shop.Api.Core.Request;
using ApolloErp.Login.Auth;
namespace Ae.Shop.Api.Controllers
{
    [Route("[controller]/[action]")]
    public class PurchaseStockController : ControllerBase
    {
        public IPurchaseStockService purchaseStockService;
        public ITransferTaskService transferTaskService;
        private IIdentityService identityService;
        public PurchaseStockController(IPurchaseStockService purchaseStockService,
            ITransferTaskService transferTaskService, IIdentityService identityService)
        {
            this.purchaseStockService = purchaseStockService;
            this.transferTaskService = transferTaskService;
            this.identityService = identityService;
        }

        /// <summary>
        /// 查询供应的产品
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<ICollection<VenderProductForAppVo>>> SearchVenderProductListForApp([FromQuery]AcceptCompanyStockRequest request)
        {
            var result = await purchaseStockService.SearchVenderProductListForApp();
            return new ApiResult<ICollection<VenderProductForAppVo>>
            {
                Code = ResultCode.Success,
                Data = result
            };
        }

        /// <summary>
        /// 初始化库存
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<string>> SubmitVenderStock([FromBody]ApiRequest<VenderStockInitRequest> request)
        {
            return await purchaseStockService.SubmitVenderStock(request.Data);
        }

        /// <summary>
        /// 提交要货 ->公司和门店使用
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<string>> SubmitCompanyInStock([FromBody]ApiRequest<AcceptCompanyStockRequest> request)
        {
            return await purchaseStockService.SubmitCompanyInStock(request.Data);
        }

        /// <summary>
        /// 获取要货列表(待发货:1  已发货:2  待收货:3  已收货:4)
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<VenderStockDTO>>> GetCompanyInStocks([FromQuery]GetCompanyStocksRequest request)
        {
            return await purchaseStockService.GetCompanyInStocks(request);
        }

        /// <summary>
        /// 公司/供应商发货  批量接口
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<string>> CompanySendStock([FromBody]ApiRequest<AcceptCompanyStockRequest> request)
        {
            return await purchaseStockService.CompanySendStock(request.Data);

        }

        /// <summary>
        /// 获取公司/供应商库存
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<VenderStockResponse>> GetCompanyStocks([FromQuery]GetCompanyStocksRequest request)
        {
            return await purchaseStockService.GetCompanyStocks(request);
        }

        /// <summary>
        /// 公司取消发货
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<string>> CancelCompanySendStock([FromBody]ApiRequest<CancelTaskRequest> request)
        {
            return await purchaseStockService.CancelCompanySendStock(request.Data);
        }

        /// <summary>
        /// 获取供应商产品表记录
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<ICollection<VenderProductForAppVo>>> SearchVenderProductList()
        {
            var result = await purchaseStockService.SearchVenderProductList();
            return new ApiResult<ICollection<VenderProductForAppVo>>
            {
                Code = ResultCode.Success,
                Data = result
            };
        }

        [HttpPost]
        public async Task<ApiResult<string>> VenderCheckInStock([FromBody]ApiRequest<AcceptCompanyStockRequest> request)
        {
            if (!request.Data.Products.Any())
            {
                return new ApiResult<string>
                {
                    Code = ResultCode.Exception,
                    Message = "请选择需要收货的产品!"
                };
            }
            var stockRequest = new AcceptCompanyStockRequest
            {
                UpdateBy = identityService.GetUserName(),
                LocationId = Convert.ToInt64(identityService.GetOrganizationId()),
                LocationType = Convert.ToInt32(identityService.GetOrgType()),
                Products = request.Data.Products
            };
            return await transferTaskService.VenderCheckInStock(stockRequest);
        }
    }
}