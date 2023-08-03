using ApolloErp.Web.WebApi;
using Ae.Order.Service.Core.Model.Vehicle;
using Ae.Order.Service.Core.Request.Vehicle;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Order.Service.Client.Interface
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
