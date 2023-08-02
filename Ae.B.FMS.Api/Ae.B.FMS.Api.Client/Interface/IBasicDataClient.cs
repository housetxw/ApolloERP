using Ae.B.FMS.Api.Client.Request;
using Ae.B.FMS.Api.Client.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ApolloErp.Web.WebApi;
namespace Ae.B.FMS.Api.Client.Interface
{
  public  interface IBasicDataClient
    {
        Task<List<GetRegionChinaListByRegionIdDTO>> GetRegionChinaListByRegionId(GetRegionChinaListByRegionIdClientRequest request);
    }
}
