using ApolloErp.Web.WebApi;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Receive.Service.Core.Request.Arrival
{
    public class ApiPagedResultDataResponse<T> : ApiPagedResultData<T>
    {
        /// <summary>
        /// 到店记录数量
        /// </summary>
        public GetArrivalListCountResponse ArrivalListCount { get; set; }
    }
}
