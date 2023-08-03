using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ApolloErp.Log;
using ApolloErp.Login.Auth;
using ApolloErp.Web.WebApi;
using Ae.Shop.Api.Core.Interfaces;
using Ae.Shop.Api.Core.Model;
using Ae.Shop.Api.Core.Request;
using Ae.Shop.Api.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ae.Shop.Api.Controllers
{
    [Route("[controller]/[action]")]
    //[Filter(nameof(CompanyController))]
    public class TransferController : ControllerBase
    {
        private readonly ApolloErpLogger<TransferController> _logger;
        private readonly ITransferService _transferService;

        public TransferController(ITransferService transferService, ApolloErpLogger<TransferController> logger)
        {
            this._logger = logger;
            this._transferService = transferService;
        }

        [HttpGet]
        public async Task<ApiResult<List<GroupSelectDTO>>> GetSourceWarehouses([FromQuery] GetShopInfoRequest request)
        {
            return await _transferService.GetSourceWarehouses(request);
        }

        [HttpPost]
        public async Task<ApiPagedResult<AllotTaskDTO>> GetAllotPageData([FromBody] ApiRequest<AllotPageRequest> request)
        {
            _logger.Info(JsonConvert.SerializeObject(request));
            return await _transferService.GetAllotPageData(request.Data);
        }


        //[HttpPost]
        //public async Task<ApiResult<List<StockLocationDTO>>> GetAllotProductsStock([FromBody]ApiRequest<AllotStockRequest> request)
        //{
        //    return await _transferService.GetAllotProductsStock(request.Data);
        //}
        /// <summary>
        /// 创建调拨单（直接更新库存）
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
 
        [HttpPost]
        public async Task<ApiResult<string>> CreateAllotTask([FromBody] ApiRequest<AllotTaskDTO> request)
        {
            //return await _transferService.CreateAllotTask(request.Data);
            return await _transferService.CreateAllotTaskAndUpdateStock(request.Data);
        }

        //[AllowAnonymous]
        [HttpGet]
        public async Task<ApiResult<AllotTaskDTO>> GetAllotTaskDetail([FromQuery] AllotPageRequest request)
        {
            return await _transferService.GetAllotTaskDetail(request);
        }

        //[HttpPost]
        //public async Task<ApiResult<string>> AuditAllotTask([FromBody]ApiRequest<AllotTaskDTO> request)
        //{
        //    return await _transferService.AuditAllotTask(request.Data);
        //}

        [HttpGet]
        [AllowAnonymous]
        public async Task<ApiResult<List<BasicInfoDTO>>> GetAllotTaskStatus()
        {
            return await _transferService.GetAllotTaskStatus();
        }

        //[HttpPost]
        //public async Task<ApiResult<string>> CancelAllotTask([FromBody] ApiResult<AllotTaskDTO> request)
        //{
        //    return await _transferService.CancelAllotTask(request.Data);
        //}


        //[AllowAnonymous]
        //[HttpPost]
        //public async Task<ApiResult<string>> UpdateAllotTaskStatus([FromBody]AllotTaskDTO request)
        //{
        //    return await _transferService.UpdateAllotTaskStatus(request);
        //}
    }
}