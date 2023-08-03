using Ae.C.MiniApp.Api.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ApolloErp.Log;
using ApolloErp.Web.WebApi;
using Ae.C.MiniApp.Api.Client.Interface;
using Ae.C.MiniApp.Api.Client.Clients.BasicData;
using Ae.C.MiniApp.Api.Client.Request;
using Ae.C.MiniApp.Api.Client.Response;
using Ae.C.MiniApp.Api.Core.Model;
using Ae.C.MiniApp.Api.Core.Request;
using Ae.C.MiniApp.Api.Core.Response;
using Ae.C.MiniApp.Api.Client.Model;

namespace Ae.C.MiniApp.Api.Imp.Services
{
    public class BasicDataService : IBasicDataService
    {
        private readonly IMapper mapper;
        private readonly ApolloErpLogger<BasicDataService> logger;
        private readonly IBasicDataClient _basicDataClient;

        public BasicDataService(IMapper mapper, ApolloErpLogger<BasicDataService> logger, IBasicDataClient basicDataClient)
        {
            this.mapper = mapper;
            this.logger = logger;
            _basicDataClient = basicDataClient;
        }

        /// <summary>
        /// 获取省市区地址
        /// </summary>
        /// <returns></returns>
        public async Task<GetRegionChinaResponse> GetAllRegionChinaWithThreeLayer()
        {
            var clientResponse = await _basicDataClient.GetAllRegionChinaWithThreeLayer();
            var response = mapper.Map<GetRegionChinaResponse>(clientResponse);

            return response;
        }

        /// <summary>
        /// 获取所有城市
        /// </summary>
        /// <returns></returns>
        public async Task<List<RegionChinaVO>> GetAllCitys() 
        {
            var request = new RegionChinaReqByLevelClientRequest() 
            {
                Level = RegionChinaType.City
            };
            var clientResponse = await _basicDataClient.GetAllCitys(request);
            var response = mapper.Map<List<RegionChinaVO>>(clientResponse);

            return response;
        }


        /// <summary>
        /// 根据经纬度获取当前位置
        /// </summary>
        /// <returns></returns>
        public async Task<LocationResponse> GetPosition(LocationRequest request)
        {
            var clientRequest = mapper.Map<GetPositionClientRequest>(request);
            var clientResponse = await _basicDataClient.GetPosition(clientRequest);
            var response = mapper.Map<LocationResponse>(clientResponse);

            return response;
        }


        /// <summary>
        /// 获取省市区三级地址
        /// </summary>
        /// <returns></returns>
        public async Task<ThreeLevelRegionChinaResponse> GetThreeLevelRegionChina() 
        {
            var clientResponse = await _basicDataClient.GetThreeLevelRegionChina();
            var response = mapper.Map<ThreeLevelRegionChinaResponse>(clientResponse);
            return response;
        }
    }
}
