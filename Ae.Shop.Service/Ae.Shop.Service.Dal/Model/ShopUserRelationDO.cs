using ApolloErp.Data.DapperExtensions.Att;
using System;
using System.Collections.Generic;
using System.Text;
using Ae.Shop.Service.Common.Constant;

namespace Ae.Shop.Service.Dal.Model
{
    [Table("shop_user_relation")]
    public class ShopUserRelationDO
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        [Column("id")]
        public long Id { get; set; }
        /// <summary>
        /// 用户Id
        /// </summary>
        [Column("user_id")]
        public Guid UserId { get; set; }
        /// <summary>
        /// 门店Id
        /// </summary>
        [Column("shop_id")]
        public long ShopId { get; set; }
        /// <summary>
        /// 100普通会员  200高级会员  300VIP贵宾
        /// </summary>
        [Column("member_tag")]
        public int MemberTag { get; set; }
        /// <summary>
        /// 最后一次到店Id
        /// </summary>
        [Column("last_arrive_id")]
        public long LastArriveId { get; set; }
        /// <summary>
        /// 最后到店时间
        /// </summary>
        [Column("last_arrive_time")]
        public DateTime? LastArriveTime { get; set; }

        /// <summary>
        /// 最后一次下单No
        /// </summary>
        [Column("last_order_no")]
        public string LastOrderNo { get; set; } = string.Empty;

        /// <summary>
        /// 最后一次下单时间
        /// </summary>
        [Column("last_order_time")]
        public DateTime? LastOrderTime { get; set; }

        [Column("channel")]
        public int Channel { get; set; } = CommonConstant.Zero;
        [Column("referrer_type")]
        public int ReferrerType { get; set; } = CommonConstant.Zero;
        [Column("referrer_shop_id")]
        public long ReferrerShopId { get; set; } = CommonConstant.Zero;
        [Column("referrer_user_id")]
        public string ReferrerUserId { get; set; } = string.Empty;

        /// <summary>
        /// 首单消费标识
        /// </summary>
        [Column("is_first_order")]
        public int IsFirstOrder { get; set; }
        /// <summary>
        /// 标记删除
        /// </summary>
        [Column("is_deleted")]
        public bool IsDeleted { get; set; }
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
        /// 更新人
        /// </summary>
        [Column("update_by")]
        public string UpdateBy { get; set; } = String.Empty;

        /// <summary>
        /// 更新时间
        /// </summary>
        [Column("update_time")]
        public DateTime UpdateTime { get; set; } = new DateTime(1900, 1, 1);
    }
}
