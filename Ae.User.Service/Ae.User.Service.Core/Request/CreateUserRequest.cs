using Ae.User.Service.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.User.Service.Core.Request
{
    /// <summary>
    /// 创建用户Request
    /// </summary>
    public class CreateUserRequest
    {
        /// <summary>
        /// 提交人
        /// </summary>
        [Required(ErrorMessage = "提交人不能为空")]
        public string SubmitBy { get; set; }

        /// <summary>
        /// 提交门店ID
        /// </summary>
        public int ShopId { get; set; }

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

        /// <summary>
        /// 推荐人门店Id
        /// </summary>
        public int ReferrerShopId { get; set; }

        /// <summary>
        /// 推荐人Id
        /// </summary>
        public string ReferrerUserId { get; set; } = string.Empty;

        /// <summary>
        /// 身份证
        /// </summary>
        public string IdNumber { get; set; } = string.Empty;

        /// <summary>
        /// 驾驶证到期日
        /// </summary>
        public string DriverLicenseExpiry { get; set; } = string.Empty;

        /// <summary>
        /// 通讯地址
        /// </summary>
        public string Address { get; set; } = string.Empty;
    }
}
