using ApolloErp.Data.DapperExtensions;
using Ae.ConsumerOrder.Service.Dal.Model.SharingPromotin;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.ConsumerOrder.Service.Dal.Repository.SharingPromtion
{
    public interface IOrderUserShareRepository : IRepository<OrderUserShareDO>
    {
        Task<OrderUserShareDO> GetOrderUserShare(string userId);

        Task<long> UpdateOrderUserShareNum(string userId,int shareSumNum,int exchangeNum, int exchangeCouponNum=0);
    }
}
