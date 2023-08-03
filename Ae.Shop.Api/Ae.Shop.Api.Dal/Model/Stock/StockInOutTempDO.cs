using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Api.Dal.Model
{
    public class StockInOutTempDO
    {
        public int StockId { get; set; }

        public string SourceInventoryNo { get; set; }

        public string LocationName { get; set; }


        public string OperationType { get; set; }

        public string SourceType { get; set; }

        public string CreateBy { get; set; }

        public DateTime CreateTime { get; set; }

        public string Type { get; set; }

        public string Status { get; set; }
        public string Remark { get; set; }
    }
}
