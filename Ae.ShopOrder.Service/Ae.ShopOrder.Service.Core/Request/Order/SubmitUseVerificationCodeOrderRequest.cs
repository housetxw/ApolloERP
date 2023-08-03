using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.ShopOrder.Service.Core.Request.Order
{
    public class SubmitUseVerificationCodeOrderRequest
    {
        /// <summary>
        /// 核销码
        /// </summary>
        [Required(ErrorMessage = "核销码不可为空")]
        public string Code { get; set; }
        /// <summary>
        /// 核销门店ID
        /// </summary>
        [Required(ErrorMessage = "核销门店ID不可为空")]
        public long ShopId { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 终端类型（0未设置 1小程序 2Android-App 3iOS-App 4S网站 5官网）
        /// </summary>
        public sbyte TerminalType { get; set; }

        public string CreateBy { get; set; }
    }
}
