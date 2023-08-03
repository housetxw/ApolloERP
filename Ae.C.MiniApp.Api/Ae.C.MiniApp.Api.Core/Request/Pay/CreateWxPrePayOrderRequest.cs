using Ae.C.MiniApp.Api.Core.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.C.MiniApp.Api.Core.Request
{
    public class CreateWxPrePayOrderRequest : BaseOrderOperationCondition
    {
        /// <summary>
        /// 微信用户标识
        /// </summary>
        [Required(ErrorMessage = "微信用户标识不能为空")]
        public string OpenId { get; set; }
    }
}
