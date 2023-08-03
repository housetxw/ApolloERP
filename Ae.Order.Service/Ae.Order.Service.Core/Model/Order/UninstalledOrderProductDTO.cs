using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Order.Service.Core.Model
{
    public class UninstalledOrderProductDTO
    {
        /// <summary>
        /// 子系统订单商品Id（同步关联）
        /// </summary>
        public long OrderProductId { get; set; }
        /// <summary>
        /// 产品性质（0 实物产品、1 套餐产品、2 服务产品、3 数字产品）
        /// </summary>
        public sbyte ProductAttribute { get; set; }
        /// <summary>
        /// 所属父级订单套餐产品Id
        /// </summary>
        public long ParentOrderPackagePid { get; set; }
        /// <summary>
        /// 产品名称
        /// </summary>
        public string ProductName { get; set; } = string.Empty;
        /// <summary>
        /// 图片路径
        /// </summary>
        public string ImageUrl { get; set; } = string.Empty;
        /// <summary>
        /// 单位（个/升/斤等）
        /// </summary>
        public string Unit { get; set; } = string.Empty;
        /// <summary>
        /// 总数量（指乘以购买数量后）
        /// </summary>
        public int TotalNumber { get; set; }
        /// <summary>
        /// 产品总价
        /// </summary>
        public decimal TotalAmount { get; set; }
    }
}
