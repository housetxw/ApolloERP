using ApolloErp.Web.WebApi;
using Ae.ShopOrder.Service.Client.Model.Shop;
using Ae.ShopOrder.Service.Client.Request.Shop;
using Ae.ShopOrder.Service.Core.Model;
using Ae.ShopOrder.Service.Core.Request.Arrival;
using Ae.ShopOrder.Service.Core.Request.Order;
using Ae.ShopOrder.Service.Core.Request.Shop;
using Ae.ShopOrder.Service.Core.Response.Order;
using Ae.ShopOrder.Service.Core.Response.Shop;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.ShopOrder.Service.Client.Clients.ShopServer
{
    public interface IShopClient
    {
        /// <summary>
        /// 获取门店单表信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult<ShopDTO>> GetShopById(GetShopRequest request);

        Task<ApiResult<List<GetShopServiceListWithPIDResponse>>> GetShopServiceListWithPID(GetShopServiceListWithPIDRequest request);

        /// <summary>
        /// 得到技师信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<List<GetTechnicianPageResponse>> GetTechnicianPage(GetTechnicianPageRequest request);

        /// <summary>
        /// 获取门店配置表信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult<ShopConfigDTO>> GetShopConfigByShopId(GetShopRequest request);

        /// <summary>
        /// 查询门店简单信息-列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiPagedResult<ShopSimpleInfoDTO>> GetShopListAsync(GetShopListRequest request);

        /// <summary>
        /// 更新门店的押金
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult> UpdateShopDeposit(UpdateShopDepositRequest request);

        /// <summary>
        /// 更新公司的押金
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult<bool>> OperateCompanyDeposit(OperateCompanyDepositRequest request);

        Task<ApiResult<EmployeeInfoDTO>> GetEmployeeInfo(EmployeeInfoRequest request);

        /// <summary>
        /// 得到门店员工信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<List<GetTechnicianPageResponse>> GetShopEmployeeByJobIdPage(GetShopEmployeeByJobIdPageRequest request);

        Task<ApiResult<List<ShopGrouponProductDTO>>> GetShopGrouponProduct(GetShopGrouponProductRequest request);
        Task<ApiResult<List<ShopPerformanceConfigDTO>>> GetShopPerformanceConfig(GetShopRequest request);
    }
}
