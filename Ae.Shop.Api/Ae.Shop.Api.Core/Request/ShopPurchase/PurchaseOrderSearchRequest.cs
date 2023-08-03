using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Api.Core.Request.ShopPurchase
{
    public class PurchaseOrderSearchRequest: BasePageRequest
    {
        /// <summary>
        /// 采购类型 类型 1 公司采购 2 门店采购，3内销单，4外销单
        /// </summary>
        public int PurchaseType { get; set; }

        /// <summary>
        /// 内销公司Id
        /// </summary>
        public long SaleCompanyId { get; set; }
        /// <summary>
        /// 供应商Id
        /// </summary>
        public int VenderId { get; set; }
        /// <summary>
        /// 仓库Id
        /// </summary>
        public long WarehouseId { get; set; } = 0;

        /// <summary>
        /// 采购单号
        /// </summary>
        public int PurchaseOrderId { get; set; }

        /// <summary>
        ///  搜索条件
        /// </summary>
        public string Key { get; set; }
        /// <summary>
        ///  采购开始时间
        /// </summary>
        public DateTime? PurchaseOrderStartTime { get; set; }

        /// <summary>
        ///  采购结束时间
        /// </summary>
        public DateTime? PurchaseOrderEenTime { get; set; }

        /// <summary>
        /// 支付状态(1未付款 2部分付款 3已付款)
        /// </summary>
        public int PayStatus { get; set; }

        /// <summary>
        /// 状态(1新建 2待发货 3已取消 4发货中 5部分收货 6全部收货 )
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 结算方式~
        /// </summary>
        public int PayType { get; set; }

        public long ShopId { get; set; }

    }
}
