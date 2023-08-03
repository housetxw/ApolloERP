using ApolloErp.Data.DapperExtensions;
using Ae.ConsumerOrder.Service.Core.Request.SharingPromotion;
using Ae.ConsumerOrder.Service.Dal.Model.SharingPromotin;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.ConsumerOrder.Service.Dal.Repository.SharingPromtion
{
    public interface IOrderUserShareDetailRepository : IRepository<OrderUserShareDetailDO>
    {
        Task<PagedEntity<OrderUserShareDetailDO>> GetOrderUserShareDetail(GetSharingOrdersRequest request);

        Task<OrderUserShareDetailDO> GetOrderUserShareDetailDoByUserId(string sourceUserId, string destinationUserId);

        Task<int> UpdateOrderUserShareDetailStatus(List<string> orderNos, long couponId);
       
    }
}
