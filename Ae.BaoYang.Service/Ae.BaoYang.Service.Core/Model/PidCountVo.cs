using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.BaoYang.Service.Core.Model
{
    /// <summary>
    /// 商品数量
    /// </summary>
    public class PidCountVo
    {
        /// <summary>
        /// 商品Pid
        /// </summary>
        public string Pid { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public int Count { get; set; }
    }
}
