using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ApolloErp.Web.WebApi;
using Ae.Account.Api.Client.Interface.AccountAuthorityServer;
using Ae.Account.Api.Client.Request;
using Ae.Account.Api.Client.Response;
using Ae.Account.Api.Common.Constant;
using Ae.Account.Api.Core.Interfaces;
using Ae.Account.Api.Core.Model;
using Ae.Account.Api.Core.Request;
using Ae.Account.Api.Core.Response;

namespace Ae.Account.Api.Imp.Services
{
    public class AccountService : IAccountService
    {
        private readonly IMapper mapper;

        private readonly IAccountClient acctClient;

        public AccountService(IMapper mapper, IAccountClient acctClient)
        {
            this.mapper = mapper;
            this.acctClient = acctClient;
        }

        public async Task<ApiPagedResultData<AccountPageResVO>> GetAuthorityPage(AccountPageReqDTO req)
        {
            var resDo = await acctClient.GetAuthorityPage(req);
            var res = mapper.Map<ApiPagedResultData<AccountPageResVO>>(resDo);
            return res;
        }

        public async Task<List<AccountKeyInfoListDTO>> GetAccountKeyInfoListById(AccountListReqDTO req)
        {
            var res = await acctClient.GetAccountKeyInfoListById(req);
            return res;
        }

        public async Task<List<AccountKeyInfoListDTO>> GetAllAccountKeyInfoListAsync()
        {
            var res = await acctClient.GetAllAccountKeyInfoListAsync();
            return res;
        }

        public async Task<bool> UnlockAccountById(AccountUnlockReqByIdVO req)
        {
            var reqDto = mapper.Map<AccountUnlockReqByIdDTO>(req);
            var res = await acctClient.UnlockAccountById(reqDto);
            return res;
        }

        public async Task<bool> LockAccountById(AccountLockReqByIdVO req)
        {
            var reqDto = mapper.Map<AccountLockReqByIdDTO>(req);
            var res = await acctClient.LockAccountById(reqDto);
            return res;
        }

        public async Task<bool> DeleteBatchAccountById(AccountBatchDeleteReqByIdVO req)
        {
            var reqDto = mapper.Map<AccountBatchDeleteReqByIdDTO>(req);
            var res = await acctClient.DeleteBatchAccountById(reqDto);
            return res;
        }

        public async Task<AccountResetPasswordResByIdVO> ResetAccountPasswordById(AccountResetPasswordReqByIdVO req)
        {
            var reqDto = mapper.Map<AccountResetPasswordReqByIdDTO>(req);
            var resDo = await acctClient.ResetAccountPasswordById(reqDto);
            var res = mapper.Map<AccountResetPasswordResByIdVO>(resDo);
            return res;
        }

        public async Task<ApiResult<bool>> UpdatePasswordAsync(AccountUpdatePasswordEntityVO req)
        {
            var reqDto = mapper.Map<AccountUpdatePasswordEntityDTO>(req);

            var optArr = reqDto.UpdateBy.Split(CommonConstant.SeparatorVertical);
            reqDto.UpdateBy = optArr.FirstOrDefault() ?? "";
            reqDto.UpdateById = optArr.LastOrDefault() ?? "";

            return await acctClient.UpdatePasswordAsync(reqDto);
        }


    }
}
