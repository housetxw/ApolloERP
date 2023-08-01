using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.User.Service.Core.Request
{
    /// <summary>
    /// AddUserBankCardRequest
    /// </summary>
    public class AddUserBankCardRequest
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        [Required(ErrorMessage = "UserId不能为空")]
        public string UserId { get; set; }

        /// <summary>
        /// 银行名称
        /// </summary>
        public string BankName { get; set; }

        /// <summary>
        /// 银行卡号
        /// </summary>
        [Required(ErrorMessage = "CardNumber不能为空")]
        public string CardNumber { get; set; }

        /// <summary>
        /// 预留手机号
        /// </summary>
        public string PhoneNo { get; set; }
    }
}
