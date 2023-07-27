using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.Shop.Service.Core.Request
{
    public class EmployeePerformanceDetialRequest
    {
        /// <summary>
        ///  开始日期
        /// </summary>
        [Required(ErrorMessage = "开始日期不能为空")]
        public DateTime StartDate { get; set; }

        /// <summary>
        ///  结束日期
        /// </summary>
        [Required(ErrorMessage = "结束日期不能为空")]
        public DateTime EndDate { get; set; }

        /// <summary>
        /// 员工Id
        /// </summary>
        [Required(ErrorMessage = "员工Id不能为空")]
        public string EmployeeId { get; set; }
    }
}
