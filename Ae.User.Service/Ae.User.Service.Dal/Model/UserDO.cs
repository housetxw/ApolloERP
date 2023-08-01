using ApolloErp.Data.DapperExtensions.Att;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.User.Service.Dal.Model
{
    [Table("user")]
    public class UserDO
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        [Key]
        [Column("id")]
        public Guid Id { get; set; }

        /// <summary>
        /// 用户姓名
        /// </summary>
        [Column("user_name")]
        public string UserName { get; set; } = string.Empty;

        /// <summary>
        /// 用户昵称
        /// </summary>
        [Column("nick_name")]
        public string NickName { get; set; } = string.Empty;

        /// <summary>
        /// 联系姓名
        /// </summary>
        [Column("contact_name")]
        public string ContactName { get; set; } = string.Empty;

        /// <summary>
        /// 邮箱
        /// </summary>
        [Column("email")]
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// 密码
        /// </summary>
        [Column("password")]
        public string Password { get; set; } = string.Empty;
        /// <summary>
        /// 性别（0未设置 1男 2女）
        /// </summary>
        [Column("gender")]
        public int Gender { get; set; }

        /// <summary>
        /// 生日
        /// </summary>
        [Column("birth_day")]
        public DateTime BirthDay { get; set; } = new DateTime(1900, 1, 1);

        /// <summary>
        /// 手机号
        /// </summary>
        [Column("mobile_number")]
        public string MobileNumber { get; set; } = string.Empty;

        /// <summary>
        /// 头像地址url
        /// </summary>
        [Column("head_url")]
        public string HeadUrl { get; set; } = string.Empty;

        /// <summary>
        /// 个人签名
        /// </summary>
        [Column("personal_sign")]
        public string PersonalSign { get; set; } = string.Empty;

        /// <summary>
        /// 通讯地址
        /// </summary>
        [Column("contact_address")]
        public string ContactAddress { get; set; } = string.Empty;

        /// <summary>
        /// 身份证
        /// </summary>
        [Column("id_number")]
        public string IdNumber { get; set; } = string.Empty;

        /// <summary>
        /// 渠道来源，包括:0-未设置，1- B端，2-C端， 3-BOSS
        /// </summary>
        [Column("channel")]
        public int Channel { get; set; }

        /// <summary>
        /// 渠道来源详细类型：0-未设置，1文章,2海报,3段子,4门店码,5管家码,6商品促销,7自行搜索,8直接转发小程序，9-技师推广，10-商品详情，11-手动添加，12-快速排队码
        /// </summary>
        [Column("referrer_type")]
        public int ReferrerType { get; set; }

        /// <summary>
        /// 推荐人门店Id
        /// </summary>
        [Column("referrer_shop_id")]
        public long ReferrerShopId { get; set; }

        /// <summary>
        /// 推荐人Id
        /// </summary>
        [Column("referrer_user_id")] 
        public string ReferrerUserId { get; set; } = string.Empty;

        /// <summary>
        /// 是否锁定0正常，1锁定
        /// </summary>
        [Column("state")]
        public int State { get; set; }
        /// <summary>
        /// 驾驶证到期日
        /// </summary>
        [Column("driver_license_expiry")]
        public DateTime DriverLicenseExpiry { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 是否可用
        /// </summary>
        [Column("is_deleted")]
        public bool IsDeleted { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        [Column("create_by")]
        public string CreateBy { get; set; } = string.Empty;

        /// <summary>
        /// 创建时间
        /// </summary>
        [Column("create_time")]
        public DateTime CreateTime { get; set; } = new DateTime(1900, 1, 1);

        /// <summary>
        /// 更新人
        /// </summary>
        [Column("update_by")]
        public string UpdateBy { get; set; } = string.Empty;

        /// <summary>
        /// 更新时间
        /// </summary>
        [Column("update_time")]
        public DateTime UpdateTime { get; set; } = new DateTime(1900, 1, 1);


    }
}
