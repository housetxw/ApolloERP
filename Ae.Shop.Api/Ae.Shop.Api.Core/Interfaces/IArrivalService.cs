using ApolloErp.Web.WebApi;
using Ae.Shop.Api.Core.Request.Arrival;
using Ae.Shop.Api.Core.Response.Arrival;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Ae.Shop.Api.Core.Request;
using Ae.Shop.Api.Core.Response;

namespace Ae.Shop.Api.Core.Interfaces
{
    public interface IArrivalService
    {
        /// <summary>
        /// 到店记录列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiPagedResult<GetArrivalListResponse>> GetArrivalList(GetArrivalListRequest request);

        /// <summary>
        /// 到店记录详情
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ApiResult<GetArrivalDetailResponse>> GetArrivalDetail(GetArrivalDetailRequest request);

        ArrivalTrendStatisticsResDTO GetArrivalTrendStatistics(ArrivalTrendStatisticsReqDTO req);
        

    }
}
