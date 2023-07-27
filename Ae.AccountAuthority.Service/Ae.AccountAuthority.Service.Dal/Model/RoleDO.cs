using System;
using ApolloErp.Data.DapperExtensions.Att;
using Ae.AccountAuthority.Service.Core.Model;

namespace Ae.AccountAuthority.Service.Dal.Model
{
    [Table("role")]
    public class RoleDO
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }

        /// <summary>
        /// 角色名称
        /// </summary>
        [Column("name")]
        public string Name { get; set; }

        /// <summary>
        /// 0：公司；1：门店；2：仓库；
        /// </summary>
        [Column("type")]
        public RoleType Type { get; set; }

        /// <summary>
        /// 所属单位Id
        /// </summary>
        [Column("organization_id")]
        public long OrganizationId { get; set; }

        /// <summary>
        /// 角色属性门店的0：精简 1：旗舰 2：个人 ；公司的1平台公司，2普通公司，3区域合伙公司
        /// </summary>
        [Column("features")]
        public int Features { get; set; }

        /// <summary>
        /// 类型 0公司 1合作店 2直营店 4上门养车 8认证店
        /// </summary>
        [Column("shop_type")]
        public sbyte ShopType { get; set; }

        [Column("description")]
        public string Description { get; set; }

        [Column("is_deleted")]
        public bool IsDeleted { get; set; }

        [Column("create_by")]
        public string CreateBy { get; set; } = string.Empty;

        [Column("create_time")]
        public DateTime CreateTime { get; set; } = DateTime.Now;

        [Column("update_by")]
        public string UpdateBy { get; set; } = string.Empty;

        [Column("update_time")]
        public DateTime UpdateTime { get; set; } = DateTime.Now;
    }
}
