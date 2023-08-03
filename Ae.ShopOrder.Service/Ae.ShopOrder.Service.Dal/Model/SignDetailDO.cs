using System;
using System.Collections.Generic;
using System.Text;
using ApolloErp.Data.DapperExtensions.Att;

namespace Ae.ShopOrder.Service.Dal.Model
{
    [Table("sign_detail")]
    public class SignDetailDO
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        [Column("id")]
        public long Id { get; set; }
        /// <summary>
        /// 签收记录id
        /// </summary>
        [Column("sign_id")]
        public long SignId { get; set; }
        /// <summary>
        /// 关联单号
        /// </summary>
        [Column("relation_no")]
        public string RelationNo { get; set; } = string.Empty;
        /// <summary>
        /// 物流单号
        /// </summary>
        [Column("package_no")]
        public string PackageNo { get; set; } = string.Empty;
        /// <summary>
        /// 签收用户
        /// </summary>
        [Column("sign_user")]
        public string SignUser { get; set; } = string.Empty;
        /// <summary>
        /// 签收时间
        /// </summary>
        [Column("sign_time")]
        public DateTime SignTime { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 门店id
        /// </summary>
        [Column("shop_id")]
        public long ShopId { get; set; }
        /// <summary>
        /// 订单备注
        /// </summary>
        [Column("remark")]
        public string Remark { get; set; } = string.Empty;
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
