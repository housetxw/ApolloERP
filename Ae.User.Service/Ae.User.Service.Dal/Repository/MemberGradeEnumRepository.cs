using ApolloErp.Data.DapperExtensions;
using Ae.User.Service.Dal.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.User.Service.Dal.Repository
{
    public class MemberGradeEnumRepository : AbstractRepository<MemberGradeEnumDO>, IMemberGradeEnumRepository
    {
        public MemberGradeEnumRepository()
        {
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("User");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("UserReadOnly");
        }

        /// <summary>
        /// 会员等级
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<MemberGradeEnumDO>> GetMemberGradeEnumAsync()
        {
            var result = await GetListAsync<MemberGradeEnumDO>("");

            return result;
        }
    }
}
