using ApolloErp.Data.DapperExtensions;
using Ae.Receive.Service.Dal.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Ae.Receive.Service.Dal.Model.Extend;

namespace Ae.Receive.Service.Dal.Repositorys
{
    public interface IReserveTrackRepository: IRepository<ReserveTrackDO>
    {
        /// <summary>
        /// 预约处理记录
        /// </summary>
        /// <param name="reserveId"></param>
        /// <returns></returns>
        Task<List<ReserveTrackLogDo>> GetReserveTrackList(long reserveId);
    }
}
