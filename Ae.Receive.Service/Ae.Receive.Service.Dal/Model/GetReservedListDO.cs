using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Receive.Service.Dal.Model
{
    public class GetReservedListDO
    {
        /// <summary>
        /// 列表
        /// </summary>
        public List<ReserveInfoDO> List { get; set; }
        /// <summary>
        /// 总数
        /// </summary>
        public int TotalItems { get; set; }
    }
}
