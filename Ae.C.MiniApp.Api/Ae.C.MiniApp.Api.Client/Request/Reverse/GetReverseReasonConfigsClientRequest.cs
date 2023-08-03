using Ae.C.MiniApp.Api.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.C.MiniApp.Api.Client.Request
{
    public class GetReverseReasonConfigsClientRequest
    {
        /// <summary>
        /// 申请类型
        /// </summary>
        [Required(ErrorMessage = "必须指定申请类型")]
        public ReverseApplyTypeEnum ApplyType { get; set; }
    }
}
