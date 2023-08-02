using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.User.Api.Client.Request
{
    public class CreateUserClientRequest
    {
        /// <summary>
        /// 提交人
        /// </summary>
        public string SubmitBy { get; set; }

        /// <summary>
        /// 用户姓名
        /// </summary>
        public string UserName { get; set; } = string.Empty;

        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName { get; set; } = string.Empty;

        /// <summary>
        /// 头像地址
        /// </summary>
        public string HeadUrl { get; set; } = string.Empty;

        /// <summary>
        /// 性别（0未设置 1男 2女）
        /// </summary>
        public int Gender { get; set; }

        /// <summary>
        /// 生日
        /// </summary>
        public string BirthDay { get; set; } = string.Empty;

        /// <summary>
        /// 个性签名
        /// </summary>
        public string PersonalSign { get; set; } = string.Empty;

        /// <summary>
        /// 用户手机号
        /// </summary>
        public string UserTel { get; set; } = string.Empty;

        /// <summary>
        /// 渠道来源
        /// </summary>
        public ChannelType Channel { get; set; }

        /// <summary>
        /// 渠道明细
        /// </summary>
        public ReferrerType ReferrerType { get; set; }
    }

    public enum ChannelType
    {
        None = 0,

        Employee = 1,

        Consumer = 2,

        /// <summary>
        /// Boss
        /// </summary>
        Boss = 3
    }

    public enum ReferrerType
    {
        None = 0,

        Article = 1,

        Poster = 2,

        Joke = 3,

        ShopCode = 4,

        GooseAppCode = 5,

        ProductPromotion = 6,

        SelfSearch = 7,

        DirectFowardMiniApp = 8,

        /// <summary>
        /// BossWeb
        /// </summary>
        BossWeb = 9
    }
}
