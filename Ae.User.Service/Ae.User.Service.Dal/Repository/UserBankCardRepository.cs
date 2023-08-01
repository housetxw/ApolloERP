using Dapper;
using ApolloErp.Data.DapperExtensions;
using Ae.User.Service.Dal.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ae.User.Service.Dal.Repository
{
    public class UserBankCardRepository : AbstractRepository<UserBankCardDO>, IUserBankCardRepository
    {
        public UserBankCardRepository()
        {
            SetDbType(DbType.MySql);
            ConnectionString = ConnectionStringConfig.GetConnectionString("User");
            SlaveConnectionString = ConnectionStringConfig.GetConnectionString("UserReadOnly");
        }

        /// <summary>
        /// 获取用户银行卡信息
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="cardNumber"></param>
        /// <param name="readOnly"></param>
        /// <returns></returns>
        public async Task<List<UserBankCardDO>> GetUserBank(string userId, string cardNumber = "", bool readOnly = true)
        {
            var param = new DynamicParameters();
            StringBuilder condition = new StringBuilder();
            condition.Append("WHERE user_id = @userId");
            param.Add("@userId", userId);
            if (!string.IsNullOrWhiteSpace(cardNumber))
            {
                condition.Append(" AND card_number = @cardNumber");
                param.Add("@cardNumber", cardNumber);
            }

            var result = await GetListAsync(condition.ToString(), param, !readOnly);

            return result?.ToList() ?? new List<UserBankCardDO>();
        }
    }
}
