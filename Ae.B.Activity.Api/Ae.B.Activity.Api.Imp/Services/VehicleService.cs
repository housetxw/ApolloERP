using AutoMapper;
using Ae.B.Activity.Api.Client.Clients.Vehicle;
using Ae.B.Activity.Api.Client.Request.Vehicle;
using Ae.B.Activity.Api.Core.Interfaces;
using Ae.B.Activity.Api.Core.Request;
using Ae.B.Activity.Api.Core.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ae.B.Activity.Api.Imp.Services
{
   public class VehicleService: IVehicleService
    {
        private readonly IMapper _mapper;
        private readonly IVehicleClient _vehicleClient;
        public VehicleService(IMapper mapper, IVehicleClient vehicleClient)
        {
            this._mapper = mapper;
            this._vehicleClient = vehicleClient;
        }

       

        public async  Task<List<GetVehicleBrandResponse>> GetVehicleBrandList()
        {
            var result = await _vehicleClient.GetVehicleBrandList();
            var vo = _mapper.Map<List<GetVehicleBrandResponse>>(result);
            return vo;
        }

        public async  Task<List<GetVehicleListResponse>> GetVehicleListByBrand(GetVehicleListByBrandRequest request)
        {
            var clientRequest = _mapper.Map<GetVehicleListByBrandClientRequest>(request);
            var result = await _vehicleClient.GetVehicleListByBrand(clientRequest);
            var vo = _mapper.Map<List<GetVehicleListResponse>>(result);
            return vo;
        }

        public async Task<List<BasicInfoResponse>> GetPaiLiangByVehicleId(GetPaiLiangByVehicleIdRequest request)
        {
            var clientRequest = _mapper.Map<GetPaiLiangByVehicleIdClientRequest>(request);
            var result = await _vehicleClient.GetPaiLiangByVehicleId(clientRequest);
            var basicInfos = new List<BasicInfoResponse>();
            if (result.Any())
            {
                result.ForEach(r =>
                {
                    basicInfos.Add(new BasicInfoResponse
                    {
                        Key = r,
                        Value = r
                    });
                });
            }
            return basicInfos;
        }

        public async  Task<List<BasicInfoResponse>> GetVehicleNianByPaiLiang(GetVehicleNianByPaiLiangRequest request)
        {
            var clientRequest = _mapper.Map<GetVehicleNianByPaiLiangClientRequest>(request);
            var result = await _vehicleClient.GetVehicleNianByPaiLiang(clientRequest);
            var basicInfos = new List<BasicInfoResponse>();
            if (result.Any())
            {
                result.ForEach(r =>
                {
                    basicInfos.Add(new BasicInfoResponse
                    {
                        Key = r,
                        Value = r
                    });
                });
            }
            return basicInfos;
        }

        public async  Task<List<GetVehicleSalesNameByNianResponse>> GetVehicleSalesNameByNian(GetVehicleSalesNameByNianRequest request)
        {
            var clientRequest = _mapper.Map<GetVehicleSalesNameByNianClientRequest>(request);
            var result = await _vehicleClient.GetVehicleSalesNameByNian(clientRequest);
            var vo = _mapper.Map<List<GetVehicleSalesNameByNianResponse>>(result);
            return vo;

        }
    }
}
