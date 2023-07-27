using ApolloErp.Data.DapperExtensions;
using Ae.Shop.Service.Core.Request;
using Ae.Shop.Service.Dal.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Shop.Service.Dal.Repositorys.JoinIn
{
    public interface IShopJoinRepository : IRepository<ShopJoinDO>
    {
        Task<PagedEntity<ShopJoinDO>> GetShopJointListAsync(GetShopJoinListRequest request);
        Task<bool> CheckShopJoinAsync(CheckShopJoinRequest request);
    }
}
