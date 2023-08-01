using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ApolloErp.Web.WebApi;
using Ae.BaoYang.Service.Core.Model.Config;
using Ae.BaoYang.Service.Core.Request;
using Ae.BaoYang.Service.Core.Request.Config;
using Ae.BaoYang.Service.Core.Response;
using Ae.BaoYang.Service.Core.Response.Config;

namespace Ae.BaoYang.Service.Core.Interfaces
{
    /// <summary>
    /// 保养配置Service
    /// </summary>
    public interface IBaoYangConfigService
    {
        /// <summary>
        /// 获取配件类型
        /// </summary>
        /// <returns></returns>
        Task<List<GetPartNameResponse>> GetPartNameListAsync();

        /// <summary>
        /// 保养适配配置查询
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<List<GetBaoYangPartAdaptationsResponse>> GetBaoYangPartAdaptationsAsync(
            GetBaoYangPartAdaptationsRequest request);

        /// <summary>
        /// 添加配件号
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> AddPartCodeAsync(AddPartCodeRequest request);

        /// <summary>
        /// 添加特殊配件号
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> AddSpecialPartCodeAsync(AddSpecialPartRequest request);

        /// <summary>
        /// 批量添加配件
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> BatchAddAdaptationAsync(BatchAddAdaptationRequest request);

        /// <summary>
        /// 删除配件号
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> DeletePartCodeAsync(DeletePartCodeRequest request);

        /// <summary>
        /// 修改OE件号
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> UpdateOePartCodeAsync(UpdateOePartCodeRequest request);

        /// <summary>
        /// 修改零件号
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> UpdatePartCodeAsync(UpdatePartCodeRequest request);

        /// <summary>
        /// 根据OE号查询零件号
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<List<PartCodeVo>> GetPartCodeAndBrandByOe(PartCodeAndBrandByOeRequest request);

        /// <summary>
        /// 辅料类型配置
        /// </summary>
        /// <returns></returns>
        Task<BaoYangAccessoryConfig> GetPartAccessoryConfig();

        /// <summary>
        /// 查询辅料配置数据
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<List<BaoYangPartAccessory>> GetBaoYangPartAccessory(BaoYangPartAccessoryRequest request);

        /// <summary>
        /// 编辑辅料配置
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> UpdateAccessory(UpdateAccessoryRequest request);

        /// <summary>
        /// 批量编辑辅料配置
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> BatchEditAccessory(BatchEditAccessoryRequest request);

        /// <summary>
        /// 删除辅料配置
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> DeleteAccessory(DeleteAccessoryRequest request);

        /// <summary>
        /// 配件号关联商品pid
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> InsertPartsAssociation(InsertPartsAssociationRequest request);

        /// <summary>
        /// 校验配件号
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<List<string>> CheckPartCode(CheckPartCodeRequest request);

        /// <summary>
        /// 校验商品pid
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<CheckProductResponse> CheckProduct(CheckProductRequest request);

        /// <summary>
        /// 校验配件号 商品是否匹配
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<CheckPartCodeResultResponse> CheckPartCodeAndProduct(CheckPartCodeAndProductRequest request);

        /// <summary>
        /// 产品新增/更新 自动更新配件号关联Pid
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> AutoInsertPartsAssociation(AutoInsertPartsAssociationRequest request);

        /// <summary>
        /// 配件号关联产品查询
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<Tuple<List<BaoYangPartProductVo>, int>> GetBaoYangProductRef(BaoYangProductRefRequest request);

        /// <summary>
        /// 删除 配件号产品关联关系
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> DeleteBaoYangProductRef(DeleteBaoYangProductRefRequest request);

        /// <summary>
        /// 套餐适配默认车型
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> AdaptiveDefaultCarsByPackageId(AdaptiveDefaultCarsByPackageIdRequest request);

        /// <summary>
        /// 车型添加套餐
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> RelateVehicleAndPackage(RelateVehicleAndPackageRequest request);

        /// <summary>
        /// 删除保养套餐
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> DeleteBaoYangPackageRef(DeleteBaoYangPackageRefRequest request);

        /// <summary>
        /// 车型关联套餐列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<List<BaoYangPackageRefVo>> GetBaoYangPackageRef(BaoYangPackageRefRequest request);

        /// <summary>
        /// 根据Tid适配套餐（剔除 已关联的 套餐）
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<List<BaoYangPackageVo>> GetBaoYangPackageByTid(BaoYangPackageByTidRequest request);

        /// <summary>
        /// 获取套餐baoYangType
        /// </summary>
        /// <returns></returns>
        Task<List<BaoYangTypeConfigVo>> GetPackageByType();

        /// <summary>
        /// 五级属性适配列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<List<BaoYangPropertyAdaptationVo>> GetBaoYangPropertyAdaptation(
            BaoYangPropertyAdaptationRequest request);

        /// <summary>
        /// 获取保养五级属性描述
        /// </summary>
        /// <returns></returns>
        Task<List<BaoYangPropertyDescriptionVo>> GetBaoYangPropertyDescription();

        /// <summary>
        /// 删除五级属性适配
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> DeletePropertyAdaptation(DeletePropertyAdaptationRequest request);

        /// <summary>
        /// 保存五级属性适配
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> SavePropertyAdaptation(SavePropertyAdaptationRequest request);

        /// <summary>
        /// 初始化数据
        /// </summary>
        void TestIni();

        /// <summary>
        /// 初始化配件数据
        /// </summary>
        /// <param name="partCode"></param>
        /// <param name="brand"></param>
        /// <param name="vehicle"></param>
        /// <param name="paiLiang"></param>
        /// <param name="startNian"></param>
        /// <param name="endNian"></param>
        /// <param name="nian"></param>
        /// <param name="partName"></param>
        /// <returns></returns>
        Task<bool> InitializeAdaptivePartConfig(string partCode, string brand, string vehicle,
            string paiLiang, string startNian, string endNian, string nian, string partName);

        /// <summary>
        /// 保养OE映射关系查询
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiPagedResultData<OeCodeMapVo>> GetOeCodeMapList(OeCodeMapRequest request);

        /// <summary>
        /// OE件号详情
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<OePartCodeDetailVo> GetOeCodeMapDetailByOeCode(OeCodeMapDetailByOeCodeRequest request);

        /// <summary>
        /// 删除OE件号
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> DeleteOePartCode(DeleteOePartCodeRequest request);

        /// <summary>
        /// 编辑OE件号
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> EditOePartCode(EditOePartCodeRequest request);

        /// <summary>
        /// 添加OE件号
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> AddOePartCode(AddOePartCodeRequest request);

        /// <summary>
        /// PackageType配置列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiPagedResultData<BaoYangPackageConfigVo>> GetPackageTypeConfig(PackageTypeConfigRequest request);

        /// <summary>
        /// 获取所有有效的BaoYangType
        /// </summary>
        /// <returns></returns>
        Task<List<BaoYangTypeConfigVo>> GetValidBaoYangTypeConfig();

        /// <summary>
        /// 获取一级分类
        /// </summary>
        /// <returns></returns>
        Task<List<BaoYangCategoryConfigVo>> GetBaoYangCategoryConfig();

        /// <summary>
        /// 编辑PackageType
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> EditPackageTypeConfig(EditPackageTypeConfigRequest request);

        /// <summary>
        /// 新增PackageType
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<int> AddPackageTypeConfig(AddPackageTypeConfigRequest request);

        /// <summary>
        /// BaoYangType配置列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiPagedResultData<BaoYangTypeConfig>> GetBaoYangTypeConfig(BaoYangTypeConfigRequest request);

        /// <summary>
        /// 编辑BaoYangType
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> EditBaoYangTypeConfig(EditBaoYangTypeConfigRequest request);

        /// <summary>
        /// 新增BaoYangTypeConfig
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<int> AddBaoYangTypeConfig(AddBaoYangTypeConfigRequest request);

        /// <summary>
        /// 安装服务配置
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiPagedResultData<InstallAddFeeConfigVo>> GetInstallAddFeeConfig(InstallAddFeeConfigRequest request);

        /// <summary>
        /// 新增服务加价配置
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> AddInstallAddFeeConfig(AddInstallAddFeeConfigRequest request);

        /// <summary>
        /// 编辑服务加价配置
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> EditInstallAddFeeConfig(EditInstallAddFeeConfigRequest request);
    }
}
