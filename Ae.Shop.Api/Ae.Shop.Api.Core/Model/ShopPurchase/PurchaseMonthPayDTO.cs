using Ae.Shop.Api.Core.Request;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.Shop.Api.Core.Model
{

    public class PurchaseMonthPayRequest : BasePageRequest
    {
        public string Status { get; set; }
    }

    public class PurchaseMonthPayDTO
    {
        /// <summary>
        /// 主键
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 门店id
        /// </summary>
        public long ShopId { get; set; }



        public long VenderId { get; set; }


        public string VenderName { get; set; }


        public string BankName { get; set; }

        public string AccountNo { get; set; }

        public decimal Amount { get; set; }

        public string PayMethod { get; set; }

        public string Status { get; set; }

        public DateTime AuditTime { get; set; } = new DateTime(1900, 1, 1);

        public string AuditUser { get; set; }

        public string SerialNumber { get; set; }

        public string Payer { get; set; }

        public DateTime PayTime { get; set; } = new DateTime(1900, 1, 1);

        public DateTime CancleTime { get; set; } = new DateTime(1900, 1, 1);

        public string CancleUser { get; set; }



        /// <summary>
        /// 备注说明
        /// </summary>
        public string Remark { get; set; } = string.Empty;
        /// <summary>
        /// 删除标识 0否 1是
        /// </summary>
        public int IsDeleted { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public string CreateBy { get; set; } = string.Empty;
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 修改人
        /// </summary>
        public string UpdateBy { get; set; } = string.Empty;
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime UpdateTime { get; set; } = new DateTime(1900, 1, 1);

        /// <summary>
        /// 1.审核  2.取消
        /// </summary>
        public int Type;

        public List<string> PurchaseIds { get; set; } = new List<string>();

        public string OpeningBank { get; set; } = string.Empty;

        public string ReceiveBankName { get; set; } = string.Empty;

    }

    public class GetTargetVenderPurchaseOrderResponse { 
        public string Bank { get; set; }

        public string Account { get; set; }
        public string OpeningBank { get; set; }

        public string ReceiveBankName { get; set; }


        public List<AddPurchasePayInfo> purchaseOrders { get; set; } = new List<AddPurchasePayInfo>();
    }


    public class AddPurchasePayInfo
    {
        public long PurchaseId { get; set; }

        public decimal Amount { get; set; }

        public string shopName { get; set; }

        public string createTime { get; set; }

        public string deliveryCode { get; set; }

        public string hcType { get; set; }

        public string relationPurchaseId { get; set; }

        public string joinText { get; set; }

        public List<PurchaseOrderProductInfo> products { get; set; } = new List<PurchaseOrderProductInfo>();

    }

    public class TempPurchaseOrderInfo : PurchaseOrderProductInfo
    {
        public string shopName { get; set; }

        public long shopId { get; set; }

        public string createTime { get; set; }

        public string deliveryCode { get; set; }


        public string hcType { get; set; }

        public string relationPurchaseId { get; set; }

        public long PurchaseId { get; set; }
    }


    public class PurchaseOrderProductInfo
    {
        public string ProductName { get; set; }

        public string ProductId { get; set; }

        public int Num { get; set; }

        public decimal Price { get; set; }
    }


    public class GetVenderPurchaseOrdersRequest {

        [Range(1, long.MaxValue, ErrorMessage = "供应商ID必须大于0")]
        public long VenderId { get; set; }

        /// <summary>
        /// 门店id
        /// </summary>
        public long ShopId { get; set; } = 0;

        public List<string> Times { get; set; } = new List<string>();
    }


}
