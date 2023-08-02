using Ae.B.Product.Api.Client.Request;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Ae.B.Product.Api.Client.Response;
using Ae.B.Product.Api.Client.Request.BaoYang;
using Ae.B.Product.Api.Client.Model.BaoYang;
using Ae.B.Product.Api.Client.Model;
using ApolloErp.Web.WebApi;

namespace Ae.B.Product.Api.Client.Interfaces
{
    public interface IBaoYangClient
    {
        /// <summary>
        /// 保养适配配置查询
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<List<BaoYangPartAdaptationsResponse>> GetBaoYangPartAdaptationsAsync(
            BaoYangPartAdaptationsRequest request);

        /// <summary>
        /// 获取配件类型
        /// </summary>
        /// <returns></returns>
        Task<List<GetPartNameResponse>> GetPartNameListAsync();

        /// <summary>
        /// 添加配件号
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> AddPartCodeAsync(AddPartCodeRemoteRequest request);

        /// <summary>
        /// 添加特殊配件号
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> AddSpecialPartCodeAsync(AddSpecialPartRemoteRequest request);

        /// <summary>
        /// 批量添加配件
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> BatchAddAdaptationAsync(BatchAddAdaptationRemoteRequest request);

        /// <summary>
        /// 删除配件号
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> DeletePartCodeAsync(DeletePartCodeRemoteRequest request);

        /// <summary>
        /// 修改OE件号
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> UpdateOePartCodeAsync(UpdateOePartCodeRemoteRequest request);

        /// <summary>
        /// 修改零件号
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> UpdatePartCodeAsync(UpdatePartCodeRemoteRequest request);

        /// <summary>
        /// 根据Tid适配套餐（剔除 已关联的 套餐）
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<List<BaoYangPackageDto>> GetBaoYangPackageByTid(BaoYangPackageByTidClientRequest request);

        /// <summary>
        /// 车型关联套餐列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<List<BaoYangPackageRefDto>> GetBaoYangPackageRef(BaoYangPackageRefClientRequest request);

        /// <summary>
        /// 车型添加套餐
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> RelateVehicleAndPackage(RelateVehicleAndPackageClientRequest request);

        /// <summary>
        /// 删除保养套餐
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> DeleteBaoYangPackageRef(DeleteBaoYangPackageRefClientRequest request);

        /// <summary>
        /// 配件类型查询
        /// </summary>
        /// <returns></returns>
        Task<List<BaoYangPartsDto>> GetBaoYangParts();

        /// <summary>
        /// 保养类型
        /// </summary>
        /// <returns></returns>
        Task<List<BaoYangTypeConfigDto>> GetPackageByType();

        /// <summary>
        /// 辅料类型配置
        /// </summary>
        /// <returns></returns>
        Task<List<BaoYangAccessoryDto>> GetPartAccessoryConfig();

        /// <summary>
        /// 查询辅料配置数据
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<List<BaoYangPartAccessoryDto>> GetBaoYangPartAccessory(
            BaoYangPartAccessoryClientRequest request);

        /// <summary>
        /// 五级属性适配列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<List<BaoYangPropertyAdaptationDto>> GetBaoYangPropertyAdaptation(
            BaoYangPropertyAdaptationClientRequest request);

        /// <summary>
        /// 编辑辅料配置
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> UpdateAccessory(UpdateAccessoryClientRequest request);

        /// <summary>
        /// 批量编辑辅料配置
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> BatchEditAccessory(BatchEditAccessoryClientRequest request);

        /// <summary>
        /// 删除辅料配置
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> DeleteAccessory(DeleteAccessoryClientRequest request);

        /// <summary>
        /// 根据OE号查询零件号
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<List<PartCodeDto>> GetPartCodeAndBrandByOe(PartCodeAndBrandByOeClientRequest request);

        /// <summary>
        /// 获取服务项目枚举
        /// </summary>
        /// <returns></returns>
        Task<List<ServiceTypeDTO>> GetServiceTypeEnum();

        /// <summary>
        /// 保养OE映射关系查询
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiPagedResultData<OeCodeMapDto>> GetOeCodeMapList(OeCodeMapClientRequest request);

        /// <summary>
        /// OE件号详情
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<OePartCodeDetailDto> GetOeCodeMapDetailByOeCode(OeCodeMapDetailByOeCodeClientRequest request);

        /// <summary>
        /// 删除OE件号
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> DeleteOePartCode(DeleteOePartCodeClientRequest request);

        /// <summary>
        /// 编辑OE件号
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> EditOePartCode(EditOePartCodeClientRequest request);

        /// <summary>
        /// 添加OE件号
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> AddOePartCode(AddOePartCodeClientRequest request);

        /// <summary>
        /// 获取保养五级属性描述
        /// </summary>
        /// <returns></returns>
        Task<List<BaoYangPropertyDescriptionDto>> GetBaoYangPropertyDescription();

        /// <summary>
        /// 删除五级属性适配
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> DeletePropertyAdaptation(DeletePropertyAdaptationClientRequest request);

        /// <summary>
        /// 保存五级属性适配
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> SavePropertyAdaptation(SavePropertyAdaptationClientRequest request);

        /// <summary>
        /// 配件号关联产品查询
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiPagedResultData<BaoYangPartProductDto>> GetBaoYangProductRef(BaoYangProductRefClientRequest request);

        /// <summary>
        /// 删除 配件号产品关联关系
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> DeleteBaoYangProductRef(DeleteBaoYangProductRefClientRequest request);

        /// <summary>
        /// 配件号关联商品pid
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> InsertPartsAssociation(InsertPartsAssociationClientRequest request);

        /// <summary>
        /// 校验配件号
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<List<string>> CheckPartCode(CheckPartCodeClientRequest request);

        /// <summary>
        /// 校验商品pid
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<CheckProductClientResponse> CheckProduct(CheckProductClientRequest request);

        /// <summary>
        /// 校验配件号 商品是否匹配
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<CheckPartCodeResultClientResponse> CheckPartCodeAndProduct(CheckPartCodeAndProductClientRequest request);

        /// <summary>
        /// 保养适配首页接口
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult<List<BaoYangCategoryDto>>> GetBaoYangPackagesAsync(
            BaoYangPackagesClientRequest request);

        /// <summary>
        /// 更多商品列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<List<BaoYangPackageProductDto>> SearchPackageProductsWithConditionAsync(
            SearchProductClientRequest request);

        /// <summary>
        /// PackageType配置列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiPagedResultData<BaoYangPackageConfigDto>> GetPackageTypeConfig(
            PackageTypeConfigClientRequest request);

        /// <summary>
        /// 编辑PackageType
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> EditPackageTypeConfig(EditPackageTypeConfigClientRequest request);

        /// <summary>
        /// 新增PackageType
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<int> AddPackageTypeConfig(AddPackageTypeConfigClientRequest request);

        /// <summary>
        /// BaoYangType配置列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiPagedResultData<BaoYangTypeConfigDetailDto>> GetBaoYangTypeConfig(
            BaoYangTypeConfigClientRequest request);

        /// <summary>
        /// 编辑BaoYangTypeConfig
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> EditBaoYangTypeConfig(EditBaoYangTypeConfigClientRequest request);

        /// <summary>
        /// 新增BaoYangTypeConfig
        /// </summary>
        /// <param name="request"></param>
        Task<int> AddBaoYangTypeConfig(AddBaoYangTypeConfigClientRequest request);

        /// <summary>
        /// 获取所有有效的BaoYangType
        /// </summary>
        /// <returns></returns>
        Task<List<BaoYangTypeConfigDto>> GetValidBaoYangTypeConfig();

        /// <summary>
        /// 获取一级分类
        /// </summary>
        /// <returns></returns>
        Task<List<BaoYangCategoryConfigDto>> GetBaoYangCategoryConfig();

        /// <summary>
        /// 产品新增/更新 自动更新配件号关联Pid
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> AutoInsertPartsAssociation(AutoInsertPartsAssociationClientRequest request);

        /// <summary>
        /// 安装服务配置
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiPagedResultData<InstallAddFeeConfigDto>> GetInstallAddFeeConfig(
            InstallAddFeeConfigClientRequest request);

        /// <summary>
        /// 新增服务加价配置
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> AddInstallAddFeeConfig(AddInstallAddFeeConfigClientRequest request);

        /// <summary>
        /// 编辑服务加价配置
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> EditInstallAddFeeConfig(EditInstallAddFeeConfigClientRequest request);

        /// <summary>
        /// 获取所有配置
        /// </summary>
        /// <returns></returns>
        Task<List<InstallAddFeeConfigDto>> GetAllInstallAddFeeConfig();
    }
}
