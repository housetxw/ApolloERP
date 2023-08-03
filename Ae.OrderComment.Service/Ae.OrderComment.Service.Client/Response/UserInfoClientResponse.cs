using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.OrderComment.Service.Client.Response
{
    public class UserInfoClientResponse
    {
        /// <summary>
        /// 用户姓名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName { get; set; }

        /// <summary>
        /// 头像地址
        /// </summary>
        public string HeadUrl { get; set; }

        /// <summary>
        /// 性别（0未设置 1男 2女）
        /// </summary>
        public int Gender { get; set; }

        /// <summary>
        /// 生日
        /// </summary>
        public string BirthDay { get; set; }

        /// <summary>
        /// 用户手机号[脱敏]
        /// </summary>
        public string UserTelDes { get; set; }

        /// <summary>
        /// 用户手机号
        /// </summary>
        public string UserTel { get; set; }
        /// <summary>
        /// 等级
        /// </summary>
        public int MemberGrade { get; set; }
        /// <summary>
        /// 会员等级显示
        /// </summary>
        public string MemberLevel { get; set; }

        /// <summary>
        /// 会员积分
        /// </summary>
        public int Point { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 个性签名
        /// </summary>
        public string PersonalSign { get; set; }
    }
}
