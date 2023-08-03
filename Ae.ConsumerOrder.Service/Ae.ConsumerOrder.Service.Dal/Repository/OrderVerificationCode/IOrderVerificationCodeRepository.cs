using System.Collections.Generic;
using System.Threading.Tasks;
using ApolloErp.Data.DapperExtensions;
using Ae.ConsumerOrder.Service.Core.Request;
using Ae.ConsumerOrder.Service.Dal.Model;

namespace Ae.ConsumerOrder.Service.Dal.Repository
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
