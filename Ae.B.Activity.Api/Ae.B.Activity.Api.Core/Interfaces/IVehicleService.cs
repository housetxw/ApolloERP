using Ae.B.Activity.Api.Core.Request;
using Ae.B.Activity.Api.Core.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.B.Activity.Api.Core.Interfaces
{
   public interface IVehicleService
    {
        Task<List<GetVehicleBrandResponse>> GetVehicleBrandList();

        Task<List<GetVehicleListResponse>> GetVehicleListByBrand(GetVehicleListByBrandRequest request);

        Task<List<BasicInfoResponse>> GetPaiLiangByVehicleId(GetPaiLiangByVehicleIdRequest request);

        Task<List<BasicInfoResponse>> GetVehicleNianByPaiLiang(GetVehicleNianByPaiLiangRequest request);

        Task<List<GetVehicleSalesNameByNianResponse>> GetVehicleSalesNameByNian(GetVehicleSalesNameByNianRequest request);
    }
}
