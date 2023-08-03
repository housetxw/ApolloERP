using ApolloErp.Data.DapperExtensions;
using Ae.ConsumerOrder.Service.Dal.Model.Order;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.ConsumerOrder.Service.Dal.Repository.Order
{
    public interface IOrderDiscountRepository : IRepository<OrderDiscountDO>
    {
    }
}
