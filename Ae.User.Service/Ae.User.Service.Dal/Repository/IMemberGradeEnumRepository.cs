using ApolloErp.Data.DapperExtensions;
using Ae.User.Service.Dal.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.User.Service.Dal.Repository
{
    public interface IMemberGradeEnumRepository : IRepository<MemberGradeEnumDO>
    {
        /// <summary>
        /// 会员等级
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<MemberGradeEnumDO>> GetMemberGradeEnumAsync();
    }
}
