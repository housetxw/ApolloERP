using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Account.Api.Core.Model
{
    public class RoleAuthorityVO
    {
        public long Id { get; set; }

        public long RoleId { get; set; }

        public long AuthorityId { get; set; }

        /// <summary>
        /// 标志前端树结构，节点是否真正选中；(为了前端比较方便的渲染权限树，而冗余的一个字段，无任何实际的业务含义)
        /// </summary>
        public bool HalfCheck { get; set; }

        public bool IsDeleted { get; set; }

        public string CreateBy { get; set; } = string.Empty;

        public DateTime CreateTime { get; set; } = DateTime.Now;

        public string UpdateBy { get; set; } = string.Empty;

        public DateTime UpdateTime { get; set; } = DateTime.Now;
    }
}
