using ApolloErp.Data.DapperExtensions.Att;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.User.Service.Dal.Model
{
    [Table("user_bank_card")]
    public class UserBankCardDO
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        [Column("id")]
        public long Id { get; set; }

        /// <summary>
        /// 用户id
        /// </summary>
        [Column("user_id")]
        public Guid UserId { get; set; }

        /// <summary>
        /// 银行id
        /// </summary>
        [Column("bank_id")]
        public string BankId { get; set; } = string.Empty;

        /// <summary>
        /// 银行名称
        /// </summary>
        [Column("bank_name")]
        public string BankName { get; set; } = string.Empty;

        /// <summary>
        /// 银行卡号
        /// </summary>
        [Column("card_number")]
        public string CardNumber { get; set; } = string.Empty;

        /// <summary>
        /// 银行卡名称（民生信用卡(银联卡)）
        /// </summary>
        [Column("card_name")]
        public string CardName { get; set; } = string.Empty;

        /// <summary>
        /// 卡类型（贷记卡）
        /// </summary>
        [Column("card_type")]
        public string CardType { get; set; } = string.Empty;

        /// <summary>
        /// 预留手机号
        /// </summary>
        [Column("phone_no")]
        public string PhoneNo { get; set; } = string.Empty;

        /// <summary>
        /// 删除标记
        /// </summary>
        [Column("is_deleted")]
        public sbyte IsDeleted { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        [Column("create_by")]
        public string CreateBy { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [Column("create_time")]
        public DateTime CreateTime { get; set; }

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
