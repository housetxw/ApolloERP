using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.B.Product.Api.Core.Request.BaoYang
{
    /// <summary>
    /// AddInstallAddFeeConfigRequest
    /// </summary>
    public class AddInstallAddFeeConfigRequest
    {
        /// <summary>
        /// 服务pid
        /// </summary>
        [Required(ErrorMessage = "服务pid不能为空")]
        public string ServiceId { get; set; }

        /// <summary>
        /// 车辆最小指导价
        /// </summary>
        public decimal CarMinPrice { get; set; }

        /// <summary>
        /// 车辆最大指导价
        /// </summary>
        public decimal CarMaxPrice { get; set; }

        /// <summary>
        /// 加价金额
        /// </summary>
        public decimal AdditionalPrice { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; } = string.Empty;
    }
}
