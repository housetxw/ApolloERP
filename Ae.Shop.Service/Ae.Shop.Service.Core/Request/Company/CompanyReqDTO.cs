using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Ae.Shop.Service.Core.Enums;

namespace Ae.Shop.Service.Core.Request.Company
{
    public class CompanyReqDTO { }

    public class CompanyListReqByType
    {
        [Required(ErrorMessage = "请输入类型")]
        [Range(0, int.MaxValue, ErrorMessage = "类型必须大于0")]
        public EmployeeType Type { get; set; }
    }

}
