using ApolloErp.Data.DapperExtensions;
using Ae.Receive.Service.Dal.Model.ReceiveCheck;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Receive.Service.Dal.Repositorys.ReceiveCheck
{
    public interface IShopReceiveResultWordComputeRuleRepository : IRepository<ShopReceiveResultWordComputeRuleDo>
    {
        /// <summary>
        /// 规则关联结果词
        /// </summary>
        /// <returns></returns>
        Task<List<ShopReceiveResultWordComputeRuleDo>> GetResultWordComputeRule();

        /// <summary>
        /// rule关联word
        /// </summary>
        /// <returns></returns>
        Task<List<ShopCheckRuleWordDo>> GetResultWordAndRule();
    }
}
