using Ae.Shop.Api.Core.Model;
using Ae.Shop.Api.Core.Request;
using ApolloErp.Web.WebApi;
using System.Threading.Tasks;

namespace Ae.Shop.Api.Core.Interfaces
{
    public interface ITestService
    {
        Task<ApiResult<ShopVO>> GetShopById(GetShopRequest request);
    }
}
