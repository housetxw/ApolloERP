using ApolloErp.Data.DapperExtensions;
using Ae.Receive.Service.Dal.Model.Arrival;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Receive.Service.Dal.Repositorys
{
   public  interface IShopArrivalCancelRepository : IRepository<ShopArrivalCancelDO>
    {
        Task<ShopArrivalCancelDO> GetArrivalCancel(long arrivalId);
    }
}
