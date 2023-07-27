using ApolloErp.Data.DapperExtensions;
using Ae.Shop.Service.Core.Request;
using Ae.Shop.Service.Dal.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Shop.Service.Dal.Repositorys.Shop
{
    public interface IShopBankCardRepository : IRepository<ShopBankCardDO>
    {
        Task<ShopBankCardInfoDO> GetShopBankCardByShopIdAsync(long shopId);
        Task<bool> ModifyShopBankAccountAsync(ModifyShopBankAccountRequest request);
    }
}
