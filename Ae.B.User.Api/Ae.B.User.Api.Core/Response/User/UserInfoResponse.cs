using System;
using System.Collections.Generic;
using System.Text;
using Ae.B.User.Api.Core.Model.User;

namespace Ae.B.User.Api.Core.Response.User
{
    /// <summary>
    /// 用户基本信息
    /// </summary>
    public class UserInfoResponse
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
        /// 联系姓名
        /// </summary>
        public string ContactName { get; set; }

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

        /// <summary>
        /// 用户车型信息
        /// </summary>
        public List<UserVehicleVo> Vehicles { get; set; }

        /// <summary>
        /// 用户地址
        /// </summary>
        public List<UserAddressVo> Addresses { get; set; }
    }
}
