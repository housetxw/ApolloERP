using AutoMapper;
using ApolloErp.Web.WebApi;
using Ae.B.FMS.Api.Client.Interface;
using Ae.B.FMS.Api.Client.Request;
using Ae.B.FMS.Api.Core.Interfaces;
using Ae.B.FMS.Api.Core.Request;
using Ae.B.FMS.Api.Core.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ae.B.FMS.Api.Imp.Services
{
  public  class BasicDataService:IBasicDataService
    {
        private readonly IMapper _mapper;
        private readonly IBasicDataClient basicDataClient;


        public BasicDataService(IMapper mapper,
          IBasicDataClient basicDataClient)
        {
            _mapper = mapper;
            this.basicDataClient = basicDataClient;
        }

        public async  Task<List<GetRegionChinaListByRegionIdResponse>> GetRegionChinaListByRegionId(GetRegionChinaListByRegionIdRequest request)
        {
            GetRegionChinaListByRegionIdClientRequest clientRequest = _mapper.Map<GetRegionChinaListByRegionIdClientRequest>(request);

            var clientResponse = await basicDataClient.GetRegionChinaListByRegionId(clientRequest);

            var result = _mapper.Map<List<GetRegionChinaListByRegionIdResponse>>(clientResponse);
            return result;
        }
    }
}
