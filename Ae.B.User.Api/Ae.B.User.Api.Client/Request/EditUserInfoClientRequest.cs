﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.User.Api.Client.Request
{
    public class EditUserInfoClientRequest
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// 提交人
        /// </summary>
        public string SubmitBy { get; set; }

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
        /// 个性签名
        /// </summary>
        public string PersonalSign { get; set; }

        /// <summary>
        /// 用户手机号
        /// </summary>
        public string UserTel { get; set; }

        /// <summary>
        /// 身份证
        /// </summary>
        public string IdNumber { get; set; }

        /// <summary>
        /// 驾驶证到期日
        /// </summary>
        public string DriverLicenseExpiry { get; set; }

        /// <summary>
        /// 通讯地址
        /// </summary>
        public string Address { get; set; }
    }
}
