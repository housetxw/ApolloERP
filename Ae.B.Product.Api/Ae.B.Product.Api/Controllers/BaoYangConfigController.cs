using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ApolloErp.Web.WebApi;
using Ae.B.Product.Api.Core.Interfaces;
using Ae.B.Product.Api.Core.Model;
using Ae.B.Product.Api.Core.Model.BaoYang;
using Ae.B.Product.Api.Core.Request;
using Ae.B.Product.Api.Core.Request.BaoYang;
using Ae.B.Product.Api.Core.Response;
using Ae.B.Product.Api.Core.Response.BaoYang;
using Ae.B.Product.Api.Filters;

namespace Ae.B.Product.Api.Controllers
{
    /// <summary>
    /// 保养适配配置相关接口
    /// </summary>
    [Route("[controller]/[action]")]
    //[Filter(nameof(BaoYangConfigController))]
    public class BaoYangConfigController : ControllerBase
    {
        private readonly IBaoYangConfigService _baoYangConfigService;

        public BaoYangConfigController(IBaoYangConfigService baoYangConfigService)
        {
            _baoYangConfigService = baoYangConfigService;
        }

        /// <summary>
        /// 保养适配配置查询
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<GetBaoYangPartAdaptationsResponse>>> GetBaoYangPartAdaptations(
            [FromQuery] GetBaoYangPartAdaptationsRequest request)
        {
            var result = await _baoYangConfigService.GetBaoYangPartAdaptationsAsync(request);

            return Result.Success(result);
        }

        /// <summary>
        /// 获取配件类型
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<PartNameListResponse>>> GetPartNameList()
        {
            var result = await _baoYangConfigService.GetPartNameListAsync();

            return Result.Success(result);
        }

        /// <summary>
        /// 添加配件号
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<bool>> AddPartCode([FromBody]ApiRequest<AddPartCodeRequest> request)
        {
            var result = await _baoYangConfigService.AddPartCodeAsync(request.Data);

            return Result.Success(result);
        }

        /// <summary>
        /// 根据OE号查询零件号
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<PartCodeVo>>> GetPartCodeAndBrandByOe(
            [FromQuery] PartCodeAndBrandByOeRequest request)
        {
            var result = await _baoYangConfigService.GetPartCodeAndBrandByOe(request);

            return Result.Success(result);
        }

        /// <summary>
        /// 添加特殊配件号
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<bool>> AddSpecialPartCode([FromBody]ApiRequest<AddSpecialPartRequest> request)
        {
            var result = await _baoYangConfigService.AddSpecialPartCodeAsync(request.Data);

            return Result.Success(result);
        }

        /// <summary>
        /// 批量添加配件号
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<bool>> BatchAddAdaptation([FromBody]ApiRequest<BatchAddAdaptationRequest> request)
        {
            var result = await _baoYangConfigService.BatchAddAdaptationAsync(request.Data);

            return Result.Success(result);
        }

        /// <summary>
        /// 删除配件号
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<bool>> DeletePartCode([FromBody]ApiRequest<DeletePartCodeRequest> request)
        {
            var result = await _baoYangConfigService.DeletePartCodeAsync(request.Data);

            return Result.Success(result);
        }

        /// <summary>
        /// 修改OE件号
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<bool>> UpdateOePartCode([FromBody]ApiRequest<UpdateOePartCodeRequest> request)
        {
            var result = await _baoYangConfigService.UpdateOePartCodeAsync(request.Data);

            return Result.Success(result);
        }

        /// <summary>
        /// 修改零件号
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<bool>> UpdatePartCode([FromBody]ApiRequest<UpdatePartCodeRequest> request)
        {
            var result = await _baoYangConfigService.UpdatePartCodeAsync(request.Data);

            return Result.Success(result);
        }

        /// <summary>
        /// 配件类型查询
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<BaoYangPartsVo>>> GetBaoYangParts()
        {
            var result = await _baoYangConfigService.GetBaoYangParts();
            return Result.Success(result);
        }

        /// <summary>
        /// 查询车型已绑定套餐
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<BaoYangPackageRefResponse>> GetBaoYangPackageRef(
            [FromQuery] BaoYangPackageRefRequest request)
        {
            var result = await _baoYangConfigService.GetBaoYangPackageRef(request);
            return Result.Success(result);
        }

        /// <summary>
        /// 获取套餐baoYangType
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<BaoYangTypeConfigVo>>> GetPackageByType()
        {
            var result = await _baoYangConfigService.GetPackageByType();

            return Result.Success(result);
        }

        /// <summary>
        /// 根据Tid适配套餐（剔除 已关联的 套餐）
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<BaoYangPackageVo>>> GetBaoYangPackageByTid(
            [FromQuery] BaoYangPackageByTidRequest request)
        {
            var result = await _baoYangConfigService.GetBaoYangPackageByTid(request);

            return Result.Success(result);
        }

        /// <summary>
        /// 添加保养套餐
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<bool>> RelateVehicleAndPackage([FromBody]ApiRequest<RelateVehicleAndPackageRequest> request)
        {
            var result = await _baoYangConfigService.RelateVehicleAndPackage(request.Data);

            return Result.Success(result);
        }

        /// <summary>
        /// 删除保养套餐
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<bool>> DeleteBaoYangPackageRef([FromBody]ApiRequest<DeleteBaoYangPackageRefRequest> request)
        {
            var result = await _baoYangConfigService.DeleteBaoYangPackageRef(request.Data);

            return Result.Success(result);
        }

        #region 辅料

        /// <summary>
        /// 辅料类型配置
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<BaoYangAccessoryVo>>> GetPartAccessoryConfig()
        {
            var result = await _baoYangConfigService.GetPartAccessoryConfig();

            return Result.Success(result);
        }

        /// <summary>
        /// 辅料配置列表查询
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<BaoYangPartAccessoryVo>>> GetBaoYangPartAccessory(
            [FromQuery] BaoYangPartAccessoryRequest request)
        {
            var result = await _baoYangConfigService.GetBaoYangPartAccessory(request);

            return Result.Success(result);
        }

        /// <summary>
        /// 编辑辅料配置
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<bool>> UpdateAccessory([FromBody]ApiRequest<UpdateAccessory> request)
        {
            var result = await _baoYangConfigService.UpdateAccessory(request.Data);

            return Result.Success(result);
        }

        /// <summary>
        /// 批量编辑辅料配置
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<bool>> BatchEditAccessory([FromBody]ApiRequest<BatchEditAccessoryRequest> request)
        {
            var result = await _baoYangConfigService.BatchEditAccessory(request.Data);

            return Result.Success(result);
        }

        /// <summary>
        /// 删除辅料配置
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<bool>> DeleteAccessory([FromBody]ApiRequest<DeleteAccessoryRequest> request)
        {
            var result = await _baoYangConfigService.DeleteAccessory(request.Data);

            return Result.Success(result);
        }

        #endregion

        /// <summary>
        /// 保养五级属性适配列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<BaoYangPropertyAdaptationVo>>> GetBaoYangPropertyAdaptation(
            [FromQuery] BaoYangPropertyAdaptationRequest request)
        {
            var result = await _baoYangConfigService.GetBaoYangPropertyAdaptation(request);

            return Result.Success(result);
        }

        /// <summary>
        /// 获取保养五级属性描述
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<BaoYangPropertyDescriptionVo>>> GetBaoYangPropertyDescription()
        {
            var result = await _baoYangConfigService.GetBaoYangPropertyDescription();

            return Result.Success(result);
        }

        /// <summary>
        /// 删除五级属性适配
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<bool>> DeletePropertyAdaptation(
            [FromBody] ApiRequest<DeletePropertyAdaptationRequest> request)
        {
            var result = await _baoYangConfigService.DeletePropertyAdaptation(request.Data);

            return Result.Success(result);
        }

        /// <summary>
        /// 保存五级属性适配
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<bool>> SavePropertyAdaptation(
            [FromBody] ApiRequest<SavePropertyAdaptationRequest> request)
        {
            var result = await _baoYangConfigService.SavePropertyAdaptation(request.Data);

            return Result.Success(result);
        }


        #region 保养OE映射关系

        /// <summary>
        /// 保养OE映射关系查询
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiPagedResult<OeCodeMapVo>> GetOeCodeMapList([FromQuery] OeCodeMapRequest request)
        {
            var result = await _baoYangConfigService.GetOeCodeMapList(request);

            return Result.Success(result.Items, result.TotalItems);
        }

        /// <summary>
        /// OE件号详情
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<OePartCodeDetailVo>> GetOeCodeMapDetailByOeCode(
            [FromQuery] OeCodeMapDetailByOeCodeRequest request)
        {
            var result = await _baoYangConfigService.GetOeCodeMapDetailByOeCode(request);

            return Result.Success(result);
        }

        /// <summary>
        /// 删除OE件号
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<bool>> DeleteOePartCode([FromBody] ApiRequest<DeleteOePartCodeRequest> request)
        {
            var result = await _baoYangConfigService.DeleteOePartCode(request.Data);

            return Result.Success(result);
        }

        /// <summary>
        /// 编辑OE件号
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<bool>> EditOePartCode([FromBody] ApiRequest<EditOePartCodeRequest> request)
        {
            var result = await _baoYangConfigService.EditOePartCode(request.Data);

            return Result.Success(result);
        }

        /// <summary>
        /// 添加OE件号
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<bool>> AddOePartCode([FromBody] ApiRequest<AddOePartCodeRequest> request)
        {
            var result = await _baoYangConfigService.AddOePartCode(request.Data);

            return Result.Success(result);
        }

        #endregion

        #region 配件号关联商品

        /// <summary>
        /// 配件号关联产品查询
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiPagedResult<BaoYangPartProductVo>> GetBaoYangProductRef(
            [FromQuery] BaoYangProductRefRequest request)
        {
            var result = await _baoYangConfigService.GetBaoYangProductRef(request);

            return Result.Success(result.Items, result.TotalItems);
        }

        /// <summary>
        /// 删除 配件号产品关联关系
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<bool>> DeleteBaoYangProductRef(
            [FromBody] ApiRequest<DeleteBaoYangProductRefRequest> request)
        {
            var result = await _baoYangConfigService.DeleteBaoYangProductRef(request.Data);

            return Result.Success(result);
        }

        /// <summary>
        /// 配件号关联商品pid
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<bool>> InsertPartsAssociation(
            [FromBody] ApiRequest<InsertPartsAssociationRequest> request)
        {
            var result = await _baoYangConfigService.InsertPartsAssociation(request.Data);

            return Result.Success(result);
        }

        /// <summary>
        /// 校验配件号
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<string>>> CheckPartCode([FromQuery] CheckPartCodeRequest request)
        {
            var result = await _baoYangConfigService.CheckPartCode(request);

            return Result.Success(result);
        }

        /// <summary>
        /// 校验商品pid
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<CheckProductResponse>> CheckProduct([FromQuery] CheckProductRequest request)
        {
            var result = await _baoYangConfigService.CheckProduct(request);

            return Result.Success(result);
        }

        /// <summary>
        /// 校验配件号 商品是否匹配
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<CheckPartCodeResultResponse>> CheckPartCodeAndProduct(
            [FromQuery] CheckPartCodeAndProductRequest request)
        {
            var result = await _baoYangConfigService.CheckPartCodeAndProduct(request);

            return Result.Success(result);
        }

        #endregion

        /// <summary>
        /// PackageType配置列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiPagedResult<BaoYangPackageConfigVo>> GetPackageTypeConfig(
            [FromQuery] PackageTypeConfigRequest request)
        {
            var result = await _baoYangConfigService.GetPackageTypeConfig(request);

            return Result.Success(result.Items, result.TotalItems);
        }

        /// <summary>
        /// 编辑PackageType
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<bool>> EditPackageTypeConfig(
            [FromBody] ApiRequest<EditPackageTypeConfigRequest> request)
        {
            var result = await _baoYangConfigService.EditPackageTypeConfig(request.Data);

            return Result.Success(result);
        }

        /// <summary>
        /// 新增PackageType
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<int>> AddPackageTypeConfig(
            [FromBody] ApiRequest<AddPackageTypeConfigRequest> request)
        {
            var result = await _baoYangConfigService.AddPackageTypeConfig(request.Data);

            return Result.Success(result);
        }

        /// <summary>
        /// BaoYangType配置列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiPagedResult<BaoYangTypeConfigDetailVo>> GetBaoYangTypeConfig(
            [FromQuery] BaoYangTypeConfigRequest request)
        {
            var result = await _baoYangConfigService.GetBaoYangTypeConfig(request);

            return Result.Success(result.Items, result.TotalItems);
        }

        /// <summary>
        /// 获取一级分类
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<BaoYangCategoryConfigVo>>> GetBaoYangCategoryConfig()
        {
            var result = await _baoYangConfigService.GetBaoYangCategoryConfig();

            return Result.Success(result);
        }

        /// <summary>
        /// 编辑BaoYangTypeConfig
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<bool>> EditBaoYangTypeConfig(
            [FromBody] ApiRequest<EditBaoYangTypeConfigRequest> request)
        {
            var result = await _baoYangConfigService.EditBaoYangTypeConfig(request.Data);

            return Result.Success(result);
        }

        /// <summary>
        /// 新增BaoYangTypeConfig
        /// </summary>
        /// <param name="request"></param>
        [HttpPost]
        public async Task<ApiResult<int>> AddBaoYangTypeConfig(
            [FromBody] ApiRequest<AddBaoYangTypeConfigRequest> request)
        {
            var result = await _baoYangConfigService.AddBaoYangTypeConfig(request.Data);

            return Result.Success(result);
        }

        /// <summary>
        /// 获取所有有效的BaoYangType
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<BaoYangTypeConfigVo>>> GetValidBaoYangTypeConfig()
        {
            var result = await _baoYangConfigService.GetValidBaoYangTypeConfig();

            return Result.Success(result);
        }

        /// <summary>
        /// 车型安装服务加价
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiPagedResult<VehicleInstallAddFeeVo>> GetVehicleInstallAddFee(
            [FromQuery] VehicleInstallAddFeeRequest request)
        {
            var result = await _baoYangConfigService.GetVehicleInstallAddFee(request);

            return Result.Success(result.Items, result.TotalItems);
        }

        /// <summary>
        /// 安装服务配置
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiPagedResult<InstallAddFeeConfigVo>> GetInstallAddFeeConfig(
            [FromQuery] InstallAddFeeConfigRequest request)
        {
            var result = await _baoYangConfigService.GetInstallAddFeeConfig(request);

            return Result.Success(result.Items, result.TotalItems);
        }

        /// <summary>
        /// 新增服务加价配置
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<bool>> AddInstallAddFeeConfig(
            [FromBody] ApiRequest<AddInstallAddFeeConfigRequest> request)
        {
            var result = await _baoYangConfigService.AddInstallAddFeeConfig(request.Data);

            return Result.Success(result);
        }

        /// <summary>
        /// 编辑服务加价配置
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<bool>> EditInstallAddFeeConfig(
            [FromBody] ApiRequest<EditInstallAddFeeConfigRequest> request)
        {
            var result = await _baoYangConfigService.EditInstallAddFeeConfig(request.Data);

            return Result.Success(result);
        }

        /// <summary>
        /// 获取保养安装服务
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<BaoYangInstallServiceVo>>> GetBaoYangInstallService()
        {
            var result = await _baoYangConfigService.GetBaoYangInstallService();

            return Result.Success(result);
        }
    }
}