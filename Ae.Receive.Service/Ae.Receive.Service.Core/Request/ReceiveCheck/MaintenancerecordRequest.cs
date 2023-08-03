using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.Receive.Service.Core.Request.ReceiveCheck
{
    /// <summary>
    /// 维修记录查询
    /// </summary>
    public class MaintenancerecordRequest
    {
        /// <summary>
        /// 车Id
        /// </summary>
        [Required(ErrorMessage = "车辆Id不能为空")]
        public string CarId { get; set; }

        /// <summary>
        /// 服务类型
        /// </summary>
        public string ServiceCode { get; set; }
    }
}
