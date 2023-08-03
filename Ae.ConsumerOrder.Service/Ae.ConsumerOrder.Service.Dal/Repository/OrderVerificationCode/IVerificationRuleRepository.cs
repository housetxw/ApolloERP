using System.Collections.Generic;
using System.Threading.Tasks;
using ApolloErp.Data.DapperExtensions;
using Ae.ConsumerOrder.Service.Core.Model;
using Ae.ConsumerOrder.Service.Dal.Model;

namespace Ae.ConsumerOrder.Service.Dal.Repository
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

        /// <summary>
        /// 获取满足条件的核销规则
        /// </summary>
        /// <param name="shopType"></param>
        /// <param name="shopId"></param>
        /// <param name="pid"></param>
        /// <returns></returns>
        Task<List<VerificationRuleDO>> GetRuleForPackage(string pid);


        /// <summary>
        /// 获取满足条件的核销规则
        /// </summary>
        /// <param name="shopType"></param>
        /// <param name="shopId"></param>
        /// <param name="pid"></param>
        /// <returns></returns>
        Task<List<VerificationRuleDO>> GetRuleForPackageCard(int shopType, long shopId, long ruleId);
    }
}
