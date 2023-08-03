using Ae.Receive.Service.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Receive.Service.Core.Response
{
    public class ReservedListResponse
    {
        /// <summary>
        /// 已预约列表
        /// </summary>
        public List<ReservedInfoDTO> ReservedList { get; set; }
        /// <summary>
        /// 总数量
        /// </summary>
        public int TotalItems { get; set; }
    }
}
