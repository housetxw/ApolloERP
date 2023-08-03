using ApolloErp.Web.WebApi;
using Ae.Shop.Api.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Shop.Api.Core.Interfaces
{
    public interface IOilTransferService
    {
        Task<ApiResult<List<ProductStockDTO>>> GetProductStocks(ProductStockDTO request);
    }
}
