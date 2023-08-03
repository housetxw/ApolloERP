using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ApolloErp.Web.WebApi;
using Ae.ShopOrder.Service.Client.Model.WMS;
using Ae.ShopOrder.Service.Client.Request.WMS;
using Ae.ShopOrder.Service.Core.Model.Order;
using Ae.ShopOrder.Service.Core.Request.Order;
using Ae.ShopOrder.Service.Core.Response.Order;

namespace Ae.ShopOrder.Service.Client.Clients.WMS
{
    /// <summary>
    /// WMS
    /// </summary>
    public interface IWMSClient
    {
        /// <summary>
        /// 得到WMS出库信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult<GetWareHouseTransferClientDTO>> GetWarehouseTransferAllTask(GetWareHouseTransferClientRequest request);

        /// <summary>
        /// 得到物流信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult<List<GetWareHouseTransferPackageClientDTO>>> GetTransferPackageTask(GetWareHouseTransferPackageClientRequest request);

        /// <summary>
        /// 签收接口
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult> UpdateWarehouseTransferSignUp(UpdateWarehouseTransferSignUpClientRequest request);

        /// <summary>
        /// 入库接口
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult> UpdateWarehouseTransferProductReceiveNum(UpdateWarehouseTransferProductReceiveNumClientRequest request);

        /// <summary>
        /// 更新包裹
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult> UpdateWarehouseTransferPackageStatus(UpdateWarehouseTransferPackageStatusClientRequest request);

        /// <summary>
        /// 得到物流信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult<List<GetBatchWarehouseTransferPackagesDTO>>> GetBatchWarehouseTransferPackages(GetBatchWarehouseTransferPackagesRequest request);

        Task<ApiResult<List<GetShopOccupyMappingsByRelationIdResponse>>> GetShopOccupyMappingsByRelationId(GetShopOccupyMappingsByRelationIdRequest request);

        Task<ApiResult<List<GetOrderOccupyShopProductPurchaseResponse>>> GetOrderOccupyShopProductPurchaseInfo(
            GetOrderOccupyShopProductPurchaseInfoReqeust reqeust);


    }
}
