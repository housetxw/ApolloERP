using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using ApolloErp.Log;
using ApolloErp.Web.WebApi;
using Ae.C.Login.Api.Client.Request;
using Ae.C.Login.Api.Common.Constant;

namespace Ae.C.Login.Api.Client.Clients.UserServer
{
    public class UserClient : IUserClient
    {
        private readonly HttpClient client;
        private IConfiguration configuration { get; }

        private readonly ApolloErpLogger<UserClient> logger;
        private readonly string className;

        public UserClient(ApolloErpLogger<UserClient> logger, IHttpClientFactory clientFactory, IConfiguration configuration)
        {
            this.configuration = configuration;
            client = clientFactory.CreateClient("UserServer");

            this.logger = logger;
            className = this.GetType().Name;
        }

        public async Task<bool> OperateUserPoint(OperateUserPointRequest req)
        {
            ApiResult<bool> resApi = null;

            try
            {
                resApi = await client.PostAsJsonAsync<OperateUserPointRequest, ApiResult<bool>>(configuration["UserServer:OperateUserPoint"], req);

                if (resApi?.Code != ResultCode.Success)
                {
                    var msg = $"{CommonConstant.ResultCodeFailed}\n" +
                              $"{CommonConstant.ParameterReqDetail}\n{JsonConvert.SerializeObject(resApi)}";
                    logger.Warn(msg);
                    return false;
                }
            }
            catch (Exception e)
            {
                var msg = $"{CommonConstant.ResultCodeException}\n" +
                          $"{CommonConstant.ParameterReqDetail}\n{JsonConvert.SerializeObject(req)}";
                logger.Error(msg, e);
                throw;
            }
            finally
            {
                logger.Info($"API: {className}.OperateUserPoint 请求参数：{JsonConvert.SerializeObject(req)}");
                logger.Info($"API: {className}.OperateUserPoint 返回值：{JsonConvert.SerializeObject(resApi)}");
            }

            return resApi.Data;
        }


    }
}
