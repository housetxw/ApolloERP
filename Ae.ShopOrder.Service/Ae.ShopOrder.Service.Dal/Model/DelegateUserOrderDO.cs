using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Ae.ShopOrder.Service.Dal.Model
{
    [Table("delegate_user_order")]
    public class DelegateUserOrderDO
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        [Column("id")]
        public long Id { get; set; }
        /// <summary>
        /// 订单号（RGC或RGS前缀）
        /// </summary>
        [Column("order_no")]
        public string OrderNo { get; set; } = string.Empty;
        /// <summary>
        /// 订单分类
        /// </summary>
        [Column("produce_type")]
        public sbyte ProductType { get; set; } = -1;
        /// <summary>
        /// 代理者Id
        /// </summary>
        [Column("user_id")]
        public string UserId { get; set; } = string.Empty;
        /// <summary>
        /// 代理姓名
        /// </summary>
        [Column("user_name")]
        public string UserName { get; set; } = string.Empty;
        /// <summary>
        /// 门店Id
        /// </summary>
        [Column("shop_id")]
        public long ShopId { get; set; }
        /// <summary>
        /// 关联的月度结算订单号（RGC或RGS前缀）
        /// </summary>
        [Column("ref_order_no")]
        public string RefOrderNo { get; set; } = string.Empty;
        /// <summary>
        /// 删除标识
        /// </summary>
        [Column("is_deleted")]
        public sbyte IsDeleted { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        [Column("create_by")]
        public string CreateBy { get; set; } = string.Empty;
        /// <summary>
        /// 创建时间
        /// </summary>
        [Column("create_time")]
        public DateTime CreateTime { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 修改人
        /// </summary>
        [Column("update_by")]
        public string UpdateBy { get; set; } = string.Empty;
        /// <summary>
        /// 修改时间
        /// </summary>
        [Column("update_time")]
        public DateTime UpdateTime { get; set; } = new DateTime(1900, 1, 1);
    }
}
