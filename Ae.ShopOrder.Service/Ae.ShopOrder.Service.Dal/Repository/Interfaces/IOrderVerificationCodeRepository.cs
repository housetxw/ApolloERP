using ApolloErp.Data.DapperExtensions;
using Ae.ShopOrder.Service.Core.Request.Order;
using Ae.ShopOrder.Service.Dal.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.ShopOrder.Service.Dal.Repository.Interfaces
{
    public interface IOrderVerificationCodeRepository : IRepository<OrderVerificationCodeDO>
    {
        Task<OrderVerificationCodeDO> GetByCode(string code);
        Task<List<OrderVerificationCodeDO>> GetListByUserIdAndOrderNo(string userId, string orderNo, bool useMaster = false);

        Task<PagedEntity<OrderVerificationCodeDO>> GetListPagedByCondition(GetVerificationCodeOrderListRequest request);
        /// <summary>
        /// 根据订单号激活核销码
        /// </summary>
        /// <param name="orderNo"></param>
        /// <returns></returns>
        Task<bool> ActiveByOrderNo(string orderNo);
    }
}
