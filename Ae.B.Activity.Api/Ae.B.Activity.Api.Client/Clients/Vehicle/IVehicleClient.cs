using ApolloErp.Web.WebApi;
using Ae.B.Activity.Api.Client.Request.Vehicle;
using Ae.B.Activity.Api.Client.Response.Vehicle;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.B.Activity.Api.Client.Clients.Vehicle
{
   public interface IVehicleClient
    {
        Task<List<GetVehicleBrandClientResponse>> GetVehicleBrandList();

        Task<List<GetVehicleListClientResponse>> GetVehicleListByBrand(
             GetVehicleListByBrandClientRequest request);

        Task<List<string>> GetPaiLiangByVehicleId(GetPaiLiangByVehicleIdClientRequest request);

        Task<List<string>> GetVehicleNianByPaiLiang(GetVehicleNianByPaiLiangClientRequest request);

        Task<List<GetVehicleSalesNameByNianClientResponse>> GetVehicleSalesNameByNian(GetVehicleSalesNameByNianClientRequest request);


    }
}
