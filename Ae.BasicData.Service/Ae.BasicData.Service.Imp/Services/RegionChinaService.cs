using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Ae.BasicData.Service.Core.Interfaces;
using Ae.BasicData.Service.Core.Request;
using Ae.BasicData.Service.Core.Response;
using Ae.BasicData.Service.Dal.Repositorys.IDAL;
using System.Linq;
using Castle.DynamicProxy;
using Newtonsoft.Json;
using ApolloErp.Log;
using Ae.BasicData.Service.Client.Inteface;
using Ae.BasicData.Service.Client.Request;
using Ae.BasicData.Service.Common.Extension;
using Ae.BasicData.Service.Core.Enums;
using Ae.BasicData.Service.Core.Model;
using Ae.BasicData.Service.Imp.OperationLog;
using ApolloErp.Web.WebApi;

namespace Ae.BasicData.Service.Imp.Services
{
    public class RegionChinaService : IRegionChinaService
    {
        // ---------------------------------- 变量和构造器 --------------------------------------
        private readonly IMapper mapper;
        private readonly IBaseLogRepository logRepo;
        private readonly ApolloErpLogger<OperationLogInterceptor> loggerInter;
        private readonly IRegionChinaRepository regionChinaRepo;
        private readonly ITencentMapClient tencentMapClient;

        private readonly ProxyGenerator proxyGenerator;

        public RegionChinaService(IMapper mapper,
            IBaseLogRepository logRepo,
            ApolloErpLogger<OperationLogInterceptor> loggerInter,
            ITencentMapClient tencentMapClient,
            IRegionChinaRepository regionChinaRepo)
        {
            this.logRepo = logRepo;
            this.loggerInter = loggerInter;
            this.mapper = mapper;
            this.regionChinaRepo = regionChinaRepo;
            this.tencentMapClient = tencentMapClient;

            proxyGenerator = new ProxyGenerator();
        }
        
        // ---------------------------------- 接口实现 --------------------------------------
        public async Task<List<RegionChinaResDTO>> GetAllRegionChinaList()
        {
            var resDo = await regionChinaRepo.GetAllRegionChinaList();
            var res = mapper.Map<List<RegionChinaResDTO>>(resDo);
            return res;
        }

        public async Task<List<RegionChinaResDTO>> GetRegionChinaListByRegionId(RegionChinaReqByRegionIdDTO req)
        {
            var resDo = await regionChinaRepo.GetRegionChinaListByRegionId(req);
            var res = mapper.Map<List<RegionChinaResDTO>>(resDo);
            return res;
        }

        public async Task<List<RegionChinaResByLevelDTO>> GetRegionChinaListByLevel(RegionChinaReqByLevelDTO req)
        {
            var acctRepoInterceptor = proxyGenerator.CreateInterfaceProxyWithTarget(regionChinaRepo,
                new OperationLogInterceptor(loggerInter, logRepo, new LogOperationReqDTO
                {
                    LogId = TableNameEnum.RegionChina.ToString(),
                    LogType = LogType.R.GetEnumDescription(),
                    BizType = TableNameEnum.RegionChina.GetEnumDescription(),
                    ReqParam = JsonConvert.SerializeObject(req)
                }));

            var resDo = await regionChinaRepo.GetRegionChinaListByLevel(req);
            var res = mapper.Map<List<RegionChinaResByLevelDTO>>(resDo);
            return res;
        }

        /// <summary>
        /// 获取省市区三层地址信息
        /// </summary>
        /// <returns></returns>
        public async Task<GetRegionChinaResponse> GetAllRegionChinaWithThreeLayer()
        {
            //查询所有
            var resDo = await regionChinaRepo.GetAllRegionChinaList();
            //筛选省
            Dictionary<string, string> provinces = new Dictionary<string, string>();
            resDo.Where(o => o.Level.Equals(RegionChinaType.Province)).ToList().ForEach(p => provinces.Add(p.RegionId, p.Name));
            Dictionary<string, string> citys = new Dictionary<string, string>();
            //筛选市
            resDo.Where(o => o.Level.Equals(RegionChinaType.City)).ToList().ForEach(p => citys.Add(p.RegionId, p.Name));
            //筛选区
            Dictionary<string, string> districts = new Dictionary<string, string>();
            resDo.Where(o => o.Level.Equals(RegionChinaType.District)).ToList().ForEach(p => districts.Add(p.RegionId, p.Name));

            var response = new GetRegionChinaResponse()
            {
                Provinces = provinces,
                Citys = citys,
                Districts = districts
            };
            return response;
        }

        /// <summary>
        /// 获取省市区三级地址
        /// </summary>
        /// <returns></returns>
        public async Task<ThreeLevelRegionChinaResponse> GetThreeLevelRegionChina()
        {
            //查询所有
            var resDo = await regionChinaRepo.GetAllRegionChinaList();
            //筛选省
            var provinces = new List<RegionChinaSiteDTO>();
            resDo.Where(o => o.Level.Equals(RegionChinaType.Province)).ToList().ForEach(p => provinces.Add(new RegionChinaSiteDTO { Value = p.RegionId, Label = p.Name }));
            foreach (var item in provinces)
            {
                //筛选市
                var citys = new List<RegionChinaSiteCityDTO>();
                resDo.Where(o => o.Level.Equals(RegionChinaType.City) && o.ParentId == item.Value).ToList().ForEach(p => citys.Add(new RegionChinaSiteCityDTO { Value = p.RegionId, Label = p.Name }));
                foreach (var itemCity in citys)
                {
                    //筛选区
                    var districts = new List<RegionChinaSiteDistrictDTO>();
                    resDo.Where(o => o.Level.Equals(RegionChinaType.District) && o.ParentId == itemCity.Value).ToList().ForEach(p => districts.Add(new RegionChinaSiteDistrictDTO { Value = p.RegionId, Label = p.Name }));
                    itemCity.Children = districts;
                }
                item.Children = citys;
            }
            var response = new ThreeLevelRegionChinaResponse()
            {
                Children = provinces
            };
            return response;
        }


        /// <summary>
        /// 根据经纬度获取当前位置
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<GetPositionResponse>> GetPosition(GetPositionRequest request)
        {
            var clientRequest = new GetPositionClientRequest()
            {
                location = request.Latitude + "," + request.Longitude
            };
            var response = new GetPositionResponse();
            var result = await tencentMapClient.GetPosition(clientRequest);
            if (result.Code == ApolloErp.Web.WebApi.ResultCode.Success)
            {
                response.Province = result.Data.Province;
                response.City = result.Data.City;
                response.District = result.Data.District;
                response.StreetNumber = result.Data.Street_Number;
                response.DistrictId = result.Data.AdCode;
                var regionDo = await regionChinaRepo.GetConverseRegion(result.Data.AdCode);
                if (regionDo != null)
                {
                    response.CityId = regionDo.CityId;
                    response.ProvinceId = regionDo.ProvinceId;
                }
                return new ApiResult<GetPositionResponse>() 
                {
                    Data = response,
                    Code = ResultCode.Success,
                    Message = "定位成功"
                };
            }
            return new ApiResult<GetPositionResponse>()
            {
                Code = ResultCode.Failed,
                Message = result.Message
            };
        }


        /// <summary>
        /// 根据地址获取坐标
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<GetCoordinateResponse>> GetCoordinate(GetCoordinateRequest request) 
        {
            var clientRequest = new GetCoordinateClientRequest()
            {
                address = request.Address
            };

            var result = await tencentMapClient.GetCoordinate(clientRequest);
            if (result != null && result.Status == 0 && result.Result != null)
            {
                var resultResponse = result.Result;
                var response = new GetCoordinateResponse()
                {
                    Longitude = resultResponse.Location.Lng,
                    Latitude = resultResponse.Location.Lat,
                    DistrictId = resultResponse.ad_info.AdCode
                };
                return new ApiResult<GetCoordinateResponse>()
                {
                    Data = response,
                    Code = ResultCode.Success,
                    Message = "地址解析成功"
                };
            }
            else 
            {
                return new ApiResult<GetCoordinateResponse>()
                {
                    Code = ResultCode.Failed,
                    Message = "地址解析失败"
                };
            }
        }

        /// <summary>
        /// 获取父级RegionId
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<string> GetParentRegionId(ParentRegionIdRequest request)
        {
            var result = await regionChinaRepo.GetRegionChinaByRegionId(request.RegionId);

            return result?.ParentId ?? string.Empty;
        }

        // ---------------------------------- 私有方法 --------------------------------------

    }
}
