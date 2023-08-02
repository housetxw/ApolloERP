using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using ApolloErp.Web.WebApi;
using Ae.B.FMS.Api.Client.Model;
using Ae.B.FMS.Api.Client.Request;
using Ae.B.FMS.Api.Core.Request;
using Ae.B.FMS.Api.Core.Response;

namespace Ae.B.FMS.Api.Imp.Mappers
{
    public class AccountCheckProfile : Profile
    {
        public AccountCheckProfile()
        {
            CreateMap<GetAccountCheckRequest, GetAccountCheckClientRequest>();

            CreateMap<AccountCheckLogRequest, AccountCheckLogClientRequest>();

            CreateMap<AccountCheckExceptionRequest, AccountCheckExceptionClientRequest>();

            CreateMap<ApiPagedResultData<AccountCheckDTO>, ApiPagedResultData<AccountCheckResponse>>();
            CreateMap<AccountCheckDTO, AccountCheckResponse>();


            CreateMap<AccountCheckRequest, AccountCheckDTO>();
            CreateMap<RgAccountCheckConfirmRequest, RgAccountCheckConfirmClientRequest>();
            

            CreateMap<AccountCheckLogDTO, AccountCheckLogResponse>();
            CreateMap<AccountCheckExceptionCollectClientDTO, AccountCheckExceptionCollectResponse>();
            CreateMap<ApiPagedResultData<AccountCheckExceptionCollectClientDTO>, ApiPagedResultData<AccountCheckExceptionCollectResponse>>();

            CreateMap<RgAccountCheckWithdrawRequeset, RgAccountCheckWithdrawClientRequeset>();

            CreateMap<GetAccountCheckCollectRequest, GetAccountCheckCollectClientRequest>();

            CreateMap<AccountCheckCollectDTO, AccountCheckCollectResponse>();

            CreateMap<ApiPagedResultData<AccountCheckCollectDTO>, ApiPagedResultData<AccountCheckCollectResponse>>();
            CreateMap<AccountCheckExceptionHandleRequest, AccountCheckExceptionHandleClientRequest>();
            

        }
    }
}
