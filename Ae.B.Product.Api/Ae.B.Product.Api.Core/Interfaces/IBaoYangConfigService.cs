using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ApolloErp.Web.WebApi;
using Ae.B.Product.Api.Core.Model;
using Ae.B.Product.Api.Core.Model.BaoYang;
using Ae.B.Product.Api.Core.Request;
using Ae.B.Product.Api.Core.Request.BaoYang;
using Ae.B.Product.Api.Core.Response;
using Ae.B.Product.Api.Core.Response.BaoYang;

namespace Ae.B.Product.Api.Core.Interfaces
{
    /// <summary>
    /// 保养配置
    /// </summary>
    public interface IBaoYangConfigService
    {
        /// <summary>
        /// 保养适配配置查询
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<List<GetBaoYangPartAdaptationsResponse>> GetBaoYangPartAdaptationsAsync(
            GetBaoYangPartAdaptationsRequest request);

        /// <summary>
        /// 获取配件类型
        /// </summary>
        /// <returns></returns>
        Task<List<PartNameListResponse>> GetPartNameListAsync();

        /// <summary>
        /// 添加配件号
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> AddPartCodeAsync(AddPartCodeRequest request);

        /// <summary>
        /// 根据OE号查询零件号
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<List<PartCodeVo>> GetPartCodeAndBrandByOe(PartCodeAndBrandByOeRequest request);

        /// <summary>
        /// 添加特殊配件号
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> AddSpecialPartCodeAsync(AddSpecialPartRequest request);

        /// <summary>
        /// 批量添加配件号
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
        /// 配件类型查询
        /// </summary>
        /// <returns></returns>
        Task<List<BaoYangPartsVo>> GetBaoYangParts();

        /// <summary>
        /// 查询车型已绑定套餐
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<BaoYangPackageRefResponse> GetBaoYangPackageRef(BaoYangPackageRefRequest request);

        /// <summary>
        /// 获取套餐baoYangType
        /// </summary>
        /// <returns></returns>
        Task<List<BaoYangTypeConfigVo>> GetPackageByType();

        /// <summary>
        /// 根据Tid适配套餐（剔除 已关联的 套餐）
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<List<BaoYangPackageVo>> GetBaoYangPackageByTid(BaoYangPackageByTidRequest request);

        /// <summary>
        /// 添加保养套餐
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
        /// 辅料类型配置
        /// </summary>
        /// <returns></returns>
        Task<List<BaoYangAccessoryVo>> GetPartAccessoryConfig();

        /// <summary>
        /// 辅料配置列表查询
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<List<BaoYangPartAccessoryVo>> GetBaoYangPartAccessory(BaoYangPartAccessoryRequest request);

        /// <summary>
        /// 编辑辅料配置
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> UpdateAccessory(UpdateAccessory request);

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
        /// 保养五级属性适配列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<List<BaoYangPropertyAdaptationVo>> GetBaoYangPropertyAdaptation(BaoYangPropertyAdaptationRequest request);

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
        /// 配件号关联产品查询
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiPagedResultData<BaoYangPartProductVo>> GetBaoYangProductRef(BaoYangProductRefRequest request);

        /// <summary>
        /// 删除 配件号产品关联关系
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> DeleteBaoYangProductRef(DeleteBaoYangProductRefRequest request);

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
        /// PackageType配置列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiPagedResultData<BaoYangPackageConfigVo>> GetPackageTypeConfig(
            PackageTypeConfigRequest request);

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
        Task<ApiPagedResultData<BaoYangTypeConfigDetailVo>> GetBaoYangTypeConfig(
            BaoYangTypeConfigRequest request);

        /// <summary>
        /// 获取一级分类
        /// </summary>
        /// <returns></returns>
        Task<List<BaoYangCategoryConfigVo>> GetBaoYangCategoryConfig();

        /// <summary>
        /// 编辑BaoYangTypeConfig
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> EditBaoYangTypeConfig(EditBaoYangTypeConfigRequest request);

        /// <summary>
        /// 新增BaoYangTypeConfig
        /// </summary>
        /// <param name="request"></param>
        Task<int> AddBaoYangTypeConfig(AddBaoYangTypeConfigRequest request);

        /// <summary>
        /// 获取所有有效的BaoYangType
        /// </summary>
        /// <returns></returns>
        Task<List<BaoYangTypeConfigVo>> GetValidBaoYangTypeConfig();

        /// <summary>
        /// 车型安装服务加价
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiPagedResultData<VehicleInstallAddFeeVo>> GetVehicleInstallAddFee(
            VehicleInstallAddFeeRequest request);

        /// <summary>
        /// 安装服务配置
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiPagedResultData<InstallAddFeeConfigVo>> GetInstallAddFeeConfig(
            InstallAddFeeConfigRequest request);

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

        /// <summary>
        /// 获取保养安装服务
        /// </summary>
        /// <returns></returns>
        Task<List<BaoYangInstallServiceVo>> GetBaoYangInstallService();
    }
}
