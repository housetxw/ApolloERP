using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ApolloErp.Web.WebApi;
using Ae.BasicData.Service.Common.Constant;
using Ae.BasicData.Service.Core.Interfaces;
using Ae.BasicData.Service.Core.Request;
using Ae.BasicData.Service.Core.Response;
using Ae.BasicData.Service.Filters;

namespace Ae.BasicData.Service.Controllers
{
    [Route("[controller]/[action]")]
    //[Filter(nameof(RegionChinaController))]
    public class RegionChinaController : Controller
    {
        // ---------------------------------- 变量和构造器 --------------------------------------
        private readonly IRegionChinaService regionChinaSvc;

        public RegionChinaController(IRegionChinaService regionChinaSvc)
        {
            this.regionChinaSvc = regionChinaSvc;
        }

        // ---------------------------------- 接口实现 --------------------------------------
        /// <summary>
        /// 获取省市区所有数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<RegionChinaResDTO>>> GetAllRegionChinaList()
        {
            var res = await regionChinaSvc.GetAllRegionChinaList();
            return Result.Success(res, CommonConstant.QuerySuccess);
        }

        /// <summary>
        /// 根据RegionId获取子一级所有数据
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<RegionChinaResDTO>>> GetRegionChinaListByRegionId(RegionChinaReqByRegionIdDTO req)
        {
            if (req?.RegionId?.Length == 1 && req.RegionId != "0" || req?.RegionId?.Length > 1 && req.RegionId.Length < 6)
            {
                return Result.Failed<List<RegionChinaResDTO>>(ResultCode.ArgumentError, "输入的RegionId格式不正确");
            }

            var res = await regionChinaSvc.GetRegionChinaListByRegionId(req);
            return Result.Success(res, CommonConstant.QuerySuccess);
        }

        /// <summary>
        /// 根据RegionLevel获取所有数据
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<RegionChinaResByLevelDTO>>> GetRegionChinaListByLevel(RegionChinaReqByLevelDTO req)
        {
            var res = await regionChinaSvc.GetRegionChinaListByLevel(req);
            return Result.Success(res, CommonConstant.QuerySuccess);
        }

        /// <summary>
        /// 获取省市区三层地址信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<GetRegionChinaResponse>> GetAllRegionChinaWithThreeLayer() 
        {
            var result = await regionChinaSvc.GetAllRegionChinaWithThreeLayer();
            return Result.Success(result, CommonConstant.QuerySuccess);
        }

        /// <summary>
        /// 获取省市区三级地址
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<ThreeLevelRegionChinaResponse>> GetThreeLevelRegionChina() 
        {
            var result = await regionChinaSvc.GetThreeLevelRegionChina();
            return Result.Success(result, CommonConstant.QuerySuccess);
        }

        /// <summary>
        /// 根据经纬度获取当前位置
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<GetPositionResponse>> GetPosition([FromQuery] GetPositionRequest request)
        {
            return await regionChinaSvc.GetPosition(request);
        }


        /// <summary>
        /// 根据地址获取坐标
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<GetCoordinateResponse>> GetCoordinate([FromQuery] GetCoordinateRequest request) 
        {
            return await regionChinaSvc.GetCoordinate(request);
        }
        // ---------------------------------- 私有方法 --------------------------------------


        /// <summary>
        /// 获取父级RegionId
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<string>> GetParentRegionId([FromQuery] ParentRegionIdRequest request)
        {
            var result = await regionChinaSvc.GetParentRegionId(request);

            return Result.Success<string>(result);
        }
    }
}