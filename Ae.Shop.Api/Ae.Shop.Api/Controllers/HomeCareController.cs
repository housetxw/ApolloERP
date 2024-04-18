using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ApolloErp.Login.Auth;
using ApolloErp.Web.WebApi;
using Ae.Shop.Api.Core.Interfaces;
using Ae.Shop.Api.Core.Model;
using Ae.Shop.Api.Core.Request;
using Ae.Shop.Api.Core.Request.HomeCare;
using Ae.Shop.Api.Filters;

namespace Ae.Shop.Api.Controllers
{
    /// <summary>
    /// 上门养车
    /// </summary>
    [Route("[controller]/[action]")]
    //[Filter(nameof(HomeCareController))]
    public class HomeCareController : ControllerBase
    {
        public IHomeCareService _homeCareService;
        private IIdentityService identityService;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="homeCareService"></param>
        /// <param name="identityService"></param>
        public HomeCareController(IHomeCareService homeCareService, IIdentityService identityService)
        {
            this._homeCareService = homeCareService;
            this.identityService = identityService;
        }

        #region 技师领料
        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<string>> CreateHomeCareRecord([FromBody]ApiRequest<HomeCareRecordDTO> request)
        {
            return await _homeCareService.CreateHomeCareRecord(request.Data);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<HomeCareProductDTO>>> GetHomeCareProducts([FromQuery]HomeCareProductDTO request)
        {
            return await _homeCareService.GetHomeCareProducts(request);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiPagedResult<HomeCareRecordDTO>> GetHomeCareRecordPages([FromBody]ApiRequest<HomeCareRecordRequest> request)
        {
            return await _homeCareService.GetHomeCareRecordPages(request.Data);
        }
        #endregion

        #region 技师退料
        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiPagedResult<HomeReturnRecordDTO>> GetHomeReturnRecordPages([FromBody]ApiRequest<HomeCareRecordRequest> request)
        {
            return await _homeCareService.GetHomeReturnRecordPages(request.Data);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<string>> CreateHomeReturnRecord([FromBody]ApiRequest<HomeReturnRecordDTO> request)
        {
            return await _homeCareService.CreateHomeReturnRecord(request.Data);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<HomeReturnProductDTO>>> GetHomeReturnProducts([FromQuery]HomeReturnProductDTO request)
        {
            return await _homeCareService.GetHomeReturnProducts(request);
        }


        #endregion
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<EmployeeDTO>>> GetEmployes()
        {
            return await _homeCareService.GetEmployes();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<HomeReturnProductDTO>>> GetHomeReturnProductsByTech([FromQuery]HomeReturnRecordDTO request)
        {
            return await _homeCareService.GetHomeReturnProductsByTech(request);
        }

        #region B.App 接口
        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        public async Task<ApiResult<List<HomeCareProductDTO>>> GetHomeCareStocks([FromQuery]HomeCareStockRequest request)
        {
            return await _homeCareService.GetHomeCareStocks(request);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        public async Task<ApiPagedResult<HomeCareRecordDTO>> GetHomeCareRecords([FromQuery]HomeCareRequest request)
        {
            return await _homeCareService.GetHomeCareRecords(request);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        public async Task<ApiResult<HomeCareRecordDTO>> GetHomeCareRecord([FromQuery]HomeCareDetailRequest request)
        {
            return await _homeCareService.GetHomeCareRecord(request);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        public async Task<ApiPagedResult<HomeReturnRecordDTO>> GetHomeReturnRecords([FromQuery]HomeCareRequest request)
        {
            return await _homeCareService.GetHomeReturnRecords(request);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        public async Task<ApiResult<HomeReturnRecordDTO>> GetHomeReturnRecord([FromQuery]HomeCareDetailRequest request)
        {
            return await _homeCareService.GetHomeReturnRecord(request);
        }

        #endregion

        /// <summary>
        /// 分页查询耗材领料
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiPagedResult<ProductClaimRecordDTO>> GetProductClaimRecordPages([FromBody]ApiRequest<HomeCareRecordRequest> request)
        {
            return await _homeCareService.GetProductClaimRecordPages(request.Data);

        }

        /// <summary>
        /// 新增耗材领料
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<string>> CreateProductClaimRecord([FromBody]ApiRequest<ProductClaimRecordDTO> request)
        {
            return await _homeCareService.CreateProductClaimRecord(request.Data);

        }

        /// <summary>
        /// 取消耗材领料
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<string>> CancelProductClaimRecord([FromBody] ApiRequest<HomeCareDetailRequest> request)
        {
            return await _homeCareService.CancelProductClaimRecord(request.Data);

        }

        /// <summary>
        /// 领料记录重新更新库存--不重复扣减
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<string>> ClaimRepeatReduceStock([FromBody] ApiRequest<HomeCareDetailRequest> request)
        {
            return await _homeCareService.ClaimRepeatReduceStock(request.Data);

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<TechClaimProductDTO>>> GetProductClaimRecords([FromQuery]TechClaimProductDTO request)
        {
            return await _homeCareService.GetProductClaimRecords(request);

        }

    }
}