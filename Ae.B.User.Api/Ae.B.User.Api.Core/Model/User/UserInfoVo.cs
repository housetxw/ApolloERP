using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.User.Api.Core.Model.User
{
    /// <summary>
    /// 搜索客户
    /// </summary>
    public class UserInfoVo : UserBaseInfoVo
    {
        /// <summary>
        /// 会员等级显示
        /// </summary>
        public string MemberLevel { get; set; }

        /// <summary>
        /// 会员等级
        /// </summary>
        public int MemberGrade { get; set; }
    }
}
