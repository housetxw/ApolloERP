using ApolloErp.Data.DapperExtensions;
using Ae.Shop.Api.Core.Request.Arrival;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Ae.Shop.Api.Core.Response.Arrival;
using Ae.Shop.Api.Dal.Model.Arrival;
using Ae.Shop.Api.Core.Request;
using Ae.Shop.Api.Core.Response;

namespace Ae.Shop.Api.Dal.Repositorys.Arrival
{
    public interface IArrivalRepository : IRepository<ShopArrivalDO>
    {
        /// <summary>
        /// 得到到店记录列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<PagedEntity<GetArrivalListResponse>> GetArrivalList(GetArrivalListRequest request);

        /// <summary>
        /// 得到到店记录详情
        /// </summary>
        /// <param name="arrivalId"></param>
        /// <returns></returns>
        Task<ShopArrivalDO> GetShopArrival(long arrivalId);

        /// <summary>
        /// 到店记录下面订单
        /// </summary>
        /// <param name="arrivalIds"></param>
        /// <returns></returns>
        Task<List<ShopArrivalOrderDO>> GetShopArrivalOrders(List<long> arrivalIds);

        Task<List<ArrivalTrendChartEntityDTO>> GetArrivalTrendStatisticsByStatus(ArrivalTrendStatisticsReqDO req);

      
        
    }
}
