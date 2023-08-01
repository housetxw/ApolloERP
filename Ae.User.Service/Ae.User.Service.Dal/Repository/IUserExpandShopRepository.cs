using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ApolloErp.Data.DapperExtensions;
using Ae.User.Service.Dal.Model;

namespace Ae.User.Service.Dal.Repository
{
    public interface IUserExpandShopRepository : IRepository<UserExpandShopDo>
    {
        Task<UserExpandShopDo> GetDefaultUserExpandShop(string userId);

        Task<UserExpandShopDo> GetDefaultUserExpandShop(long shopId);
    }
}
