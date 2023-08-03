using ApolloErp.Web.WebApi;
using Ae.OrderComment.Service.Client.Model;
using Ae.OrderComment.Service.Client.Request;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.OrderComment.Service.Client.Interface
{
    public interface IVehicleClient
    {
        Task<ApiResult<UserVehicleDTO>> GetUserVehicleByCarIdAsync(UserVehicleByCarIdClientRequest request);
    }
}
