using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.Vehicle.Service.Core.Request
{
    /// <summary>
    /// 查询车型配置信息亲求实体
    /// </summary>
    public class VehicleConfigRequest
    {
        /// <summary>
        /// 五级车型Tid
        /// </summary>
        [Required(ErrorMessage = "Tid不能为空")]
        [MinLength(1, ErrorMessage = "Tid不能为空")]
        public List<string> TidList { get; set; }
    }
}
