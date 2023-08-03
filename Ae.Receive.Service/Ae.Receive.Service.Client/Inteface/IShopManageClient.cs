using ApolloErp.Web.WebApi;
using Ae.Receive.Service.Client.Model.Shop;
using Ae.Receive.Service.Client.Request;
using Ae.Receive.Service.Client.Request.Shop;
using Ae.Receive.Service.Client.Response;
using Ae.Receive.Service.Client.Response.Shop;
using Ae.Receive.Service.Core.Request.Arrival;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Receive.Service.Client.Inteface
{
    public interface IShopManageClient
    {
        Task<GetShopSimpleInfoClientResponse> GetShopSimpleInfo(GetShopSimpleInfoClientRequest request);
        Task<List<ShopSimpleInfoDTO>> GetShopSimpleList(GetShopListClientRequest request);
        Task<List<BaseServiceDTO>> GetShopServiceCategory(GetShopSimpleInfoClientRequest request);

        /// <summary>
        /// 用户关联门店
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> AddShopUserRelation(AddShopUserRelationRequest request);

        /// <summary>
        /// 得到门店服务
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<List<GetShopServiceTypeAsyncResponse>> GetShopServiceTypeAsync(GetShopServiceTypeAsyncRequest request);

        /// <summary>
        /// 更新客户最后到店时间
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> UpdateUserLastReceiveTime(UpdateUserLastReceiveTimeRequest request);

        /// <summary>
        /// 得到技师信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<List<GetTechnicianPageResponse>> GetTechnicianPage(GetTechnicianPageRequest request);

        /// <summary>
        /// 门店列表查询
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<List<ShopListDto>> GetShopListByIdsAsync(ShopListByIdsAsyncClientRequest request);

        /// <summary>
        /// 得到门店员工信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<List<GetTechnicianPageResponse>> GetShopEmployeeByJobIdPage(GetShopEmployeeByJobIdPageRequest request);
    }
}
