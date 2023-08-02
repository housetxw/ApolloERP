using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Product.Service.Core.Model.Promotion
{
    /// <summary>
    /// 商品活动Model
    /// </summary>
    public class ProductActivityVo
    {
        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderNo { get; set; }

        /// <summary>
        /// 商品Pid
        /// </summary>
        public string ProductCode { get; set; }

        /// <summary>
        /// 活动Id
        /// </summary>
        public string ActivityId { get; set; }

        /// <summary>
        /// 商品数量
        /// </summary>
        public int Num { get; set; }
    }
}
