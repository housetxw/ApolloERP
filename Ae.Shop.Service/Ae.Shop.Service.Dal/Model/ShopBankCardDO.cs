using System;

using ApolloErp.Data.DapperExtensions.Att;
using Ae.Shop.Service.Common.Helper;

namespace Ae.Shop.Service.Dal.Model
{
    [Table("shop_bank_card")]
    public class ShopBankCardDO
    {
        /// <summary>
        /// 门店银行账户id
        /// </summary>
        [Key]
        [Column("id")]
        public long Id { get; set; } 
        /// <summary>
        /// 门店id
        /// </summary>
        [Column("shop_id")]
        [CompareDiffAttribute(Name = "门店id")]
        public long ShopId { get; set; }

        /// <summary>
        /// 账户类型 0个人账号 1企业账号
        /// </summary>
        [Column("type")]
        [CompareDiffAttribute(Name = "账户类型")]
        public sbyte Type { get; set; }

        /// <summary>
        /// 银行id
        /// </summary>
        [Column("bank_id")]
        [CompareDiffAttribute(Name = "银行id")]
        public long BankId { get; set; }
        /// <summary>
        /// 开户银行
        /// </summary>
        [Column("opening_bank")]
        [CompareDiffAttribute(Name = "开户银行")]
        public string OpeningBank { get; set; } = string.Empty;
        /// <summary>
        /// 开户支行
        /// </summary>
        [Column("opening_branch")]
        [CompareDiffAttribute(Name = "开户支行")]
        public string OpeningBranch { get; set; } = string.Empty;
        /// <summary>
        /// 银行行号
        /// </summary>
        [Column("bank_no")]
        [CompareDiffAttribute(Name = "银行行号")]
        public string BankNo { get; set; } = string.Empty;
        /// <summary>
        /// 开户人名称
        /// </summary>
        [Column("opening_user_name")]
        [CompareDiffAttribute(Name = "开户人名称")]
        public string OpeningUserName { get; set; } = string.Empty;
        /// <summary>
        /// 银行卡号
        /// </summary>
        [Column("bank_card_no")]
        [CompareDiffAttribute(Name = "银行卡号")]
        public string BankCardNo { get; set; } = string.Empty;
        /// <summary>
        /// 企业名称
        /// </summary>
        [Column("company_name")]
        [CompareDiffAttribute(Name = "企业名称")]
        public string CompanyName { get; set; } = string.Empty;
        /// <summary>
        /// 开户许可证
        /// </summary>
        [Column("opening_licence")]
        [CompareDiffAttribute(Name = "开户许可证")]
        public string OpeningLicence { get; set; } = string.Empty;
        /// <summary>
        /// 删除标识 0未删除 1已删除
        /// </summary>
        [Column("is_deleted")]
        [CompareDiffAttribute(Name = "删除标识")]
        public sbyte IsDeleted { get; set; } 
        /// <summary>
        /// 创建人
        /// </summary>
        [Column("create_by")]
        [CompareDiffAttribute(Name = "创建人")]
        public string CreateBy { get; set; } = string.Empty;
        /// <summary>
        /// 创建时间
        /// </summary>
        [Column("create_time")]
        [CompareDiffAttribute(Name = "创建时间")]
        public DateTime CreateTime { get; set; } = DateTime.Now;
        /// <summary>
        /// 修改人
        /// </summary>
        [Column("update_by")]
        [CompareDiffAttribute(Name = "修改人")]
        public string UpdateBy { get; set; } = string.Empty;
        /// <summary>
        /// 修改时间
        /// </summary>
        [Column("update_time")]
        [CompareDiffAttribute(Name = "修改时间")]
        public DateTime UpdateTime { get; set; } = new DateTime(1900, 1, 1);
    }
}