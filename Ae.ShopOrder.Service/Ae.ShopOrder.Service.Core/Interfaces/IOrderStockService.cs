using ApolloErp.Web.WebApi;
using Ae.ShopOrder.Service.Core.Request.Stock;
using Ae.ShopOrder.Service.Core.Response.Stock;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.ShopOrder.Service.Core.Interfaces
{
    public interface IOrderStockService
    {
    
        Task<ApiResult<QueryUseStockOrderDetailResponse>> QueryUseStockOrderDetail(QueryUseStockOrderDetailRequest request);
        Task<ApiResult<QueryOrderDetailUseStockResponse>> QueryOrderRealProductDetail(QueryUseStockOrderDetailRequest request);
        Task<ApiResult> OrderUseStockNotify(OrderUseStockNotifyRequest request);
    }
}
