using AutoMapper;
using Ae.Shop.Api.Client.Clients.VehicleServer;
using Ae.Shop.Api.Client.Request.Vehicle;
using Ae.Shop.Api.Core.Interfaces;
using Ae.Shop.Api.Core.Model.Reserve;
using Ae.Shop.Api.Core.Request.Reserve;
using Ae.Shop.Api.Core.Request.Vehicle;
using Ae.Shop.Api.Core.Response.Vehicle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Shop.Api.Imp.Services
{
    public class VehicleService : IVehicleService
    {
        private readonly IVehicleClient vehicleClient;
        private readonly IMapper mapper;

        public VehicleService(IVehicleClient vehicleClient, IMapper mapper)
        {
            this.vehicleClient = vehicleClient;
            this.mapper = mapper;
        }

        /// <summary>
        /// 用户车型列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<List<UserVehicleSimpleVo>> GetAllUserVehicles(UserVehiclesRequest request)
        {
            List<UserVehicleSimpleVo> result = new List<UserVehicleSimpleVo>();
            var vehicleList = await vehicleClient.GetAllUserVehiclesAsync(new AllUserVehiclesClientRequest()
            {
                UserId = request.UserId
            });
            if (vehicleList != null)
            {
                result = mapper.Map<List<UserVehicleSimpleVo>>(vehicleList);
            }

            result.ForEach(_ => { _.CarType = $"{_.Vehicle}|{_.PaiLiang}|{_.Nian}|{_.SalesName}"; });
            return result;
        }

        /// <summary>
        /// 获取车型品牌
        /// </summary>
        /// <returns></returns>
        public async Task<List<VehicleBrandResponse>> GetVehicleBrandListAsync()
        {
            List<VehicleBrandResponse> response = new List<VehicleBrandResponse>();
            var result = await vehicleClient.GetVehicleBrandListAsync();
            if (result != null && result.Any())
            {
                response = result.Select(_ => new VehicleBrandResponse
                {
                    Brand = _.Brand
                }).ToList();
            }

            return response;
        }

        /// <summary>
        /// 根据品牌查车系
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<List<VehicleListResponse>> GetVehicleListByBrandAsync(VehicleListByBrandRequest request)
        {
            List<VehicleListResponse> response = new List<VehicleListResponse>();
            var result = await vehicleClient.GetVehicleListByBrandAsync(new VehicleListByBrandClientRequest
            {
                Brand = request.Brand
            });
            if (result != null && result.Any())
            {
                response = result.Select(_ => new VehicleListResponse
                {
                    VehicleId = _.VehicleId,
                    Vehicle = _.Vehicle,
                    Factory = _.Factory,
                    VehicleSeries = _.VehicleSeries,
                    TireSize = _.TireSize
                }).ToList();
            }

            return response;
        }

        /// <summary>
        /// 根据车系查排量
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<List<string>> GetPaiLiangByVehicleIdAsync(GetPaiLiangByVehicleIdRequest request)
        {
            var result = await vehicleClient.GetPaiLiangByVehicleIdAsync(new PaiLiangByVehicleIdClientRequest
            {
                VehicleId = request.VehicleId
            });
            return result ?? new List<string>();
        }

        /// <summary>
        /// 根据排量获取生产年份
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<List<string>> GetVehicleNianByPaiLiangAsync(VehicleNianByPaiLiangRequest request)
        {
            var result = await vehicleClient.GetVehicleNianByPaiLiangAsync(new VehicleNianByPaiLiangClientRequest
            {
                VehicleId = request.VehicleId,
                PaiLiang = request.PaiLiang
            });

            return result ?? new List<string>();
        }

        /// <summary>
        /// 根据生产年份查询款型
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<List<VehicleSalesNameByNianResponse>> GetVehicleSalesNameByNianAsync(
            VehicleSalesNameByNianRequest request)
        {
            var result = await vehicleClient.GetVehicleSalesNameByNianAsync(new VehicleSalesNameByNianClientRequest
            {
                VehicleId = request.VehicleId,
                PaiLiang = request.PaiLiang,
                Nian = request.Nian
            });

            return result?.Select(_ => new VehicleSalesNameByNianResponse
            {
                Tid = _.Tid,
                SalesName = _.SalesName
            })?.ToList() ?? new List<VehicleSalesNameByNianResponse>();
        }
    }
}
