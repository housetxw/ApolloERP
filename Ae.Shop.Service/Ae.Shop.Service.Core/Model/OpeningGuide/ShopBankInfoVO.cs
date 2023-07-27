using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Service.Core.Model.OpeningGuide
{
    public class ShopBankInfoVO
    {
        /// <summary>
        /// 开户银行
        /// </summary>
        public string OpeningBank { get; set; } = string.Empty;
        /// <summary>
        /// 开户人姓名
        /// </summary>
        public string OpeningUserName { get; set; } = string.Empty;
        /// <summary>
        /// 银行卡号
        /// </summary>
        public string BankCardNo { get; set; } = string.Empty;
        /// <summary>
        /// 身份证正面
        /// </summary>
        public string IdCardFront { get; set; } = string.Empty;
        /// <summary>
        /// 身份证背面
        /// </summary>
        public string IdCardBack { get; set; } = string.Empty;
    }
}
