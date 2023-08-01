using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.BaoYang.Service.Core.Model
{
    /// <summary>
    /// 商品更多列表返回实体
    /// </summary>
    /// <typeparam name="T"></typeparam>
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
}
