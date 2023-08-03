using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.ShopOrder.Service.Core.Response.Order
{
    public class GetOrderOutProductsProfitResponse
    {
        /// <summary>
        /// 门店编号
        /// </summary>
        public long ShopId { get; set; }

        /// <summary>
        /// 门店名称
        /// </summary>
        public string SimpleName { get; set; }

        /// <summary>
        /// 产品编号
        /// </summary>
        public string ProductId { get; set; }

        /// <summary>
        /// 产品名称
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        /// 原厂编号
        /// </summary>
        public string OeNumber { get; set; }

        /// <summary>
        /// 产品类目
        /// </summary>
        public string CategoryCode { get; set; }

        /// <summary>
        /// 供应商简称
        /// </summary>
        public string VenderShortName { get; set; }

        /// <summary>
        /// 采购单号
        /// </summary>
        public string PurchaseOrder { get; set; }

        /// <summary>
        /// 销售单号
        /// </summary>
        public string SaleOrder { get; set; }

        /// <summary>
        /// 客户名称
        /// </summary>
        public string UserName { get; set; }
        public string UserPhone { get; set; }

        /// <summary>
        /// 车牌号
        /// </summary>
        public string CarNumber { get; set; }

        /// <summary>
        /// 车型信息
        /// </summary>
        public string CarInfo { get; set; }

        /// <summary>
        /// 采购单价
        /// </summary>
        public decimal PurchasePrice { get; set; }

        /// <summary>
        /// 销售单价
        /// </summary>
        public decimal SalePrice { get; set; }

        /// <summary>
        /// 订单数量
        /// </summary>
        public int TotalNumber { get; set; }

        /// <summary>
        /// 采购数量
        /// </summary>
        public int PurchaseNumber { get; set; }

        /// <summary>
        /// 采购金额
        /// </summary>
        public decimal PurchaseTotalPrice { get; set; }

        /// <summary>
        /// 销售总价
        /// </summary>
        public decimal SaleOrderPrice { get; set; }

        /// <summary>
        /// 订单实收金额
        /// </summary>
        public decimal ActualAmount { get; set; }

        /// <summary>
        /// 订单下单日期
        /// </summary>
        public string CreateTime { get; set; }

        /// <summary>
        /// 采购日期
        /// </summary>
        public string PurchaseTime { get; set; }


        /// <summary>
        /// 库存数量
        /// </summary>
        public int StockNum { get; set; }

    }
}
