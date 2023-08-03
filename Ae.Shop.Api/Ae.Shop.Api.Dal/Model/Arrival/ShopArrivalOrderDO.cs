using ApolloErp.Data.DapperExtensions.Att;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Api.Dal.Model.Arrival
{
    [Table("shop_arrival_order")]
    public class ShopArrivalOrderDO
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        [Column("id")]
        public long Id { get; set; }
        /// <summary>
        /// 到店Id
        /// </summary>
        [Column("arrival_id")]
        public long ArrivalId { get; set; }
        /// <summary>
        /// 订单号
        /// </summary>
        [Column("order_no")]
        public string OrderNo { get; set; }
        /// <summary>
        /// 订单类型（0未设置 1轮胎 2保养 3美容）
        /// </summary>
        [Column("order_type")]
        public string OrderType { get; set; }
        /// <summary>
        /// 产品名称
        /// </summary>
        [Column("product_name")]
        public string ProductName { get; set; }
        /// <summary>
        /// 产品PID
        /// </summary>
        [Column("pid")]
        public string Pid { get; set; }
        /// <summary>
        /// 销售单价
        /// </summary>
        [Column("price")]
        public decimal Price { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        [Column("num")]
        public sbyte Num { get; set; }
        /// <summary>
        /// 删除标识
        /// </summary>
        [Column("is_deleted")]
        public sbyte IsDeleted { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        [Column("create_by")]
        public string CreateBy { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        [Column("create_time")]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        [Column("update_time")]
        public DateTime UpdateTime { get; set; }
        /// <summary>
        /// 修改人
        /// </summary>
        [Column("update_by")]
        public string UpdateBy { get; set; }
    }
}
