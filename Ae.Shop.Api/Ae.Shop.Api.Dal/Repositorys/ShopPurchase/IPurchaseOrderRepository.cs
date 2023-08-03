using ApolloErp.Data.DapperExtensions;
using Ae.Shop.Api.Core.Model;
using Ae.Shop.Api.Core.Request.ShopPurchase;
using Ae.Shop.Api.Dal.Model.ShopPurchase;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Shop.Api.Dal.Repositorys.ShopPurchase
{
    public interface IPurchaseOrderRepository : IRepository<PurchaseOrderDO>
    {
        Task<int> UpdatePurchaseOrderDeliveryInfo(PurchaseOrderDO request);
        Task<int> UpdatePurchasePrice(List<PurchaseOrderProductDO> request);

        Task<int> DeletePurchaseOrder(PurchaseOrderDO request);

        Task<int> UpdatePurchaseOrderPayType(List<long> purchaseIds, PurchaseOrderDO request);

        Task<List<PurchaseOrderViewDTO>> SelectPurchaseOrderViewPages(SelectOutPurchaseOrdersRequest request);

        Task<List<TempPurchaseOrderInfo>> SelectPurchasePayInfos(List<long> shopIds,long venderId, List<string> times);

        Task<int> BatchUpdatePurchasePayStatus(List<string> purchaseIds, PurchaseOrderDO request);

        Task<int> BatchReleasePurchasePayStatus(List<string> purchaseIds, string updateBy);

         Task<int> BatchUpdatePurchasePayStatusToAudit(List<string> purchaseIds, string updateBy);

        Task<List<TempPurchaseOrderInfo>> GetTargetPurchaseOrders(List<string> purchaseIds);

    }
}
