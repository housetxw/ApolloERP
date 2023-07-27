using ApolloErp.Data.DapperExtensions;
using Ae.Shop.Service.Dal.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Shop.Service.Dal.Repositorys.Shop
{
    public interface IBankInformationRepository : IRepository<BankInformationDO>
    {
        /// <summary>
        /// 查询银行列表
        /// </summary>
        /// <returns></returns>
        Task<List<BankInformationDO>> GetBankListAsync();
    }
}
