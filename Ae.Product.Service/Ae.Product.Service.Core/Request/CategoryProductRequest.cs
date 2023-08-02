using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Product.Service.Core.Request
{
    public class CategoryProductRequest : PageListQuery
    {
        /// <summary>
        /// 类目Id
        /// </summary>
        public int? CategoryId { get; set; }


        /// <summary>
        /// 是否包括属性信息
        /// </summary>
        public bool HasAttribute { get; set; } = false;

        /// <summary>
        /// 是否包含安装服务
        /// </summary>
        public bool HasInstallService { get; set; } = false;


        /// <summary>
        ///  是否上架  0 否 1 是
        /// </summary>
        public int? IsOnSale { get; set; }
    }
}
