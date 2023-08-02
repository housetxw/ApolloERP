using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.B.Order.Api.Client.Model
{
    /// <summary>
    /// 订单操作条件基类
    /// </summary>
    public class BaseOrderOperationConditionDTO
    {
        /// <summary>
        /// 订单号
        /// </summary>
        [Required(ErrorMessage = "订单号不能为空")]
        public string OrderNo { get; set; }
        /// <summary>
        /// 操作修改人
        /// </summary>
        [Required(ErrorMessage = "操作修改人不能为空")]
        public string UpdateBy { get; set; } = string.Empty;
    }
}
