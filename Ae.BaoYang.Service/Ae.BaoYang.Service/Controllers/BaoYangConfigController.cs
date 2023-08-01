using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ApolloErp.Web.WebApi;
using Ae.BaoYang.Service.Core.Interfaces;
using Ae.BaoYang.Service.Core.Model.Config;
using Ae.BaoYang.Service.Core.Request;
using Ae.BaoYang.Service.Core.Request.Config;
using Ae.BaoYang.Service.Core.Response;
using Ae.BaoYang.Service.Core.Response.Config;
using Ae.BaoYang.Service.Filters;

namespace Ae.BaoYang.Service.Controllers
{
    /// <summary>
    /// 保养适配配置相关接口
    /// </summary>
    [Route("[controller]/[action]")]
    // [Filter(nameof(BaoYangConfigController))]
    public class BaoYangConfigController : ControllerBase
    {
        private readonly IBaoYangConfigService _baoYangConfigService;

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="baoYangConfigService"></param>
        public BaoYangConfigController(IBaoYangConfigService baoYangConfigService)
        {
            _baoYangConfigService = baoYangConfigService;
        }

        /// <summary>
        /// 获取配件类型
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<GetPartNameResponse>>> GetPartNameList()
        {
            var result = await _baoYangConfigService.GetPartNameListAsync();
            return Result.Success(result);
        }

        /// <summary>
        /// 保养适配配置查询
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<List<GetBaoYangPartAdaptationsResponse>>> GetBaoYangPartAdaptations(
            [FromBody] GetBaoYangPartAdaptationsRequest request)
        {
            var result = await _baoYangConfigService.GetBaoYangPartAdaptationsAsync(request);
            return Result.Success(result);
        }

        /// <summary>
        /// 添加配件号
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<bool>> AddPartCode([FromBody] AddPartCodeRequest request)
        {
            var result = await _baoYangConfigService.AddPartCodeAsync(request);

            return Result.Success(result);
        }

        /// <summary>
        /// 添加特殊配件号
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<bool>> AddSpecialPartCode([FromBody] AddSpecialPartRequest request)
        {
            var result = await _baoYangConfigService.AddSpecialPartCodeAsync(request);

            return Result.Success(result);
        }

        /// <summary>
        /// 批量添加配件号
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<bool>> BatchAddAdaptation([FromBody] BatchAddAdaptationRequest request)
        {
            var result = await _baoYangConfigService.BatchAddAdaptationAsync(request);

            return Result.Success(result);
        }

        /// <summary>
        /// 删除配件号
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<bool>> DeletePartCode([FromBody] DeletePartCodeRequest request)
        {
            var result = await _baoYangConfigService.DeletePartCodeAsync(request);

            return Result.Success(result);
        }

        /// <summary>
        /// 修改OE件号
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<bool>> UpdateOePartCode([FromBody] UpdateOePartCodeRequest request)
        {
            var result = await _baoYangConfigService.UpdateOePartCodeAsync(request);

            return Result.Success(result);
        }

        /// <summary>
        /// 修改零件号
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<bool>> UpdatePartCode([FromBody] UpdatePartCodeRequest request)
        {
            var result = await _baoYangConfigService.UpdatePartCodeAsync(request);

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
        /// 辅料类型配置
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<BaoYangAccessory>>> GetPartAccessoryConfig()
        {
            var result = await _baoYangConfigService.GetPartAccessoryConfig();

            return Result.Success(result?.PartAccessories ?? new List<BaoYangAccessory>());
        }

        /// <summary>
        /// 查询辅料配置数据
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<List<BaoYangPartAccessory>>> GetBaoYangPartAccessory(
            [FromBody] BaoYangPartAccessoryRequest request)
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
        public async Task<ApiResult<bool>> UpdateAccessory([FromBody] UpdateAccessoryRequest request)
        {
            var result = await _baoYangConfigService.UpdateAccessory(request);

            return Result.Success(result);
        }

        /// <summary>
        /// 批量编辑辅料配置
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<bool>> BatchEditAccessory([FromBody] BatchEditAccessoryRequest request)
        {
            var result = await _baoYangConfigService.BatchEditAccessory(request);

            return Result.Success(result);
        }

        /// <summary>
        /// 删除辅料配置
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<bool>> DeleteAccessory(DeleteAccessoryRequest request)
        {
            var result = await _baoYangConfigService.DeleteAccessory(request);

            return Result.Success(result);
        }

        /// <summary>
        /// 配件号关联商品pid
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<bool>> InsertPartsAssociation([FromBody] InsertPartsAssociationRequest request)
        {
            var result = await _baoYangConfigService.InsertPartsAssociation(request);

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

        /// <summary>
        /// 产品新增/更新 自动更新配件号关联Pid
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<bool>> AutoInsertPartsAssociation(
            [FromBody] AutoInsertPartsAssociationRequest request)
        {
            var result = await _baoYangConfigService.AutoInsertPartsAssociation(request);

            return Result.Success(result);
        }

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

            return Result.Success(result.Item1, result.Item2);
        }

        /// <summary>
        /// 删除 配件号产品关联关系
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<bool>> DeleteBaoYangProductRef([FromBody] DeleteBaoYangProductRefRequest request)
        {
            var result = await _baoYangConfigService.DeleteBaoYangProductRef(request);

            return Result.Success(result);
        }

        /// <summary>
        /// 套餐适配默认车型
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<bool>> AdaptiveDefaultCarsByPackageId(
            [FromBody] AdaptiveDefaultCarsByPackageIdRequest request)
        {
            var result = await _baoYangConfigService.AdaptiveDefaultCarsByPackageId(request);

            return Result.Success(result);
        }

        /// <summary>
        /// 车型添加套餐
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<bool>> RelateVehicleAndPackage([FromBody] RelateVehicleAndPackageRequest request)
        {
            var result = await _baoYangConfigService.RelateVehicleAndPackage(request);

            return Result.Success(result);
        }

        /// <summary>
        /// 删除保养套餐
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<bool>> DeleteBaoYangPackageRef([FromBody] DeleteBaoYangPackageRefRequest request)
        {
            var result = await _baoYangConfigService.DeleteBaoYangPackageRef(request);

            return Result.Success(result);
        }

        /// <summary>
        /// 车型关联套餐列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<List<BaoYangPackageRefVo>>> GetBaoYangPackageRef(
            [FromBody] BaoYangPackageRefRequest request)
        {
            var result = await _baoYangConfigService.GetBaoYangPackageRef(request);

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
        /// 获取套餐baoYangType
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult<List<BaoYangTypeConfigVo>>> GetPackageByType()
        {
            var result = await _baoYangConfigService.GetPackageByType();

            return Result.Success(result);
        }

        #region 五级属性适配

        /// <summary>
        /// 五级属性适配列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<List<BaoYangPropertyAdaptationVo>>> GetBaoYangPropertyAdaptation(
            [FromBody] BaoYangPropertyAdaptationRequest request)
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
        public async Task<ApiResult<bool>> DeletePropertyAdaptation([FromBody] DeletePropertyAdaptationRequest request)
        {
            var result = await _baoYangConfigService.DeletePropertyAdaptation(request);

            return Result.Success(result);
        }

        /// <summary>
        /// 保存五级属性适配
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<bool>> SavePropertyAdaptation([FromBody] SavePropertyAdaptationRequest request)
        {
            var result = await _baoYangConfigService.SavePropertyAdaptation(request);

            return Result.Success(result);
        }

        #endregion

        /// <summary>
        /// 初始化数据
        /// </summary>
        [HttpPost]
        public void TestIni()
        {
            _baoYangConfigService.TestIni();
        }

        /// <summary>
        /// 初始化车型配件适配 配置数据
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<bool>> InitializeAdaptivePartConfig(IFormFile request)
        {
            string fileName = request.FileName.Split(new[] {'.'})[0];
            var stream = request.OpenReadStream();
            StreamReader sr = new StreamReader(stream);
            string dateTime = DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss");
            FileStream fs = new FileStream($@"C:\{fileName}{dateTime}异常数据.txt", FileMode.OpenOrCreate,
                FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs);
            String line;
            while ((line = sr.ReadLine()) != null)
            {
                var itemVehicle = line.Split(new string[] {"\t"}, StringSplitOptions.None);
                var partCode = itemVehicle[0].Trim();
                var brand = itemVehicle[1].Trim();
                var vehicle = itemVehicle[2].Trim();
                var paiLiang = itemVehicle[3].Trim();
                var startYear = itemVehicle[4].Trim();
                var endYear = itemVehicle[5].Trim();
                var nian = itemVehicle[6].Trim();

                var result = await _baoYangConfigService.InitializeAdaptivePartConfig(partCode, brand, vehicle,
                    paiLiang, startYear, endYear, nian, fileName);
                if (!result)
                {
                    sw.WriteLine(line);
                }
            }

            sr.Close();
            sw.Close();
            fs.Close();

            return Result.Success(true);
        }

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
        public async Task<ApiResult<bool>> DeleteOePartCode([FromBody] DeleteOePartCodeRequest request)
        {
            var result = await _baoYangConfigService.DeleteOePartCode(request);

            return Result.Success(result);
        }

        /// <summary>
        /// 编辑OE件号
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<bool>> EditOePartCode([FromBody] EditOePartCodeRequest request)
        {
            var result = await _baoYangConfigService.EditOePartCode(request);

            return Result.Success(result);
        }

        /// <summary>
        /// 添加OE件号
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<bool>> AddOePartCode([FromBody] AddOePartCodeRequest request)
        {
            var result = await _baoYangConfigService.AddOePartCode(request);

            return Result.Success(result);
        }

        #region Package 配置

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
        /// 编辑PackageType
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<bool>> EditPackageTypeConfig([FromBody] EditPackageTypeConfigRequest request)
        {
            var result = await _baoYangConfigService.EditPackageTypeConfig(request);

            return Result.Success(result);
        }

        /// <summary>
        /// 新增PackageType
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<int>> AddPackageTypeConfig([FromBody] AddPackageTypeConfigRequest request)
        {
            var result = await _baoYangConfigService.AddPackageTypeConfig(request);

            return Result.Success(result);
        }

        /// <summary>
        /// BaoYangType配置列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiPagedResult<BaoYangTypeConfig>> GetBaoYangTypeConfig(
            [FromQuery] BaoYangTypeConfigRequest request)
        {
            var result = await _baoYangConfigService.GetBaoYangTypeConfig(request);

            return Result.Success(result.Items, result.TotalItems);
        }

        /// <summary>
        /// 编辑BaoYangTypeConfig
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<bool>> EditBaoYangTypeConfig([FromBody] EditBaoYangTypeConfigRequest request)
        {
            var result = await _baoYangConfigService.EditBaoYangTypeConfig(request);

            return Result.Success(result);
        }

        /// <summary>
        /// 新增BaoYangTypeConfig
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<int>> AddBaoYangTypeConfig([FromBody] AddBaoYangTypeConfigRequest request)
        {
            var result = await _baoYangConfigService.AddBaoYangTypeConfig(request);

            return Result.Success(result);
        }

        #endregion

        #region 安装服务费加价配置

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
        public async Task<ApiResult<bool>> AddInstallAddFeeConfig([FromBody] AddInstallAddFeeConfigRequest request)
        {
            var result = await _baoYangConfigService.AddInstallAddFeeConfig(request);

            return Result.Success(result);
        }

        /// <summary>
        /// 编辑服务加价配置
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResult<bool>> EditInstallAddFeeConfig([FromBody] EditInstallAddFeeConfigRequest request)
        {
            var result = await _baoYangConfigService.EditInstallAddFeeConfig(request);

            return Result.Success(result);
        }

        #endregion
    }
}