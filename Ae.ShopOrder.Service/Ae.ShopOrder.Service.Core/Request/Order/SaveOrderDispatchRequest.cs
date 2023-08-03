using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.ShopOrder.Service.Core.Request.Order
{
    public class SaveOrderDispatchRequest
    {
        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderNo { get; set; }

        /// <summary>
        /// 技师的Id
        /// </summary>
        public string TechId { get; set; } = string.Empty;
        /// <summary>
        /// 技师姓名
        /// </summary>
        public string TechName { get; set; } = string.Empty;
        /// <summary>
        /// 职级
        /// </summary>
        public string Level { get; set; } = string.Empty;
        /// <summary>
        /// 门店Id
        /// </summary>
        public long ShopId { get; set; }

        public string CreateBy { get; set; }

        /// <summary>
        /// 比例
        /// </summary>
        public decimal PercentValue { get; set; }
    }
}
