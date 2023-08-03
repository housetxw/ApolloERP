using ApolloErp.Web.WebApi;
using Ae.ShopOrder.Service.Client.Model.Vehicle;
using Ae.ShopOrder.Service.Client.Request.Vehicle;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.ShopOrder.Service.Client.Clients.Vehicle
{
    public interface IVehicleClient
    {
        /// <summary>
        /// 获取用户车型
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult<UserVehicleDTO>> GetUserVehicleByCarIdAsync(UserVehicleByCarIdRequest request);
    }
}
