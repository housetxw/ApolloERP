using System;
using System.Collections.Generic;
using System.Text;
using ApolloErp.Data.DapperExtensions.Att;

namespace Ae.AccountAuthority.Service.Dal.Model
{
    [Table("role_authority")]
    public class RoleAuthorityDO
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }

        [Column("role_id")]
        public long RoleId { get; set; }

        [Column("authority_id")]
        public long AuthorityId { get; set; }

        /// <summary>
        /// 标志前端树结构，节点是否真正选中；(为了前端比较方便的渲染权限树，而冗余的一个字段，无任何实际的业务含义)
        /// </summary>
        [Column("half_check")]
        public bool HalfCheck { get; set; }

        [Column("is_deleted")]
        public bool IsDeleted { get; set; }

        [Column("create_by")]
        public string CreateBy { get; set; }

        [Column("create_time")]
        public DateTime CreateTime { get; set; } = DateTime.Now;

        [Column("update_by")]
        public string UpdateBy { get; set; }

        [Column("update_time")]
        public DateTime UpdateTime { get; set; } = DateTime.Now;
    }

    public class RoleAuthorityListReqByRoleIdDO
    {
        public long RoleId { get; set; }
    }

    public class RoleAuthorityReqDO
    {
        public long RoleId { get; set; }

        public string CreateBy { get; set; }

        public DateTime CreateTime { get; set; }

        public string UpdateBy { get; set; }

        public DateTime UpdateTime { get; set; }

        public List<RoleAuthorityDO> AddList { get; set; }

        //public List<RoleAuthorityDO> DeleteList { get; set; }
    }
}
