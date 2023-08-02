using Ae.B.Product.Api.Core.Interfaces;
using Ae.B.Product.Api.Core.Response.Vehicle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ae.B.Product.Api.Client.Interfaces;
using Ae.B.Product.Api.Client.Request.Vehicle;
using Ae.B.Product.Api.Core.Request.Vehicle;
using Ae.B.Product.Api.Core.Model.Vehicle;
using AutoMapper;
using ApolloErp.Login.Auth;

namespace Ae.B.Product.Api.Imp.Services
{
    public class VehicleService : IVehicleService
    {
        private IVehicleClient _vehicleClient;
        private readonly IMapper _mapper;
        private readonly IIdentityService _identityService;

        public VehicleService(IVehicleClient vehicleClient, IMapper mapper, IIdentityService identityService)
        {
            _vehicleClient = vehicleClient;
            _mapper = mapper;
            _identityService = identityService;
        }

        /// <summary>
        /// 获取车型品牌
        /// </summary>
        /// <returns></returns>
        public async Task<List<VehicleBrandResponse>> GetVehicleBrandList()
        {
            var result = await _vehicleClient.GetVehicleBrandListAsync();

            return result?.Select(_ => new VehicleBrandResponse
            {
                Brand = _.Brand
            })?.ToList() ?? new List<VehicleBrandResponse>();
        }

        /// <summary>
        /// 根据品牌查车系
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<List<VehicleListResponse>> GetVehicleListByBrand(VehicleListByBrandRequest request)
        {
            var result = await _vehicleClient.GetVehicleListByBrandAsync(new VehicleListByBrandClientRequest()
            {
                Brand = request.Brand
            });

            return result?.Select(_ => new VehicleListResponse
            {
                VehicleId = _.VehicleId,
                Vehicle = _.Vehicle
            })?.ToList() ?? new List<VehicleListResponse>();
        }

        /// <summary>
        /// 根据车系查排量
        /// </summary>
        /// <param name="request"></param>
        public async Task<List<string>> GetPaiLiangByVehicleId(GetPaiLiangByVehicleIdRequest request)
        {
            var result = await _vehicleClient.GetPaiLiangByVehicleIdAsync(new PaiLiangByVehicleIdClientRequest()
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
        public async Task<List<string>> GetVehicleNianByPaiLiang(VehicleNianByPaiLiangRequest request)
        {
            var result = await _vehicleClient.GetVehicleNianByPaiLiangAsync(new VehicleNianByPaiLiangClientRequest()
            {
                PaiLiang = request.PaiLiang,
                VehicleId = request.VehicleId
            });

            return result ?? new List<string>();
        }

        /// <summary>
        /// 根据生产年份查询款型
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<List<VehicleSalesNameByNianResponse>> GetVehicleSalesNameByNian(
            VehicleSalesNameByNianRequest request)
        {
            var result = await _vehicleClient.GetVehicleSalesNameByNianAsync(new VehicleSalesNameByNianClientRequest()
            {
                VehicleId = request.VehicleId,
                PaiLiang = request.PaiLiang,
                Nian = request.Nian
            });

            return result?.Select(_ => new VehicleSalesNameByNianResponse
            {
                SalesName = _.SalesName,
                Tid = _.Tid
            })?.ToList() ?? new List<VehicleSalesNameByNianResponse>();
        }

        /// <summary>
        /// 车型详细信息查询
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<List<VehicleDetailVo>> GetVehicleInfoList(VehicleInfoListRequest request)
        {
            List<VehicleDetailVo> result = new List<VehicleDetailVo>();
            var response = await _vehicleClient.GetVehicleInfoList(new Client.Request.VehicleInfoListRequest
            {
                VehicleId = request.VehicleId,
                PaiLiang = request.PaiLiang,
                Nian = request.Nian,
                Tid = request.Tid
            });
            if (response != null && response.Any())
            {
                result = _mapper.Map<List<VehicleDetailVo>>(response);
            }
            return result;
        }

        /// <summary>
        /// 删除车型
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> DeleteVehicleByTid(DeleteVehicleByTidRequest request)
        {
            var result = await _vehicleClient.DeleteVehicleByTid(new DeleteVehicleByTidClientRequest()
            {
                Tid = request.Tid,
                SubmitBy = _identityService.GetUserName()
            });

            return result;
        }

        /// <summary>
        /// 根据车型查询Tids
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<List<string>> GetTidsByVehicle(TidsByVehicleRequest request)
        {
            var result = await _vehicleClient.GetTidsByVehicle(new TidsByVehicleClientRequest
            {
                Brand = request.Brand,
                VehicleId = request.VehicleId,
                PaiLiang = request.PaiLiang,
                Nian = request.Nian,
                Tid = request.Tid
            });

            return result ?? new List<string>();
        }
    }
}
