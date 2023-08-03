using ApolloErp.Data.DapperExtensions;
using Ae.ShopOrder.Service.Core.Model.Order;
using Ae.ShopOrder.Service.Dal.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.ShopOrder.Service.Dal.Repository.Interfaces
{
    public interface IOrderAddressRepository : IRepository<OrderAddressDO>
    {
        Task<OrderAddressDO> GetOrderAddress(long orderId);

         Task<bool> UpdateOrderAddress(UpdateOrderAddressRequest request);
    }
}
