using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Product.Api.Client.Model.BaoYang
{
    public class BaoYangCategoryConfigDto
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
