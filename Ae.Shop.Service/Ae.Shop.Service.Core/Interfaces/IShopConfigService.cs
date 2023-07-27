using ApolloErp.Web.WebApi;
using Ae.Shop.Service.Core.Request;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Shop.Service.Core.Interfaces
{
    public interface IShopConfigService
    {
        Task<bool> ModifyShopConfigInfoAsync(ModifyShopConfigInfoRequest request);
        Task<bool> ModifyShopConfigExpenseAsync(ModifyShopConfigExpenseRequest request);
         Task<ApiResult<string>> ModifyShopSuspendTime(ModifyShopSuspendTimeRequest request);
    }
}
