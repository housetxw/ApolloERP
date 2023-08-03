using ApolloErp.Data.DapperExtensions;
using Ae.Shop.Api.Core.Request;
using Ae.Shop.Api.Dal.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.Shop.Api.Dal.Repositorys
{
    public interface IReceiveRepository : IRepository<SignDetailDO>
    {
        /// <summary>
        /// 获取今日签收包裹
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<PagedEntity<SignDetailDO>> GetTodaySignList(GetTodayReceivePackageRequest request);

        Task<List<SignDetailDO>> GetSignList(List<string> packageNos,long shopId);

        Task<long> SaveSign(SignDO signDO,List<SignDetailDO> details);

        Task<int> UpdateSignStatus(SignDO request);
    }
}
