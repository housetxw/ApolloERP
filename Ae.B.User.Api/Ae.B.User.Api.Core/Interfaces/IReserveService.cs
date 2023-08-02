using Ae.B.User.Api.Core.Request.Reserve;
using Ae.B.User.Api.Core.Response.Reserve;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.B.User.Api.Core.Interfaces
{
    /// <summary>
    /// 预约服务
    /// </summary>
    public interface IReserveService
    {
        /// <summary>
        /// 预约时间看板 Web端
        /// </summary>
        /// <returns></returns>
        Task<ReserveDateForWebResponse> GetReserveDateForWeb(ReserveDateForWebRequest request);
    }
}
