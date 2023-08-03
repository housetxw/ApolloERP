using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Api.Core.Model.ShopPurchase
{
    public class PurchaseOrderDTO
    {
        /// <summary>
        /// 主键
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 采购类型  1 公司采购 2 门店采购，3内销单，4外销单
        /// </summary>
        public int PurchaseType { get; set; }
        /// <summary>
        /// 供应商编号
        /// </summary>
        public long VenderId { get; set; }
        /// <summary>
        /// 供应商简称
        /// </summary>
        public string VenderShortName { get; set; } = string.Empty;
        /// <summary>
        /// 供客仓库Id
        /// </summary>
        public long VenderWarehouseId { get; set; }
        /// <summary>
        /// 供客仓库简称
        /// </summary>
        public string VenderWarehouseName { get; set; } = string.Empty;
        /// <summary>
        /// 仓库编号
        /// </summary>
        public long WarehouseId { get; set; }
        /// <summary>
        /// 仓库简称
        /// </summary>
        public string WarehouseName { get; set; } = string.Empty;
        /// <summary>
        /// 状态(1新建 2待发货 3已取消 4已发货 5部分收货 6全部收货 )
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// 发票类型(0 无需发票 1 普通发票 2 增值税发票)
        /// </summary>
        public int PinvoiceType { get; set; }
        /// <summary>
        /// 结算方式(1 现金 2 账期 3 月结)
        /// </summary>
        public int PayType { get; set; }
        /// <summary>
        /// 支付状态(1未付款 2部分付款 3已付款)
        /// </summary>
        public int PayStatus { get; set; }
        /// <summary>
        /// 总金额
        /// </summary>
        public decimal TotalAmount { get; set; }
        /// <summary>
        /// 优惠金额
        /// </summary>
        public decimal CouponAmount { get; set; }
        /// <summary>
        /// 实付款
        /// </summary>
        public decimal ActualAmount { get; set; }
        /// <summary>
        /// 采购员
        /// </summary>
        public string Buyer { get; set; } = string.Empty;
        /// <summary>
        /// 运单号
        /// </summary>
        public string WaybillNumber { get; set; } = string.Empty;
        /// <summary>
        /// 自负运费
        /// </summary>
        public decimal OwnFreight { get; set; }
        /// <summary>
        /// 备注说明
        /// </summary>
        public string Remark { get; set; } = string.Empty;
        /// <summary>
        /// 门店id
        /// </summary>
        public long ShopId { get; set; }
        public string ShopName { get; set; } = string.Empty;
        /// <summary>
        /// 删除标识 0否 1是
        /// </summary>
        public int IsDeleted { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        public string CreateBy { get; set; }


        public string PayMethod { get; set; } = string.Empty;

        public string SerialNumber { get; set; } = string.Empty;

        public string Payer { get; set; } = string.Empty;

        public DateTime PayTime { get; set; } = new DateTime(1900, 1, 1);

        /// <summary>
        /// 关联红冲采购单
        /// </summary>
        public long RelationPurchaseId { get; set; }

        /// <summary>
        /// 关联红冲采购单
        /// </summary>
        public string HcType { get; set; } = string.Empty;

        public string joinText;
    }

    public class PurchaseOrderBatchSendDTO
    {
        public List<long> PurchaseOrderIds { get; set; } = new List<long>();

        public int PayType { get; set; }

        public string UpateBy { get; set; }

        public string DeliveryCode { get; set; } = string.Empty;

    }

}
