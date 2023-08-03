using System;
using System.Collections.Generic;
using System.Text;
using ApolloErp.Data.DapperExtensions.Att;
using MySqlX.XDevAPI.Relational;

namespace Ae.ConsumerOrder.Service.Dal.Model
{
    [Table("order_user")]
    public class OrderUserDO
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        [Column("id")]
        public long Id { get; set; }
        /// <summary>
        /// 订单Id
        /// </summary>
        [Column("order_id")]
        public long OrderId { get; set; }
        /// <summary>
        /// 是否首单用户
        /// </summary>
        [Column("is_first_order")]
        public sbyte IsFirstOrder { get; set; }
        /// <summary>
        /// 用户Id
        /// </summary>
        [Column("user_id")]
        public string UserId { get; set; } = string.Empty;
        /// <summary>
        /// 用户姓名
        /// </summary>
        [Column("user_name")]
        public string UserName { get; set; } = string.Empty;
        /// <summary>
        /// 昵称
        /// </summary>
        [Column("nick_name")]
        public string NickName { get; set; } = string.Empty;
        /// <summary>
        /// 头像地址
        /// </summary>
        [Column("head_url")]
        public string HeadUrl { get; set; } = string.Empty;
        /// <summary>
        /// 性别（0未设置 1男 2女）
        /// </summary>
        [Column("gender")]
        public byte Gender { get; set; }
        /// <summary>
        /// 生日
        /// </summary>
        [Column("birth_day")]
        public DateTime BirthDay { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 用户手机号[脱敏]
        /// </summary>
        [Column("user_tel_des")]
        public string UserTelDes { get; set; } = string.Empty;
        /// <summary>
        /// 用户手机号
        /// </summary>
        [Column("user_tel")]
        public string UserTel { get; set; } = string.Empty;
        /// <summary>
        /// 会员等级显示
        /// </summary>
        [Column("member_level")]
        public string MemberLevel { get; set; } = string.Empty;
        /// <summary>
        /// 会员积分
        /// </summary>
        [Column("point")]
        public int Point { get; set; }
        /// <summary>
        /// 删除标识
        /// </summary>
        [Column("is_deleted")]
        public sbyte IsDeleted { get; set; }
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
        /// 修改人
        /// </summary>
        [Column("update_by")]
        public string UpdateBy { get; set; } = string.Empty;
        /// <summary>
        /// 修改时间
        /// </summary>
        [Column("update_time")]
        public DateTime UpdateTime { get; set; } = new DateTime(1900, 1, 1);
    }
}
