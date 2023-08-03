using ApolloErp.Data.DapperExtensions;
using Ae.Order.Service.Dal.Model.Order;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Order.Service.Dal.Repository.Order
{
    public interface IOrderAddressRepository : IRepository<OrderAddressDO>
    {
        Task<List<OrderAddressDO>> GetOrderAddress(List<long> orderIds);
    }
}
