using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ApolloErp.Data.DapperExtensions;
using Ae.User.Service.Dal.Model;

namespace Ae.User.Service.Dal.Repository
{
    public interface IUserBankCardRepository : IRepository<UserBankCardDO>
    {
        /// <summary>
        /// 获取用户银行卡信息
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="cardNumber"></param>
        /// <param name="readOnly"></param>
        /// <returns></returns>
        Task<List<UserBankCardDO>> GetUserBank(string userId, string cardNumber = "", bool readOnly = true);
    }
}
