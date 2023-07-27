using Ae.Shop.Service.Core.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.Shop.Service.Core.Request
{
    /// <summary>
    /// 银行账户信息
    /// </summary>
    public class ModifyShopBankAccountRequest
    {
        /// <summary>
        /// 门店id
        /// </summary>
        [Range(1, int.MaxValue, ErrorMessage = "门店ID必须大于0")]
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
        /// 修改人
        /// </summary>
        public string UpdateBy { get; set; } = string.Empty;
        /// <summary>
        /// 开户银行名称
        /// </summary>
        public string OpeningBank { get; set; } = string.Empty;
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
        public string BankCardNo { get; set; } = string.Empty;
        /// <summary>
        /// 企业名称
        /// </summary>
        public string CompanyName { get; set; } = string.Empty;

        /// <summary>
        /// 开户许可证
        /// </summary>
        public string OpeningLicence { get; set; } = string.Empty;

        /// <summary>
        /// 开户支行
        /// </summary>
        public string OpeningBranch { get; set; } = string.Empty;

        /// <summary>
        /// 来源 0:BOSS 1:APP  2:SHOP
        /// </summary>
        public int Source { get; set; }
    }
}
