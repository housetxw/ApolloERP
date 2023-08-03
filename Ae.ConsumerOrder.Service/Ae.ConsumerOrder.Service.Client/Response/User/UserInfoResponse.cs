using Ae.ConsumerOrder.Service.Client.Model.User;
using Ae.ConsumerOrder.Service.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.ConsumerOrder.Service.Client.Response
{
    public class UserInfoResponse
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
        /// 会员等级显示
        /// </summary>
        public string MemberLevel { get; set; }

        /// <summary>
        /// 会员积分
        /// </summary>
        public int Point { get; set; }

        /// <summary>
        /// 会员等级 (0  注册会员  10 铜牌 20 银牌  30  金牌 40  铂金  50 钻石)
        /// </summary>
        public int MemberGrade { get; set; }

        /// <summary>
        /// 默认收货地址
        /// </summary>
        public UserAddressDTO DefaultAddress { get; set; }
        /// <summary>
        /// 渠道来源
        /// </summary>
        public ChannelType Channel { get; set; }

        /// <summary>
        /// 推荐人Id
        /// </summary>
        public string ReferrerUserId { get; set; } = string.Empty;
    }
}
