using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.FMS.Service.Core.Response
{
    public class GetOrderOccupyShopProductPurchaseResponse
    {
        public string BatchId { get; set; }

        public string OrderNo { get; set; }

        public string ProductCode { get; set; }

        public string ProductName { get; set; }

        public int Num { get; set; }

        public string OleNumber { get; set; }

        public string CategoryName { get; set; }

        public decimal PurchasePrice { get; set; }

        public decimal SalePrice { get; set; }

        public string PurchaseOrder { get; set; }

        public string VenderShortName { get; set; }


    }
}
