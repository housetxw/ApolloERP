using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Product.Api.Core.Request.BaoYang
{
    /// <summary>
    /// 
    /// </summary>
    public class BaoYangProductRefRequest : BasePageRequest
    {
        /// <summary>
        /// 配件号
        /// </summary>
        public string PartCode { get; set; }

        /// <summary>
        /// 商品Pid
        /// </summary>
        public string Pid { get; set; }

        /// <summary>
        /// 开始日期
        /// </summary>
        public string StartDate { get; set; }

        /// <summary>
        /// 截止日期
        /// </summary>
        public string EndDate { get; set; }
    }
}
