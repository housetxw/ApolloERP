using ApolloErp.Data.DapperExtensions.Att;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.User.Service.Dal.Model
{
    [Table("user_expand_shop")]
    public class UserExpandShopDo
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        [Column("id")]
        public long Id { get; set; }

        /// <summary>
        /// 用户id
        /// </summary>
        [Column("user_id")]
        public Guid UserId { get; set; }

        /// <summary>
        /// 店长用户shopId
        /// </summary>
        [Column("relation_shop_id")]
        public long RelationShopId { get; set; }

        /// <summary>
        /// 推荐店shopId，每一次分享都需要更新
        /// </summary>
        [Column("recommend_shop_id")]
        public long RecommendShopId { get; set; }

        /// <summary>
        /// 推荐店长用户Id
        /// </summary>
        [Column("recommend_user_id")]
        public Guid RecommendUserId { get; set; } = Guid.Empty;

        /// <summary>
        /// 删除标记
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
        /// 更新人
        /// </summary>
        [Column("update_by")]
        public string UpdateBy { get; set; } = string.Empty;

        /// <summary>
        /// 更新时间
        /// </summary>
        [Column("update_time")]
        public DateTime UpdateTime { get; set; } = new DateTime(1900, 1, 1);
    }
}
