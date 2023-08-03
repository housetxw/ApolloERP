using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.C.MiniApp.Api.Core.Request.ReceiveCheck
{
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
