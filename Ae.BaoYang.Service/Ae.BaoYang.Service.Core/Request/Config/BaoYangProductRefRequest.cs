using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.BaoYang.Service.Core.Request.Config
{
    /// <summary>
    /// 配件号关联产品列表
    /// </summary>
    public class BaoYangProductRefRequest
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

        /// <summary>
        /// 页码
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// 步长
        /// </summary>
        public int PageSize { get; set; }
    }
}
