using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.Receive.Service.Core.Request.ReceiveCheck
{
    /// <summary>
    /// 车辆检查报告
    /// </summary>
    public class CarReceiveCheckListRequest
    {
        /// <summary>
        /// 车Id
        /// </summary>
        [Required(ErrorMessage = "车Id不能为空")]
        public string CarId { get; set; }

        /// <summary>
        /// 个数限制
        /// </summary>
        public int Limit { get; set; }
    }
}
