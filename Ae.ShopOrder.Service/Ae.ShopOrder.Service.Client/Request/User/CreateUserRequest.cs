using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.ShopOrder.Service.Client.Request.User
{
    public class CreateUserRequest
    {
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

        public string PersonalSign { get; set; }


        /// <summary>
        /// 用户手机号
        /// </summary>
        public string UserTel { get; set; }

        public string Channel { get; set; }

        public string ReferrerType { get; set; }

        public string IdNumber { get; set; }

        public string Address { get; set; }

        public string DriverLicenseExpiry { get; set; }


    }
}
