using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Ae.Shop.Service.Core.Enums;

namespace Ae.Shop.Service.Core.Request
{
   public class DepartmentReqDTO { }

   public class DepartmentListReqByOrgIdTypeDTO
   {
       [Required(ErrorMessage = "请输入OrganizationId")]
       [Range(0, int.MaxValue, ErrorMessage = "OrganizationId必须大于0")]
       public long OrganizationId { get; set; }

       [Required(ErrorMessage = "请输入部门类型信息")]
       [Range(0, (int)OrganizationType.Supplier, ErrorMessage = "部门类型必须大于0")]
       public OrganizationType Type { get; set; }
    }


}
