using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Product.Api.Client.Model.User
{
    public class UserInfoDto : UserBaseInfoDto
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
