using AutoMapper;
using Ae.Shop.Service.Client.Inteface;
using Ae.Shop.Service.Client.Response;
using Ae.Shop.Service.Core.Interfaces;
using Ae.Shop.Service.Core.Model;
using Ae.Shop.Service.Core.Request;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Shop.Service.Imp.Services
{
    public class RegionChinaService : IRegionChinaService
    {
        private readonly IBasicDataClient _basicDataClient;
        private readonly IMapper _mapper;

        public RegionChinaService(
            IBasicDataClient basicDataClient,
            IMapper mapper
            ) 
        {
            _basicDataClient = basicDataClient;
            _mapper = mapper;
        }
        /// <summary>
        /// 根据RegionId获取子一级区域
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<List<RegionChinaVO>> GetRegionChinaListByRegionId(GetRegionChinaListByRegionIdRequest request) 
        {
            var response = await _basicDataClient.RegionChinaReqByRegionId(new RegionChinaReqByRegionIdClientRequest() { RegionId = request.RegionId});
            return _mapper.Map<List<RegionChinaVO>>(response);
        }
    }
}
