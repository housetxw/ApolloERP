using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.BaoYang.Service.Core.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class PagerModel
    {
        /// <summary>
        /// 当前页面
        /// </summary>
        public int CurrentPage { get; set; }
        /// <summary>
        /// 页面条数
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// 总页面数
        /// </summary>
        public int TotalPage { get; set; }
    }
}
