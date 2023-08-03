using ApolloErp.Web.WebApi;
using Ae.OrderComment.Service.Core.Model;
using Ae.OrderComment.Service.Core.Request;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.OrderComment.Service.Core.Interfaces
{
  public  interface IShopManageService
    {
        Task<ApiPagedResult<ShopSimpleInfoDTO>> GetShopListAsync(GetShopListRequest request);
    }
}
