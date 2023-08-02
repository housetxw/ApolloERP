using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Product.Service.Dal.Model.Condition
{
    public class SearchShopProductCondition
    {
        /// <summary>
        /// 搜索关键字
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        ///  分类ID
        /// </summary>
        public List<int> CategoryId { get; set; }

        /// <summary>
        ///  门店ID
        /// </summary>
        public long ShopId { get; set; }

        /// <summary>
        ///  是否上架  0 否 1 是
        /// </summary>
        public int? IsOnSale { get; set; }

        /// <summary>
        /// 是否外采
        /// </summary>
        public int? IsStoreoutside { get; set; }

        /// <summary>
        /// 是否禁用
        /// </summary>
        public int? IsDisabled { get; set; }

        /// <summary>
        /// PageIndex
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// PageSize
        /// </summary>
        public int PageSize { get; set; }
    }
}
