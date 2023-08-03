using ApolloErp.Data.DapperExtensions;
using Ae.ConsumerOrder.Service.Dal.Model.OrderVerificationCode;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.ConsumerOrder.Service.Dal.Repository.OrderVerificationCode
{
    public interface IOrderPackageCardRepository : IRepository<OrderPackageCardDO>
    {
        /// <summary>
        /// 根据订单号激活核销码
        /// </summary>
        /// <param name="orderNo"></param>
        /// <returns></returns>
        Task<bool> ActiveByOrderNo(string orderNo);

      
    }
}
