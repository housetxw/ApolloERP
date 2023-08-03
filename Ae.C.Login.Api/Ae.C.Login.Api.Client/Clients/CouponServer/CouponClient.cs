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

namespace Ae.C.Login.Api.Client.Clients.CouponServer
{
    public class CouponClient : ICouponClient
    {
        private readonly HttpClient client;
        private IConfiguration configuration { get; }

        private readonly ApolloErpLogger<CouponClient> logger;
        private readonly string className;

        public CouponClient(ApolloErpLogger<CouponClient> logger, IHttpClientFactory clientFactory, IConfiguration configuration)
        {
            this.configuration = configuration;
            client = clientFactory.CreateClient("CouponServer");

            this.logger = logger;
            className = this.GetType().Name;
        }


        public async Task<bool> AddUserCouponForNewRegisterUser(AddUserCouponReqForNewUserDTO req)
        {
            ApiResult<bool> resApi = null;

            try
            {
                resApi = await client.PostAsJsonAsync<AddUserCouponReqForNewUserDTO, ApiResult<bool>>(configuration["CouponServer:AddUserCouponForNewRegisterUser"], req);

                if (resApi?.Code != ResultCode.Success)
                {
                    var msg = $"{CommonConstant.ResultCodeFailed}\n" +
                              $"{CommonConstant.ParameterReqDetail}:\n{JsonConvert.SerializeObject(resApi)}";
                    logger.Warn(msg);
                    return false;
                }
            }
            catch (Exception e)
            {
                var msg = $"{CommonConstant.ResultCodeException}\n" +
                          $"{CommonConstant.ParameterReqDetail}:\n{JsonConvert.SerializeObject(req)}";
                logger.Error(msg, e);
                throw;
            }
            finally
            {
                logger.Info($"API: {className}.AddUserCouponForNewRegisterUser 请求参数：{JsonConvert.SerializeObject(req)}");
                logger.Info($"API: {className}.AddUserCouponForNewRegisterUser 返回值：{JsonConvert.SerializeObject(resApi)}");
            }

            return resApi.Data;
        }


    }
}
