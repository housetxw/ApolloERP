using System;
using System.Collections.Generic;
using System.Text;
using ApolloErp.Data.DapperExtensions.Att;
using MySqlX.XDevAPI.Relational;

namespace Ae.C.Login.Api.Dal.Model
{
    [Table("user")]
    public class UserDO
    {
        /// <summary>
        /// 用户id
        /// </summary>
        [Key]
        [Column("id")]
        public Guid Id { get; set; }
        [Column("user_name")]
        public string UserName { get; set; } = string.Empty;
        [Column("nick_name")]
        public string NickName { get; set; } = string.Empty;
        [Column("email")]
        public string Email { get; set; } = string.Empty;
        [Column("password")]
        public string PassWord { get; set; } = string.Empty;
        [Column("gender")]
        public int Gender { get; set; }
        [Column("birth_day")]
        public DateTime BirthDay { get; set; } = Convert.ToDateTime("1900-01-01 00:00:00.000");
        [Column("mobile_number")]
        public string MobileNumber { get; set; }
        [Column("head_url")]
        public string HeadUrl { get; set; } = string.Empty;
        [Column("personal_sign")]
        public string PersonalSign { get; set; } = string.Empty;
        [Column("state")]
        public int State { get; set; }

        [Column("channel")]
        public int Channel { get; set; }
        [Column("referrer_type")]
        public int ReferrerType { get; set; }
        [Column("referrer_shop_id")]
        public long ReferrerShopId { get; set; }
        [Column("referrer_user_id")]
        public string ReferrerUserId { get; set; } = string.Empty;

        /// <summary>
        /// 是否删除
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
        public DateTime CreateTime { get; set; } = DateTime.Now;

        /// <summary>
        /// 修改人
        /// </summary>
        [Column("update_by")]
        public string UpdateBy { get; set; } = string.Empty;

        /// <summary>
        /// 修改时间
        /// </summary>
        [Column("update_time")]
        public DateTime UpdateTime { get; set; } = DateTime.Now;

    }
}
