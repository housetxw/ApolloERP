using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.C.MiniApp.Api.Core.Request
{
    public class GetEmployeeInfoRequest : BaseGetRequest
    {
        /// <summary>
        /// 门店员工ID
        /// </summary>
        [Required(ErrorMessage = "员工ID不能为空")]
        public string EmployeeId { get; set; }
    }
}
