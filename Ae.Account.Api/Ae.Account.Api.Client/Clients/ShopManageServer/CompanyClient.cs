using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using ApolloErp.Log;
using ApolloErp.Web.WebApi;
using Ae.Account.Api.Client.Interface;
using Ae.Account.Api.Common.Constant;
using Ae.Account.Api.Client.Response;
using Ae.Account.Api.Common.Extension;

namespace Ae.Account.Api.Client.Clients
{
    public class CompanyClient : ICompanyClient
    {
        #region 变量和构造器

        private readonly HttpClient client;

        private IConfiguration configuration { get; }

        private readonly ApolloErpLogger<CompanyClient> logger;

        public CompanyClient(ApolloErpLogger<CompanyClient> logger, IHttpClientFactory clientFactory, IConfiguration configuration)
        {
            this.logger = logger;
            this.configuration = configuration;
            client = clientFactory.CreateClient("ShopManageServer");
        }

        #endregion 变量和构造器

        #region 接口实现

        public async Task<List<UnitDTO>> GetAllOrganizationListAsync()
        {
            var res = await client.GetAsJsonAsync<ApiResult<GetAllUnitResponse>>(configuration["ShopManageServer:GetAllUnitAsync"], "");
            if (res.Code != ResultCode.Success)
            {
                logger.Error(JsonConvert.SerializeObject(res).GenExceptionInfo(CommonConstant.ResultCodeFailed));
                return new List<UnitDTO>();
            }

            logger.Info($"GetAsJsonAsync<ApiResult<GetAllUnitResponse>> 返回值：{JsonConvert.SerializeObject(res)}");
            var iEnum = res.Data.List.GroupBy(g => new { g.Id, g.Type }).Select(s => s.FirstOrDefault());
            return iEnum.ToList();
        }


        #endregion 接口实现
    }
}
