using ApolloErp.Data.DapperExtensions;
using Ae.Shop.Api.Dal.Model.ShopPurchase;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Shop.Api.Dal.Repositorys.ShopPurchase
{
    public interface IPurchaseOrderProductRepository : IRepository<PurchaseOrderProductDO>
    {
        Task<int> DeletePurchaseOrderProduct(PurchaseOrderProductDO request);

        Task<int> BatchUpdatePurchaseOrderProductStatus(List<long> purchaseIds, string updateBy);
    }
}
