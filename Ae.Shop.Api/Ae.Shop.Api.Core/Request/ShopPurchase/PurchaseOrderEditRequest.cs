using Ae.Shop.Api.Core.Model.ShopPurchase;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Api.Core.Request.ShopPurchase
{
    public class PurchaseOrderEditRequest
    {
        /// <summary>
        /// 采购的信息
        /// </summary>
        public PurchaseOrderDTO PurchaseOrder { get; set; }

        /// <summary>
        /// 采购商品信息
        /// </summary>
        public List<PurchaseOrderProductDTO> PurchaseOrderProducts { get; set; } = new List<PurchaseOrderProductDTO>();

        /// <summary>
        /// 是否编辑
        /// </summary>
        public bool IsEdit { get; set; }

    }

    public class PurchaseOrderViewDTO
    {

        /// <summary>
        /// 总采购单号
        /// </summary>
        public long PoId { get; set; }
        public long PurchaseProductId { get; set; }


        /// <summary>
        /// 产品编号
        /// </summary>
        public string ProductCode { get; set; } = string.Empty;
        /// <summary>
        /// 产品名称
        /// </summary>
        public string ProductName { get; set; } = string.Empty;
        /// <summary>
        /// 采购价格不含税
        /// </summary>
        public decimal PurchasePrice { get; set; }

        public int Num { get; set; }
        public string VenderShortName { get; set; } = string.Empty;
        /// <summary>
        /// 状态(1新建 2待发货 3已取消 4已发货 5部分收货 6全部收货 )
        /// </summary>
        public int Status { get; set; }

        public string Remark { get; set; } = string.Empty;

        public DateTime CreateTime { get; set; }

        public string CreateBy { get; set; }

    }


    public class SelectOutPurchaseOrdersRequest
    {
        public long shopId { get; set; }
    }

    public class PurchaseOrderPayDTO
    {
        public decimal Amount;

        public string VenderName;

        public string BankName;
        public string AccountNo;

        public string OpeningBank;

        public string ReceiveBankName;

    }
}
