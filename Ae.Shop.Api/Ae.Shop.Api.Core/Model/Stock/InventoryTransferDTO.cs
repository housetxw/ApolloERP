using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Api.Core.Model
{
   public class InventoryTransferDTO
    {
        public long LocationId { get; set; } = 0;

        public string LocationName { get; set; } = string.Empty;

        public long RelationId { get; set; }

        public long StockInOutId { get; set; }

        public long SupplierId { get; set; } = 0;

        public string SupplierName { get; set; } = string.Empty;

        public long RefBatchNo { get; set; }

        public string CreateBy { get; set; }
    }

}
