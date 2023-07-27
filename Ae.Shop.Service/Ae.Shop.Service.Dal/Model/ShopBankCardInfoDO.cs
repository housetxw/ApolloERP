using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Service.Dal.Model
{
    public class ShopBankCardInfoDO
    {
        /// <summary>
        /// 门店银行账户id
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 门店id
        /// </summary>
        public long ShopId { get; set; }
        /// <summary>
        /// 账户类型 0个人账号 1企业账号
        /// </summary>
        public sbyte Type { get; set; }
        /// <summary>
        /// 银行id
        /// </summary>
        public long BankId { get; set; }
        /// <summary>
        /// 银行名称
        /// </summary>
        public string BankName { get; set; } = string.Empty;
        /// <summary>
        /// 银行logo
        /// </summary>
        public string IconUrl { get; set; } = string.Empty;
        /// <summary>
        /// 开户银行名称
        /// </summary>
        public string OpeningBank { get; set; } = string.Empty;
        /// <summary>
        /// 开户支行
        /// </summary>
        public string OpeningBranch { get; set; } = string.Empty;
        /// <summary>
        /// 银行行号
        /// </summary>
        public string BankNo { get; set; } = string.Empty;
        /// <summary>
        /// 开户人名称
        /// </summary>
        public string OpeningUserName { get; set; } = string.Empty;
        /// <summary>
        /// 银行卡号
        /// </summary>
        public string BankCardNo { get; set; }

        /// <summary>
        /// 企业名称
        /// </summary>
        public string CompanyName { get; set; } = string.Empty;

        /// <summary>
        /// 开户许可证
        /// </summary>
        public string OpeningLicence { get; set; } = string.Empty;
    }
}
