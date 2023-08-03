using ApolloErp.Web.WebApi;
using Ae.Shop.Api.Core.Model;
using Ae.Shop.Api.Core.Model.ShopPurchase;
using Ae.Shop.Api.Core.Request;
using Ae.Shop.Api.Core.Request.Porduct;
using Ae.Shop.Api.Core.Request.ShopPurchase;
using Ae.Shop.Api.Core.Response.Product;
using Ae.Shop.Api.Core.Response.ShopPurchase;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Shop.Api.Core.Interfaces
{
    public interface IShopPurchaseService
    {
        /// <summary>
        ///  获取所有的供应商信息
        /// </summary>
        /// <returns></returns>
        Task<ApiResult<List<VenderDTO>>> GetSupplierList();

        /// <summary>
        /// 分页查询门店商品信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiPagedResult<PurchaseOrderDTO>> SearchPurchaseOrder(PurchaseOrderSearchRequest request);

        /// <summary>
        /// 编辑采购单
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult<bool>> SavePurchaseOrder(PurchaseOrderEditRequest request);

        /// <summary>
        /// 获取采购单信息
        /// </summary>
        /// <param name="purchaseOrderId">采购单Id</param>
        /// <returns></returns>
        Task<ApiResult<PurchaseOrderDetailResponse>> GetPurchaseOrderDetail(string purchaseOrderId);
        /// <summary>
        /// 搜索商品
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ProductSearchResponse> SearchProduct(ProductSearchRequest request);
        Task<ApiPagedResult<VenderVO>> SearchVender(SearchVenderRequest request);
        Task<ApiResult> UpsertVender(VenderVO data);
        Task<ApiResult<VenderVO>> GetVender(BaseGetByIdRequest request);
        Task<ApiResult> ActiveVender(VenderVO data);

        Task<ApiResult<bool>> UpdateShopPurchaseStatusById(long purchaseOrderId, int status, string createBy);

        /// <summary>
        /// 采购单发货
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult<string>> UpdatePurchaseOrderDeliveryInfo(PurchaseOrderDTO request);
        Task<ApiResult<string>> UpdatePurchasePrice(List<PurchaseOrderProductDTO> request);

        Task<ApiResult<string>> DeletePurchaseOrder(PurchaseOrderDTO request);

        Task<ApiResult<string>> QuickCreateVender(VenderDTO request);

        Task<ApiResult<List<VenderDTO>>> GetVenders(VenderDTO request);

        Task<ApiResult<string>> CreatePurchaseOrder(PurchaseOrderEditRequest request);

        Task<ApiResult<string>> PurchaseInStock(PurchaseOrderDTO request, bool isSyncFms = true);

        Task<ApiResult<string>> CreatePurchaseOrderFouOut(PurchaseOrderEditRequest request);

        Task<ApiResult<string>> SendPurchaseOrder(PurchaseOrderBatchSendDTO request);

        Task<List<PurchaseOrderViewDTO>> SelectOutPurchaseOrders(SelectOutPurchaseOrdersRequest request);

        Task<PurchaseOrderPayDTO> GetPurchaseOrderPayInfo(PurchaseOrderViewDTO request);

        Task<ApiResult<string>> SavePayInfo(PurchaseOrderDTO request);

        Task<ApiResult<string>> UpsertPurchaseMonthPayInfo(PurchaseMonthPayDTO request);

        Task<ApiPagedResult<PurchaseMonthPayDTO>> SelectPurchaseMonthPayInfos(PurchaseMonthPayRequest request);

        Task<ApiResult<string>> AuditPurchaseMonthPay(PurchaseMonthPayDTO request);

        Task<ApiResult<string>> SavePurchaseMonthPay(PurchaseMonthPayDTO request);

        Task<ApiResult<GetTargetVenderPurchaseOrderResponse>> GetVenderPurchaseOrders(GetVenderPurchaseOrdersRequest request);


        Task<ApiResult<List<AddPurchasePayInfo>>> GetTargetPurchaseOrders(PurchaseMonthPayDTO request);


        Task<ApiResult<PurchaseOrderDTO>> GetPurchasePayInfo(PurchaseOrderViewDTO request);

        Task<ApiResult<string>> PurchaseReturn(PurchaseOrderViewDTO request);
        Task<ApiResult<string>> PurchaseReturnPart(PurchaseOrderEditRequest request);

        Task<ApiResult<string>> ReleasePurchaseOccupyStock(PurchaseOrderViewDTO request);
        Task<ApiResult<string>> ReleasePurchaseOccupyStock_s(PurchaseOrderViewDTO request);

        Task<ApiResult<string>> UpdatePurchaseDeliveryCode(PurchaseOrderDTO request);
    }
}
