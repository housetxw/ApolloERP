using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using ApolloErp.Log;
using ApolloErp.Web.WebApi;
using Ae.Account.Api.Client.Interface.AccountAuthorityServer;
using Ae.Account.Api.Client.Model;
using Ae.Account.Api.Client.Request;
using Ae.Account.Api.Client.Response;
using Ae.Account.Api.Common.Constant;
using Ae.Account.Api.Common.Extension;

namespace Ae.Account.Api.Client.Clients.AccountAuthorityServer
{
    public class AccountClient : IAccountClient
    {
        private readonly ApolloErpLogger<AccountClient> logger;
        private readonly string className;

        private readonly HttpClient client;

        private IConfiguration configuration { get; }

        public AccountClient(ApolloErpLogger<AccountClient> logger, IHttpClientFactory clientFactory, IConfiguration configuration)
        {
            this.logger = logger;
            className = this.GetType().Name;

            this.configuration = configuration;
            client = clientFactory.CreateClient("AccountAuthorityServer");
        }

        public async Task<ApiPagedResultData<AccountPageDTO>> GetAuthorityPage(AccountPageReqDTO req)
        {
            var res = await client.GetAsJsonAsync<AccountPageReqDTO, ApiPagedResultData<AccountPageDTO>>(configuration["AccountAuthorityServer:GetAuthorityPage"], req);
            return res;
        }

        public async Task<List<AccountKeyInfoListDTO>> GetAccountKeyInfoListById(AccountListReqDTO req)
        {
            var res = new List<AccountKeyInfoListDTO>();
            try
            {
                res = await client.PostAsJsonAsync<AccountListReqDTO, List<AccountKeyInfoListDTO>>(configuration["AccountAuthorityServer:GetAccountKeyInfoListById"], req);
            }
            catch (Exception e)
            {
                logger.Error(JsonConvert.SerializeObject(req).GenAPIExceptionInfo(), e);
            }
            finally
            {
                logger.Info($"SVC: {className}.GetAccountKeyInfoListById 请求参数： {JsonConvert.SerializeObject(req)}");
                logger.Info($"SVC: {className}.GetAccountKeyInfoListById 返回值： {JsonConvert.SerializeObject(res)}");
            }
            return res;
        }

        public async Task<List<AccountKeyInfoListDTO>> GetAllAccountKeyInfoListAsync()
        {
            var res = await client.GetAsJsonAsync<List<AccountKeyInfoListDTO>>(configuration["AccountAuthorityServer:GetAllAccountKeyInfoListAsync"], "");
            return res;
        }

        public async Task<bool> UnlockAccountById(AccountUnlockReqByIdDTO req)
        {
            var res = await client.PostAsJsonAsync<AccountUnlockReqByIdDTO, bool>(configuration["AccountAuthorityServer:UnlockAccountById"], req);
            return res;
        }

        public async Task<bool> LockAccountById(AccountLockReqByIdDTO req)
        {
            var res = await client.PostAsJsonAsync<AccountLockReqByIdDTO, bool>(configuration["AccountAuthorityServer:LockAccountById"], req);
            return res;
        }

        public async Task<bool> DeleteBatchAccountById(AccountBatchDeleteReqByIdDTO req)
        {
            var res = await client.PostAsJsonAsync<AccountBatchDeleteReqByIdDTO, bool>(configuration["AccountAuthorityServer:DeleteBatchAccountById"], req);
            return res;
        }

        public async Task<AccountResetPasswordResByIdDTO> ResetAccountPasswordById(AccountResetPasswordReqByIdDTO req)
        {
            var res = await client.PostAsJsonAsync<AccountResetPasswordReqByIdDTO, AccountResetPasswordResByIdDTO>(configuration["AccountAuthorityServer:ResetAccountPasswordById"], req);
            return res;
        }

        public async Task<ApiResult<bool>> UpdatePasswordAsync(AccountUpdatePasswordEntityDTO req)
        {
            ApiResult<bool> res = new ApiResult<bool>();

            try
            {
                res = await client.PostAsJsonAsync<AccountUpdatePasswordEntityDTO, ApiResult<bool>>(
                    configuration["AccountAuthorityServer:UpdatePasswordAsync"], req);

                if (res.Code != ResultCode.Success)
                {
                    logger.Warn(JsonConvert.SerializeObject(req).GenArgumentErrorInfo(CommonConstant.ResultCodeFailed));
                    return res;
                }
            }
            catch (Exception e)
            {
                logger.Error(JsonConvert.SerializeObject(req).GenExceptionInfo(CommonConstant.ResultCodeException), e);
                return res;
            }
            finally
            {
                //logger.Info($"API: {className}.GetEmpAuthorityListForAPPByEmpIdAsync 请求参数：{JsonConvert.SerializeObject(req)}");
                logger.Info($"API: {className}.GetEmpAuthorityListForAPPByEmpIdAsync 返回值：{JsonConvert.SerializeObject(res)}");
            }

            return res;
        }

    }
}
