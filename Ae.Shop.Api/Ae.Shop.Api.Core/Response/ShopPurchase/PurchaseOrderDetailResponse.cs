using Ae.Shop.Api.Core.Model.ShopPurchase;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Api.Core.Response.ShopPurchase
{
    public class PurchaseOrderDetailResponse
    {
        /// <summary>
        /// 采购的信息
        /// </summary>
        public PurchaseOrderDTO PurchaseOrder { get; set; }

        /// <summary>
        /// 采购商品信息
        /// </summary>
        public List<PurchaseOrderProductDTO> PurchaseOrderProducts { get; set; }

        public List<PurchaseOrderLogDTO> Logs { get; set; } = new List<PurchaseOrderLogDTO>();
    }
}
