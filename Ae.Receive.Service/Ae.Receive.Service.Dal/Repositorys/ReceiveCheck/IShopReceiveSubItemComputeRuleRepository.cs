using ApolloErp.Data.DapperExtensions;
using Ae.Receive.Service.Dal.Model.ReceiveCheck;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Receive.Service.Dal.Repositorys.ReceiveCheck
{
    public interface IShopReceiveSubItemComputeRuleRepository:IRepository<ShopReceiveSubItemComputeRuleDo>
    {
        /// <summary>
        /// 查询计算规则
        /// </summary>
        /// <param name="subItemId"></param>
        /// <returns></returns>
        Task<List<ShopReceiveSubItemComputeRuleDo>> GetSubItemComputeRule(long subItemId);
    }
}
