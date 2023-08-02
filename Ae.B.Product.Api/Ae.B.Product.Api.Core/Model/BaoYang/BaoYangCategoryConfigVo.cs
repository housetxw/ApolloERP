using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Product.Api.Core.Model.BaoYang
{
    /// <summary>
    /// BaoYangCategoryConfigVo
    /// </summary>
    public class BaoYangCategoryConfigVo
    {
        /// <summary>
        /// 一级分类code
        /// </summary>
        public string CategoryAlias { get; set; }

        /// <summary>
        /// 一级分类名称
        /// </summary>
        public string CategoryName { get; set; }

        /// <summary>
        /// 一级分类简称
        /// </summary>
        public string CategorySimpleName { get; set; }
    }
}
