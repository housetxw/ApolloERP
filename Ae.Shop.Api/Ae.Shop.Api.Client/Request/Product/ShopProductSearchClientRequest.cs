using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Api.Client.Request.Product
{
    public class ShopProductSearchClientRequest
    {
        /// <summary>
        /// 搜索关键字
        /// </summary>
        public string Key { get; set; }
        /// <summary>
        ///  分类ID
        /// </summary>
        public int? CategoryId { get; set; }

        /// <summary>
        ///  门店ID
        /// </summary>
 
        public long ShopId { get; set; }

        /// <summary>
        /// 是否禁用 0 否 1 是
        /// </summary>
        public int? IsDeleted { get; set; }

        /// <summary>
        ///  是否上架  0 否 1 是
        /// </summary>
        public int? IsOnSale { get; set; }

        /// <summary>
        /// 审核状态 1 通过 2 审核中 3驳回
        /// </summary>
        public int? AuditStatus { get; set; }

        /// <summary>
        /// 是否外采
        /// </summary>
        public int? IsStoreoutside { get; set; }

        /// <summary>
        /// 是否禁用
        /// </summary>
        public int? IsDisabled { get; set; }

        public List<long> shopIds { get; set; } = new List<long>();
    }
}
