using ApolloErp.Web.WebApi;
using Ae.Shop.Service.Core.Model;
using Ae.Shop.Service.Core.Request;
using Ae.Shop.Service.Core.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Shop.Service.Core.Interfaces
{
    /// <summary>
    /// 员工绩效
    /// </summary>
    public interface IEmployeePerformanceService
    {
        /// <summary>
        /// 新增/保存基础服务配置(先delete 在新增)
        /// </summary>
        /// <returns></returns>
        Task<ApiResult<string>> CreateOrUpdateBasicPerformanceConfig(CreateBasicPerformanceConfigRequest request);

        /// <summary>
        /// 更新安装服务总额绩效配置(更新开关  如果直接关掉 并且表里面没数据 
        /// 需初始化(数值都是0) 并设定总开关是关闭   如果存在数据  就批量更新掉开关)
        /// </summary>
        /// <returns></returns>
        Task<ApiResult<string>> UpdateBasicPerformanceFlag(UpdateBasicPerformanceFlagRequest request);


        /// <summary>
        /// 获取基础服务配置(表存在数据 直接组装  不存在 组装门店的服务类型返回前台)
        /// </summary>
        /// <returns></returns>
        Task<ApiResult<GetBasicPerformanceConfigResponse>> GetBasicPerformanceConfig(GetBasicPerformanceConfigRequest request);

        /// <summary>
        /// 新增门店绩效服务配置
        /// </summary>
        /// <returns></returns>
        Task<ApiResult<string>> CreateShopPerformanceConfig(ShopPerformanceConfigDTO request);

        /// <summary>
        /// 更新门店绩效配置
        /// </summary>
        /// <returns></returns>
        Task<ApiResult<string>> UpdateShopPerformanceConfig(ShopPerformanceConfigDTO request);

        /// <summary>
        /// 删除门店绩效配置
        /// </summary>
        /// <returns></returns>
        Task<ApiResult<string>> DeleteShopPerformanceConfig(ShopPerformanceConfigDTO request);

        /// <summary>
        /// 获取门店绩效配置
        /// </summary>
        /// <returns></returns>
        Task<ApiResult<List<ShopPerformanceConfigDTO>>> GetShopPerformanceConfig(GetBasicPerformanceConfigRequest request);


        /// <summary>
        /// 新增/保存拉新配置
        /// </summary>
        /// <returns></returns>
        Task<ApiResult<string>> CreateOrUpdatePullNewConfig(CreateOrUpdatePullNewConfigRequest request);

        /// <summary>
        /// 更新拉新配置  注意更新的区分 可能开了新客奖励的 可能开了首次到店消费的
        /// </summary>
        /// <returns></returns>
        Task<ApiResult<string>> UpdatePullNewFlag(UpdatePullNewFlagRequest request);

        /// <summary>
        /// 获取拉新配置 存在数据 就直接返回  不存在初始化一个记录给前端
        /// </summary>
        /// <returns></returns>
        Task<ApiResult<GetPullNewConfigResponse>> GetPullNewConfig(GetPullNewConfigRequest request);


        Task<ApiResult<string>> CollectEmployeePerformanceReport();

        /// <summary>
        /// 获取员工绩效列表
        /// </summary>
        /// <returns></returns>
        Task<List<EmployeePerformanceDto>> GetEmployeePerformanceList(EmployeePerformanceRequest request);


        /// <summary>
        /// 获取员工绩效详情
        /// </summary>
        /// <returns></returns>
        Task<EmployeePerformanceDetialDto> GetEmployeePerformanceDetial(EmployeePerformanceDetialRequest request);

        /// <summary>
        /// 获取技师绩效列表V2
        /// </summary>
        /// <returns></returns>
        Task<List<TechPerformanceDto>> GetTechPerformanceList(TechPerformanceRequest request);

        /// <summary>
        /// 获取技师绩效明细V2
        /// </summary>
        /// <returns></returns>
        Task<List<TechPerformanceDetailDto>> GetTechPerformanceDetail(TechPerformanceDetailRequest request);

    }
}
