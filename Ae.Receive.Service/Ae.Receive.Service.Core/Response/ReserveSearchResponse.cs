using Ae.Receive.Service.Core.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Receive.Service.Core.Response
{
    /// <summary>
    /// 预约搜索Response
    /// </summary>
    public class ReserveSearchResponse
    {
        /// <summary>
        /// 预约数量
        /// </summary>
        public int ReserveCount { get; set; }

        /// <summary>
        /// 预约列表
        /// </summary>
        public List<ReserveListVo> ReserveList { get; set; } = new List<ReserveListVo>();
    }
}
