using Ae.Shop.Api.Client.Model;
using Ae.Shop.Api.Client.Request;
using ApolloErp.Web.WebApi;
using System.Threading.Tasks;
using Ae.Shop.Api.Client.Request.ShopManage;
using Ae.Shop.Api.Client.Model.ShopManage;
using System.Collections.Generic;
using Ae.Shop.Api.Core.Request.ShopReport;
using Ae.Shop.Api.Core.Model.ShopReport;
using Ae.Shop.Api.Core.Request.ShopManage;
using Ae.Shop.Api.Core.Request;
using Ae.Shop.Api.Core.Response;

namespace Ae.Shop.Api.Client.Clients
{
    public interface IShopMangeClient
    {
        Task<ApiResult<ShopDTO>> GetShopById(GetShopClientRequest request);

        /// <summary>
        /// 门店客户列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiPagedResultData<ShopCustomerListDto>> GetShopCustomerList(ShopCustomerListClientRequest request);

        /// <summary>
        /// 门店客户详情
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ShopCustomerDetailDto> GetShopCustomerDetail(ShopCustomerDetailClientRequest request);

        /// <summary>
        /// 获取门店开启的服务项目
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<List<ShopServiceTypeDto>> GetShopServiceTypeAsync(ShopServiceTypeClientRequest request);

        /// <summary>
        /// 获取技师列表
        /// </summary>
        /// <param name="shopId"></param>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        Task<List<ShopEmployeeDto>> GetShopTechnicianList(int shopId, string employeeId);

        /// <summary>
        /// 员工绩效
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult<List<EmployeePerformanceDto>>> GetEmployeePerformanceList(EmployeePerformanceRequest request);

        Task<ApiResult<List<CompanyLessInfoDTO>>> GetCompanyAndShopByParentId(GetCompanyInfoByIdRequest request);

        Task<ApiResult<List<ShopSimpleInfoResponse>>> GetShopListByIdsAsync(GetShopListByIdsRequest request);
        Task<ApiPagedResult<ShopSimpleInfoResponse>> GetShopListAsync(GetShopListRequest request);
        Task<ApiResult<List<ShopSimpleInfoResponse>>> GetShopWareHouseListAsync(GetShopListRequest request);
    }
}
