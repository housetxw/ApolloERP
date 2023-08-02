using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Product.Api.Core.Request.Product
{
    public class SearchProductForPackageCardRequest : BasePageRequest
    {
        /// <summary>
        /// 搜索关键字
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        /// 品牌
        /// </summary>
        public string Brand { get; set; }

        /// <summary>
        /// 一级类目
        /// </summary>
        public int? MainCategoryId { get; set; }

        /// <summary>
        /// 二级分类
        /// </summary>
        public int? SecondCategoryId { get; set; }

        /// <summary>
        /// 三级分类
        /// </summary>
        public int? ChildCategoryId { get; set; }
    }
}
