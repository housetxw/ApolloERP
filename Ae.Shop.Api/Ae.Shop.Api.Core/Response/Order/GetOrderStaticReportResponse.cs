using Ae.Shop.Api.Core.Request;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Api.Core.Response.Order
{
    public class GetOrderStaticReportResponse
    {
        /// <summary>
        /// 时间周期
        /// </summary>
        public string CycleTime { get; set; }
        /// <summary>
        /// 订单数量
        /// </summary>
        public int HaveFinishOrderNum { get; set; }

        /// <summary>
        /// 订单金额
        /// </summary>
        public decimal HaveFinishOrderAmount { get; set; }

        /// <summary>
        /// 订单数量
        /// </summary>
        public int NotFinishOrderNum { get; set; }

        /// <summary>
        /// 订单金额
        /// </summary>
        public decimal NotFinishOrderAmount { get; set; }

    }

    public class GetOrderDetailStaticReportResponse
    {
        public long OrderId { get; set; }
        public string OrderTime { get; set; }

        public string OrderNo { get; set; }

        public string OrderStatus { get; set; }

        public string ContactName { get; set; }

        public string ContactPhone { get; set; }

        public long ShopId { get; set; }

        public string CarNumber { get; set; }

        public string DispatchTime { get; set; }

        public string ShopName { get; set; }

        public string TechName { get; set; }

        public string CheckName { get; set; }

        public string OrderAmount { get; set; }

        public string ActualAmount { get; set; }

        public string DiscountContent { get; set; }

        public string Remark { get; set; }
    }

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
        /// 销售定价
        /// </summary>
        public decimal SalePrice { get; set; }

        /// <summary>
        /// 销售订单价格
        /// </summary>
        public decimal SaleOrderPrice { get; set; }

        public int TotalNumber { get; set; }

        /// <summary>
        /// 采购金额
        /// </summary>
        public decimal PurchaseTotalPrice { get; set; }

        /// <summary>
        /// 实收金额
        /// </summary>
        public decimal ActualAmount { get; set; }

        /// <summary>
        /// 订单下单日期
        /// </summary>
        public string CreateTime { get; set; }

    }

    public class OrderProductNewDTO
    {
        /// <summary>
        /// 物理主键（不用于对外逻辑和关联）
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 平台订单Id
        /// </summary>
        public long OrderId { get; set; }
        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderNo { get; set; } = string.Empty;
        /// <summary>
        /// 子系统订单商品Id（同步关联）
        /// </summary>
        public long OrderProductId { get; set; }
        /// <summary>
        /// 产品Id
        /// </summary>
        public string ProductId { get; set; } = string.Empty;
        /// <summary>
        /// 产品名称
        /// </summary>
        public string ProductName { get; set; } = string.Empty;
        /// <summary>
        /// 产品显示名称
        /// </summary>
        public string DisplayName { get; set; } = string.Empty;
        /// <summary>
        /// 产品描述
        /// </summary>
        public string Description { get; set; } = string.Empty;
        /// <summary>
        /// 产品性质（0 实物产品、1 套餐产品、2 服务产品、3 数字产品 4：项目 5:门店外采）
        /// </summary>
        public sbyte ProductAttribute { get; set; }
        /// <summary>
        /// 所属父级订单套餐产品Id
        /// </summary>
        public long ParentOrderPackagePid { get; set; }
        /// <summary>
        /// 服务属于订单实物产品Id（多个pid以;分割)
        /// </summary>
        public string ServeForOrderPids { get; set; } = string.Empty;
        /// <summary>
        /// 商品类目编号
        /// </summary>
        public string CategoryCode { get; set; } = string.Empty;
        /// <summary>
        /// 适配项目编号（暂不存储）
        /// </summary>
        public string ItemCode { get; set; } = string.Empty;
        /// <summary>
        /// 标签
        /// </summary>
        public string Label { get; set; } = string.Empty;
        /// <summary>
        /// 图片路径
        /// </summary>
        public string ImageUrl { get; set; } = string.Empty;
        /// <summary>
        /// 价格Id
        /// </summary>
        public long PriceId { get; set; }
        /// <summary>
        /// 市场单价
        /// </summary>
        public decimal MarketingPrice { get; set; }
        /// <summary>
        /// 销售单价
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// 总成本价（实物取自库存，服务取自门店）（指乘以购买数量后）
        /// </summary>
        public decimal TotalCostPrice { get; set; }
        /// <summary>
        /// 单位（个/升/斤等）
        /// </summary>
        public string Unit { get; set; } = string.Empty;
        /// <summary>
        /// 数量（指单个套餐中该商品）
        /// </summary>
        public int Number { get; set; }
        /// <summary>
        /// 总数量（指乘以购买数量后）
        /// </summary>
        public int TotalNumber { get; set; }
        /// <summary>
        /// 商品库存状态（0未占库 1占库中 2已占库 3释放中 4已释放）
        /// </summary>
        public sbyte StockStatus { get; set; }
        /// <summary>
        /// 金额（指单个套餐中该商品）
        /// </summary>
        public decimal Amount { get; set; }
        /// <summary>
        /// 总金额（指乘以购买数量后）
        /// </summary>
        public decimal TotalAmount { get; set; }
        /// <summary>
        /// 税率
        /// </summary>
        public decimal TaxRate { get; set; }
        /// <summary>
        /// 分摊优惠金额（指乘以购买数量后）
        /// </summary>
        public decimal ShareDiscountAmount { get; set; }
        /// <summary>
        /// 实际支付金额（指乘以购买数量后）
        /// </summary>
        public decimal ActualPayAmount { get; set; }
        /// <summary>
        /// 删除标识（0否 1是）
        /// </summary>
        public sbyte IsDeleted { get; set; }
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

        public long ShopId { get; set; }

        public string SimpleName { get; set; }
    }

    public class GetOrderDetailStaticReportApiRequest : BasePageRequest
    {
        /// <summary>
        /// 时间周期（Week,Month,Year)
        /// </summary>
        public string Type { get; set; }

    }


    public class GetOrderOutProductsProfitRequest : BasePageRequest
    {
        public string OrderNo { get; set; }

        public List<long> ShopIds { get; set; } = new List<long>();


        public string StartCreateTime { get; set; }

        public string EndCreateTime { get; set; }
    }

    public class GetOrderOutProductsProfitRequestForExcel
    {
        public string OrderNo { get; set; }

        //public List<long> ShopIds { get; set; } = new List<long>();


        public string StartCreateTime { get; set; }

        public string EndCreateTime { get; set; }
    }

    public class GetOrderProductsRequest : BasePageRequest
    {
        public string OrderNo { get; set; }

        public string StartCreateTime { get; set; }

        public string EndCreateTime { get; set; }

        public string ProductCode { get; set; }

        public string ProductName { get; set; }

        public List<long> ShopIds { get; set; } = new List<long>();

    }

    public class GetOrderCompleteStaticReportRequest
    {
        /// <summary>
        /// 开始日期
        /// </summary>
        public string StartTime { get; set; }

        /// <summary>
        /// 结束日期
        /// </summary>
        public string EndTime { get; set; }

        public List<long> ShopIds { get; set; } = new List<long>();

    }

    public class DateTimeHelper
    {
        /// <summary>
        /// 获取本周的周一日期
        /// </summary>
        /// <returns></returns>
        public static DateTime GetThisWeekMonday()
        {
            DateTime date = DateTime.Now;
            DateTime firstDate = System.DateTime.Now;
            switch (date.DayOfWeek)
            {
                case System.DayOfWeek.Monday:
                    firstDate = date;
                    break;
                case System.DayOfWeek.Tuesday:
                    firstDate = date.AddDays(-1);
                    break;
                case System.DayOfWeek.Wednesday:
                    firstDate = date.AddDays(-2);
                    break;
                case System.DayOfWeek.Thursday:
                    firstDate = date.AddDays(-3);
                    break;
                case System.DayOfWeek.Friday:
                    firstDate = date.AddDays(-4);
                    break;
                case System.DayOfWeek.Saturday:
                    firstDate = date.AddDays(-5);
                    break;
                case System.DayOfWeek.Sunday:
                    firstDate = date.AddDays(-6);
                    break;
            }

            return firstDate;
        }
    }

    public class GetOrderNotCompleteStaticReportRequest
    {
        /// <summary>
        /// 开始日期
        /// </summary>
        public string StartTime { get; set; }

        /// <summary>
        /// 结束日期
        /// </summary>
        public string EndTime { get; set; }

        public List<long> ShopIds { get; set; } = new List<long>();

    }

    public class GetOrderDetailStaticReportRequest : BasePageRequest
    {
        /// <summary>
        /// 开始日期
        /// </summary>
        public string StartTime { get; set; }

        /// <summary>
        /// 结束日期
        /// </summary>
        public string EndTime { get; set; }

        public List<long> ShopIds { get; set; } = new List<long>();

    }
    public class GetOrderCompleteStaticReportResponse
    {
        /// <summary>
        /// 订单数量
        /// </summary>
        public int OrderNum { get; set; }

        /// <summary>
        /// 订单金额
        /// </summary>
        public decimal OrderAmount { get; set; }
    }

    public class GetOrderNotCompleteStaticReportResponse
    {
        /// <summary>
        /// 订单数量
        /// </summary>
        public int OrderNum { get; set; }

        /// <summary>
        /// 订单金额
        /// </summary>
        public decimal OrderAmount { get; set; }
    }



}
