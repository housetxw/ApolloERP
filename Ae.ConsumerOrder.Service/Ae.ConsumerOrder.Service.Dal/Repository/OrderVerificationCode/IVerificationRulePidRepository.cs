﻿using System.Collections.Generic;
using System.Threading.Tasks;
using ApolloErp.Data.DapperExtensions;
using Ae.ConsumerOrder.Service.Dal.Model;

namespace Ae.ConsumerOrder.Service.Dal.Repository
{
    public interface IVerificationRulePidRepository : IRepository<VerificationRulePidDO>
    {
        Task<IEnumerable<VerificationRulePidDO>> GetListByRuleId(long ruleId);
    }
}
