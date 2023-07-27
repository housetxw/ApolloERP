using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Ae.Shop.Service.Core.Enums;

namespace Ae.Shop.Service.Core.Request.ShopServer
{
    public class JobReqDTO { }

    public class JobListReqByTypeDTO
    {
        [Required(ErrorMessage = "请输入岗位类型信息")]
        [Range(0, (int)JobType.Warehouse, ErrorMessage = "岗位类型必须大于0")]
        public JobType Type { get; set; }
    }

    public class JobListReqByOrgIdDTO
    {
        [Required(ErrorMessage = "请输入OrganizationId")]
        [Range(0, int.MaxValue, ErrorMessage = "OrganizationId必须大于0")]
        public long OrganizationId { get; set; }

        [Required(ErrorMessage = "请输入岗位类型信息")]
        [Range(0, (int)JobType.Supplier, ErrorMessage = "岗位类型必须大于0")]
        public JobType Type { get; set; }
    }


}
