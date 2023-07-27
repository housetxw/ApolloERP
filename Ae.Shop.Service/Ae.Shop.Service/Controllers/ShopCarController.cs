using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ApolloErp.Log;
using ApolloErp.Web.WebApi;
using Ae.Shop.Service.Common.Constant;
using Ae.Shop.Service.Core.Interfaces;
using Ae.Shop.Service.Core.Model;
using Ae.Shop.Service.Core.Request;
using Ae.Shop.Service.Core.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ae.Shop.Service.Controllers
{
    /// <summary>
    /// 门店车辆
    /// </summary>
    [Route("[controller]/[action]")]
    public class ShopCarController : ControllerBase
    {
        private readonly IShopCarService _shopCarService;
        private readonly ApolloErpLogger<ShopCarController> _logger;

        public ShopCarController(IShopCarService shopCarService, ApolloErpLogger<ShopCarController> logger)
        {
            _shopCarService = shopCarService;
            _logger = logger;
        }


        /// <summary>
        /// 新增或修改门店车辆信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public async Task<ApiResult<bool>> AddOrModifyShopCar([FromBody] ApiRequest<ShopCarDTO> request)
        {
            //_logger.Info($"AddOrModifyShopCar:{JsonConvert.SerializeObject(request)}");
            return await _shopCarService.AddOrModifyShopCar(request.Data);
        }


        /// <summary>
        /// 查询门店车辆列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public async Task<ApiPagedResult<SimpleShopCarDTO>> GetShopCarListByShopId([FromQuery] GetShopCarPageListRequest request)
        {
            var result = await _shopCarService.GetShopCarListByShopId(request);
            return new ApiPagedResult<SimpleShopCarDTO>()
            {
                Code = ResultCode.Success,
                Data = result,
                Message = CommonConstant.QuerySuccess
            };
        }


        /// <summary>
        /// 查询门店车辆列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiPagedResult<SimpleShopCarDTO>> GetShopCarListByShopIdForApp([FromQuery] GetShopCarPageListRequest request)
        {
            var result = await _shopCarService.GetShopCarListByShopId(request);
            return new ApiPagedResult<SimpleShopCarDTO>()
            {
                Code = ResultCode.Success,
                Data = result,
                Message = CommonConstant.QuerySuccess
            };
        }

        /// <summary>
        /// 修改车辆状态
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public async Task<ApiResult<bool>> UpdateShopCarStatus([FromBody] ApiRequest<UpdateShopCarStatusRequest> request)
        {
            //_logger.Info($"UpdateShopCarStatus:{JsonConvert.SerializeObject(request)}");
            return await _shopCarService.UpdateShopCarStatus(request.Data);
        }


        /// <summary>
        /// 查询门店车辆详情
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<ShopCarDTO>> GetShopCarInfoById([FromQuery] BaseGetInfoRequest request)
        {
            var result = await _shopCarService.GetShopCarInfoById(request);
            return Result.Success(result);
        }


        /// <summary>
        /// 查询门店车辆记录列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public async Task<ApiPagedResult<ShopCarRecordDTO>> GetShopCarRecordListByShopId([FromQuery] GetShopCarRecordPageListRequest request) 
        {
            var result = await _shopCarService.GetShopCarRecordListByShopId(request);
            return new ApiPagedResult<ShopCarRecordDTO>()
            {
                Code = ResultCode.Success,
                Data = result,
                Message = CommonConstant.QuerySuccess
            };
        }


        /// <summary>
        /// 新增门店车辆使用信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public async Task<ApiResult<bool>> AddShopCarRecordInfo([FromBody] ApiRequest<ShopCarRecordDTO> request) 
        {
            //_logger.Info($"AddShopCarRecordInfo:{JsonConvert.SerializeObject(request)}");
            return await _shopCarService.AddShopCarRecordInfo(request.Data);
        }

        /// <summary>
        /// 查询门店车辆使用详情
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<ShopCarRecordDTO>> GetShopCarRecordInfoById([FromQuery] BaseGetInfoRequest request) 
        {
            var result = await _shopCarService.GetShopCarRecordInfoById(request);
            return Result.Success(result);
        }



        /// <summary>
        /// 删除车辆使用记录
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public async Task<ApiResult<bool>> DeleteShopCarRecordStatus([FromBody] ApiRequest<BaseGetInfoRequest> request)
        {
            return await _shopCarService.DeleteShopCarRecordStatus(request.Data);
        }


        /// <summary>
        /// 查询门店技师出行记录列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiPagedResult<GetTripRecordListResponse>> GetTripRecordPageList([FromQuery] GetTripRecordPageListRequest request)
        {
            var result = await _shopCarService.GetTripRecordPageList(request);
            return new ApiPagedResult<GetTripRecordListResponse>()
            {
                Code = ResultCode.Success,
                Data = result,
                Message = CommonConstant.QuerySuccess
            };
        }

        /// <summary>
        /// 查询门店技师出行记录列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public async Task<ApiPagedResult<GetTripRecordListResponse>> GetTripRecordPageListForShop([FromQuery] GetTripRecordPageListRequest request)
        {
            var result = await _shopCarService.GetTripRecordPageListForShop(request);
            return new ApiPagedResult<GetTripRecordListResponse>()
            {
                Code = ResultCode.Success,
                Data = result,
                Message = CommonConstant.QuerySuccess
            };
        }

        /// <summary>
        /// 新增出行记录
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult> AddTechTripRecord([FromBody] AddTechTripRecordRequest request)
        {
            return await _shopCarService.AddTechTripRecord(request);
        }

        /// <summary>
        /// 添加还车
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult> AddTechTripReturn([FromBody] AddTechTripReturnRequest request)
        {
            return await _shopCarService.AddTechTripReturn(request);
        }

        /// <summary>
        /// 获取出行记录详情
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<GetTripRecordInfoResponse>> GetTechTripRecordInfo([FromQuery] BaseGetInfoRequest request)
        {
            return await _shopCarService.GetTechTripRecordInfo(request);
        }

    }
}
