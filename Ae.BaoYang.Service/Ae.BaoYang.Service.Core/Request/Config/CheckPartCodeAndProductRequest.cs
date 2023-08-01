using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.BaoYang.Service.Core.Request.Config
{
    /// <summary>
    /// 
    /// </summary>
    public class CheckPartCodeAndProductRequest
    {
        /// <summary>
        /// 配件号
        /// </summary>
        public string PartCode { get; set; }

        /// <summary>
        /// 产品Pid
        /// </summary>
        public string Pid { get; set; }
    }
}
