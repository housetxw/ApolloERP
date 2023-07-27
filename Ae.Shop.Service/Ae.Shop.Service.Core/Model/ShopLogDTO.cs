using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Service.Core.Model
{
    public class ShopLogDTO
    {
        public string CreateBy { get; set; }

        public string CreateTime { get; set; }

        public string ShopId { get; set; }

        public string OperateType { get; set; }

        public string Summary { get; set; }
    }
}
