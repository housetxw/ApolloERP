using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Product.Service.Core.Request.Product
{
    /// <summary>
    /// SearchShopProductCommon
    /// </summary>
    public class SearchShopProductCommon : BasePageRequest
    {
        /// <summary>
        /// 输入搜索内容
        /// </summary>
        public string Content { get; set; } = string.Empty;

        /// <summary>
        ///  产品性质，分为 0 实物产品、1 套餐产品、2 服务产品、3 数字产品 默认
        /// </summary>
        public List<int> ProductAttributes { get; set; }

        /// <summary>
        /// 一级类目Id
        /// </summary>
        public int MainCategoryId { get; set; }

        /// <summary>
        /// 二级类目Id
        /// </summary>
        public int SecondCategoryId { get; set; }

        /// <summary>
        /// 三级类目Id
        /// </summary>
        public int ChildCategoryId { get; set; }

        /// <summary>
        /// 查询产品类型：0 直营/外采  1直营  2外采
        /// </summary>
        public int ProductType { get; set; }

        /// <summary>
        /// 门店id
        /// </summary>
        public int ShopId { get; set; }
    }
}
