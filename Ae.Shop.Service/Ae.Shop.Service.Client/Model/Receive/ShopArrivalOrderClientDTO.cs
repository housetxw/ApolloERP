using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Service.Client.Model.Receive
{
   public class ShopArrivalOrderClientDTO
    {
        public string UserId { get; set; }

        /// <summary>
        /// 主键
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 到店Id
        /// </summary>
        public long ArrivalId { get; set; }
        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderNo { get; set; } = string.Empty;
        /// <summary>
        /// 订单类型（0未设置 1轮胎 2保养 3美容）
        /// </summary>
        public string OrderType { get; set; } = string.Empty;
        /// <summary>
        /// Pid
        /// </summary>
        public string Pid { get; set; } = string.Empty;
        /// <summary>
        /// 产品名称
        /// </summary>
        public string ProductName { get; set; } = string.Empty;
        /// <summary>
        /// 销售单价
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public sbyte Num { get; set; }
        /// <summary>
        /// 删除标识
        /// </summary>
        public sbyte IsDeleted { get; set; }
    }
}
