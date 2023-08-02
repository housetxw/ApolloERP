using ApolloErp.Web.WebApi;
using Ae.B.FMS.Api.Core.Request;
using Ae.B.FMS.Api.Core.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
namespace Ae.B.FMS.Api.Core.Interfaces
{
  public  interface IBasicDataService
    {
        Task<List<GetRegionChinaListByRegionIdResponse>> GetRegionChinaListByRegionId(GetRegionChinaListByRegionIdRequest request);
    }
}
