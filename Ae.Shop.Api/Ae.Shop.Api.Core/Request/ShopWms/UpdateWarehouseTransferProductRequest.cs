using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Api.Core.Request.ShopWms
{
    public class UpdateWarehouseTransferProductRequest
    {
        public string TransferId { get; set; }

        public string TransferType { get; set; }
        public string UpdateBy { get; set; }
        public List<UpdateWarehouseTransferProductReceiveRequest> TransferList { get; set; } = new List<UpdateWarehouseTransferProductReceiveRequest>();
    }


    public class UpdateWarehouseTransferProductReceiveRequest
    {
        public long TransferProductId { get; set; }

        public int ReceiveNum { get; set; }

    }
}
