using ApolloErp.Web.WebApi;
using Ae.Shop.Api.Core.Model;
using Ae.Shop.Api.Core.Model.ShopPurchase;
using Ae.Shop.Api.Core.Request;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Shop.Api.Client.Clients
{
    public interface IPurchaseClient
    {
        Task<ApiResult<List<SupplierProductStockDTO>>> GetSupplierPurchaseProducts(GetSupplierPurchaseRequest request);
        Task<List<VenderDTO>> GetVenders(GetSupplierPurchaseRequest request);

        Task<ApiPagedResult<VenderProductForAppVo>> SearchVenderProductListForApp(SearchVenderProductListForAppRequest request);
    }
}
