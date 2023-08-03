using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.OrderComment.Service.Client.Model
{
    public class UserBaseInfoDto
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
        /// 性别（0未设置 1男 2女）
        /// </summary>
        public int Gender { get; set; }

        /// <summary>
        /// 生日
        /// </summary>
        public string BirthDay { get; set; }

        /// <summary>
        /// 头像
        /// </summary>
        public string HeadUrl { get; set; }

        /// <summary>
        /// 用户手机号[脱敏]
        /// </summary>
        public string UserTelDes { get; set; }

        /// <summary>
        /// 用户手机号
        /// </summary>
        public string UserTel { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }

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

        /// <summary>
        /// 注册时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 关联店铺id
        /// </summary>
        public long RelationShopId { get; set; }
    }
}
