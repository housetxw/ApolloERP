using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ApolloErp.Web.WebApi;
using Ae.B.Order.Api.Core.Request;

namespace Ae.B.Order.Api.Core.Interfaces
{
    public interface IReceiveService
    {


        Task<ApiResult> ModifyReserveTime(ModifyReserveTimeRequest request);
    }
}
