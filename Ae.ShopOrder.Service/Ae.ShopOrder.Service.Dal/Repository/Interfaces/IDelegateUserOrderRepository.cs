using ApolloErp.Data.DapperExtensions;
using Ae.ShopOrder.Service.Dal.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.ShopOrder.Service.Dal.Repository.Interfaces
{
    public interface IDelegateUserOrderRepository : IRepository<DelegateUserOrderDO>
    {
        Task<bool> UpdateOrderRefInfo(List<string> orderNos, string refOrderNo, string updateBy);

        Task<bool> ClearOrderRefInfo(string refOrderNo, string updateBy);
    }
}
