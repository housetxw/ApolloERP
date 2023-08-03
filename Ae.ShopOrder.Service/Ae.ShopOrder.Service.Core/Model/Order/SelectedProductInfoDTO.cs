using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.ShopOrder.Service.Core.Model.Order
{
    /// <summary>
    ///选择商品
    /// </summary>
    public class SelectedProductInfoDTO
    {
        /// <summary>
        /// 产品编号
        /// </summary>
        public string Pid { get; set; }
        /// <summary>
        /// 购买数量
        /// </summary>
        public int Number { get; set; }

        /// <summary>
        /// 促销的Id
        /// </summary>
        public string ActivityId { get; set; }

        /// <summary>
        /// 商品的自营属性(0:自营，1：shop)
        /// </summary>
        public int ProductOwnAttri { get; set; }

        /// <summary>
        /// 价格
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// 备注描述
        /// </summary>
        public string Remark { get; set; }
    }
}
