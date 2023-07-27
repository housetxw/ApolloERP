using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Ae.Shop.Service.Common.Constant;

namespace Ae.Shop.Service.Core.Request
{
    public class EmployeeInfoReqDTO
    {
        [Required(ErrorMessage = "请输入员工Id"),
         RegularExpression(CommonConstant.PatternGuid, ErrorMessage = "员工Id格式不正确")]
        public string EmployeeId { get; set; }
    }

    public class EmployeeDeleteReqByIdDTO : EmployeeInfoReqDTO
    {
        [Required(ErrorMessage = "请输入操作人员工姓名")]
        public string UpdateBy { get; set; }
    }

    public class EmployeeBatchDeleteReqByIdDTO
    {
        [MinLength(1, ErrorMessage = "至少输入一个需要删除的员工Id")]
        public List<string> EmployeeIds { get; set; }

        public string UpdateBy { get; set; }
    }

    public class TechnicanPageReqDTO : BaseOnlyPageRequest
    {
        [Required(ErrorMessage = "请输入门店Id"), Range(1, long.MaxValue, ErrorMessage = "门店Id必须大于0")]
        public long ShopId { get; set; }

        [Required(ErrorMessage = "请输入员工Id"), RegularExpression(CommonConstant.PatternGuid, ErrorMessage = "员工Id格式不正确")]
        public string EmployeeId { get; set; }

        public List<int> JobId { get; set; } = new List<int>() { 3 };

    }

}
