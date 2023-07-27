using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NLog.Filters;
using ApolloErp.Log;
using ApolloErp.Web.WebApi;
using Ae.Shop.Service.Core.Interfaces;
using Ae.Shop.Service.Core.Model;
using Ae.Shop.Service.Core.Request;
using Ae.Shop.Service.Core.Request.ShopServer;
using Ae.Shop.Service.Core.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ae.Shop.Service.Controllers
{
    [Route("[controller]/[action]")]
    public class ShopServerController : ControllerBase
    {
        private readonly IShopServerService  _shopServerService;
        private readonly ApolloErpLogger<ShopServerController> _logger;
        public ShopServerController(IShopServerService shopServerService,
            ApolloErpLogger<ShopServerController> logger
            ) 
        {
            _shopServerService = shopServerService;
            _logger = logger;
        }

        /// <summary>
        /// 根据PID查询门店服务是否上架
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<List<GetShopServiceListWithPIDResponse>>> GetShopServiceListWithPID([FromBody] GetShopServiceListWithPIDRequest request) 
        {
            var result = await _shopServerService.GetShopServiceListWithPID(request);
            return Result.Success(result);
        }

        /// <summary>
        /// 获取门店所有上架的服务
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<GetShopServiceListWithPIDResponse>>> GetAllOnLineServicesByShopId(
            [FromQuery] GetAllOnLineServicesByShopIdRequest request)
        {
            var result = await _shopServerService.GetAllOnLineServicesByShopId(request);

            return Result.Success(result);
        }

        /// <summary>
        /// 新增基本服务大类
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult> AddBaseServiceCategoryAsync([FromBody] ApiRequest<AddServiceCategoryRequest> request)
        {
            //_logger.Info($"新增基本服务大类 data:{JsonConvert.SerializeObject(request)}");
            return await _shopServerService.AddBaseServiceCategoryAsync(request.Data);
        }

        /// <summary>
        /// 新增基本服务
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public async Task<ApiResult<bool>> AddBaseServiceAsync([FromBody] ApiRequest<BaseServiceDTO> request) 
        {
            //_logger.Info($"新增基本服务 data:{JsonConvert.SerializeObject(request)}");
            var result = await _shopServerService.AddBaseServiceAsync(request.Data);
            return Result.Success(result);
        }


        /// <summary>
        /// 查询基本服务列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<BaseServiceDTO>>> GetBaseServiceListAsync([FromQuery] GetBaseServiceListRequest request)
        {
            var result = await _shopServerService.GetBaseServiceListAsync(request);
            return Result.Success(result);
        }
        /// <summary>
        /// 获取基本服务详情
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<BaseServiceDTO>> GetBaseServiceInfoAsync([FromQuery] GetBaseServiceInfoRequest request) 
        {
            var result = await _shopServerService.GetBaseServiceInfoAsync(request);
            return Result.Success(result);
        }

        /// <summary>
        /// 修改基本服务
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<bool>> ModifyBaseServiceInfoAsync([FromBody] ApiRequest<BaseServiceDTO> request)
        {
            //_logger.Info($"修改基本服务 data:{JsonConvert.SerializeObject(request)}");
            var result = await _shopServerService.ModifyBaseServiceInfoAsync(request.Data);
            return Result.Success(result);
        }

        /// <summary>
        /// 获取门店服务列表
        /// </summary>
        /// <param name="request"></param>
        /// Type 1 门店开通过的服务；0门店未开通过的服务
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<ShopServiceDTO>>> GetShopServiceListAsync([FromQuery] GetShopServiceListRequest request) 
        {
            var result = await _shopServerService.GetShopServiceListAsync(request);
            return Result.Success(result);
        }
        /// <summary>
        /// 添加门店服务
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<bool>> AddShopServiceAsync([FromBody] ApiRequest<AddShopServiceRequest> request) 
        {
            //_logger.Info($"添加门店服务 data:{JsonConvert.SerializeObject(request)}");
            var result = await _shopServerService.AddShopServiceAsync(request.Data);
            return Result.Success(result);
        }

        /// <summary>
        /// 获取门店服务详情
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<ShopServiceDTO>> GetShopServiceInfoAsync([FromQuery] GetShopServiceInfoRequest request)
        {
            var result = await _shopServerService.GetShopServiceInfoAsync(request);
            return Result.Success(result);
        }
        /// <summary>
        /// 修改门店服务
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<bool>> ModifyShopServiceAsync([FromBody] ApiRequest<ModifyShopServiceRequest> request)
        {
            //_logger.Info($"修改门店服务 data:{JsonConvert.SerializeObject(request)}");
            var result = await _shopServerService.ModifyShopServiceAsync(request.Data);
            return Result.Success(result);
        }
        /// <summary>
        /// 查询门店开通的服务
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]/// 
        public async Task<ApiResult<List<BaseServiceDTO>>> GetShopServiceCategoryAsync([FromQuery] GetShopRequest request) 
        {
            var result = await _shopServerService.GetShopServiceCategory(request);
            return Result.Success(result);
        }


        /// <summary>
        /// 查询门店开通的服务项目
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<ShopServiceTypeDTO>>> GetShopServiceTypeAsync([FromQuery] BaseGetShopRequest request)
        {
            var result = await _shopServerService.GetShopServiceTypeListAsync(request);
            return Result.Success(result);
        }

        [HttpGet]
        public async Task<ApiPagedResult<ShopServiceTypeNewDTO>> GetShopServiceTypePagesAsync([FromQuery]GetShopServiceTypePageRequest request)
        {
            var result = await _shopServerService.GetShopServiceTypePagesAsync(request);
            return result;
        }

        [HttpPost]
        public async Task<ApiResult<string>> DeleteShopServiceType([FromBody]BaseEntityPostRequest<ShopServiceTypeNewDTO> request)
        {
            return await _shopServerService.DeleteShopServiceType(request.Data);
        }

        /// <summary>
        /// 生成门店服务类型
        /// </summary>
        /// <param name="shopId"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<bool>> GenerateShopServiceType([FromBody] BaseGetShopRequest request)
        {
            var result = await _shopServerService.GenerateShopServiceType(request.ShopId);
            return Result.Success(result);
        }
    }
}
