using ApolloErp.Data.DapperExtensions;
using Ae.ShopOrder.Service.Core.Request.Order;
using Ae.ShopOrder.Service.Dal.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.ShopOrder.Service.Dal.Repository.Interfaces
{
    public interface IVerificationRuleRepository : IRepository<VerificationRuleDO>
    {
        /// <summary>
        /// 获取满足条件的核销规则
        /// </summary>
        /// <param name="shopType"></param>
        /// <param name="shopId"></param>
        /// <param name="pid"></param>
        /// <returns></returns>
        Task<List<VerificationRuleDO>> GetRule(int shopType, long shopId, string pid);


        Task<PagedEntity<VerificationRuleDO>> GetVerificationRule(GetVerificationRuleRequest request);

        Task<List<VerificationRuleDO>> GetRuleForPackageCard(int shopType, long shopId, long ruleId);

        Task<List<VerificationRuleDO>> GetRuleForPackage(string pid);
    }
}
