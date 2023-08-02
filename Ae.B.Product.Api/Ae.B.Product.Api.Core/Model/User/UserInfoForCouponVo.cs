using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Product.Api.Core.Model.User
{
    /// <summary>
    /// 用户信息
    /// </summary>
    public class UserInfoForCouponVo
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// 用户姓名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName { get; set; }

        /// <summary>
        /// 性别（0未设置 1男 2女）
        /// </summary>
        public string Gender { get; set; }

        /// <summary>
        /// 用户手机号
        /// </summary>
        public string UserTel { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool DefaultCheck { get; set; }
    }
}
