using Ae.Shop.Api.Core.Model.Reserve;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ae.Shop.Api.Core.Response.Reserve
{
    /// <summary>
    /// 
    /// </summary>
    public class ReserveDateForWebResponse
    {
        /// <summary>
        /// 日期
        /// </summary>
        public List<string> DatePartList { get; set; }

        /// <summary>
        /// 时间
        /// </summary>
        public List<string> TimePartList { get; set; }

        /// <summary>
        /// 预约数据
        /// </summary>
        public List<ReserveDateVo> ReserveDateInfoList { get; set; } = new List<ReserveDateVo>();
    }
}
