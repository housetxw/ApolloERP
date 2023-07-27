using System;
using System.Collections.Generic;
using System.Text;
using Ae.Shop.Service.Core.Model;

namespace Ae.Shop.Service.Core.Response.ShopServer
{
    public class WorkKindResDTO { }

    public class WorkKindListResDTO : WorkKindListDTO { }

    public class WorkKindInfoResDTO
    {
        public List<EmployeeLevelListResDTO> WorkKindLevel { get; set; } = new List<EmployeeLevelListResDTO>();
        public List<WorkKindListResDTO> WorkKindList { get; set; } = new List<WorkKindListResDTO>();
    }

}
