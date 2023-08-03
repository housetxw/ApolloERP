using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.MiniApp.Api.Core.Response.Adaptation
{
    /// <summary>
    /// 
    /// </summary>
    public class SearchProductModel<T>
    {
        /// <summary>
        /// 商品信息
        /// </summary>
        public IEnumerable<T> Products { get; set; }

        /// <summary>
        /// 分页信息
        /// </summary>
        public PagerModel Pager { get; set; }
    }

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
