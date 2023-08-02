using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Product.Service.Core.Request.Product
{
    /// <summary>
    /// SearchReplaceProductRequest
    /// </summary>
    public class SearchReplaceProductRequest : BasePageRequest
    {
        /// <summary>
        /// 套餐明细主键
        /// </summary>
        public int PackageDetailId { get; set; }

        /// <summary>
        /// 产品pid
        /// </summary>
        public string Pid { get; set; }

        /// <summary>
        /// 查询产品类型：0 直营/外采  1直营  2外采
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// 搜索类容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 门店Id
        /// </summary>
        public int ShopId { get; set; }
    }
}
