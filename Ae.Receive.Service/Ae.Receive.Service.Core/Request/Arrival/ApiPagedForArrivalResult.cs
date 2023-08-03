using ApolloErp.Web.WebApi;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Receive.Service.Core.Request.Arrival
{
    public class ApiPagedForArrivalResult<T> : ApiResult
    {
        public ApiPagedForArrivalResult()
        {

        }

        public ApiPagedResultDataResponse<T> Data { get; set; }
    }
}
