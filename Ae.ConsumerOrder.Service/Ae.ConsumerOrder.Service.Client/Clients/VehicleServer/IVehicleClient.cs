using ApolloErp.Web.WebApi;
using Ae.ConsumerOrder.Service.Client.Model.Vehicle;
using Ae.ConsumerOrder.Service.Client.Request;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.ConsumerOrder.Service.Client.Clients
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
