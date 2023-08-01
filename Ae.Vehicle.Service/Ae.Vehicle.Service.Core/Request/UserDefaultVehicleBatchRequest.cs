using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.Vehicle.Service.Core.Request
{
    /// <summary>
    /// 批量获取用户默认车辆
    /// </summary>
    public class UserDefaultVehicleBatchRequest
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        [MaxLength(50, ErrorMessage = "批量查询最大支持50")]
        public List<string> UserIdList { get; set; }

    }
}
