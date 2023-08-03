using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.ConsumerOrder.Service.Core.Request.OrderCommand
{
    /// <summary>
    /// 订单更新成本请求对象
    /// </summary>
    public class UpdateOrderProductRequest
    {
        /// <summary>
        /// 订单的OrderNo
        /// </summary>
        [Required]
        public string OrderNo { get; set; }

        /// <summary>
        /// Pid
        /// </summary>
        [Required]
        public string Pid { get; set; }

        /// <summary>
        /// OrderProductId
        /// </summary>
        [Required]
        public long OrderProductId { get; set; }

        /// <summary>
        /// 更新者
        /// </summary>
        [Required]
        public string UpdateBy { get; set; }

        /// <summary>
        /// 成本
        /// </summary>
        [Required]
        [Range(0,999)]
        public decimal Cost { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [Required]
        public string Remark { get; set; }
    }
}
