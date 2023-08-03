using System;
using System.Collections.Generic;
using System.Text;
using Ae.Order.Service.Core.Enums;


namespace Ae.Order.Service.Core.Model.ShopOrder
{
    /// <summary>
    /// 订单状态列表DTO 
    /// </summary>
    public class GetOrdersForStatusDTO
    {
        /// <summary>
        /// 主键
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 订单号（RGC前缀）
        /// </summary>
        public string OrderNo { get; set; } = string.Empty;
        /// <summary>
        /// 渠道（0未设置 1C端 2门店）
        /// </summary>
        public sbyte Channel { get; set; }
        /// <summary>
        /// 订单类型（0未设置 1轮胎 2保养 3美容 4 钣金维修 5 汽车改装 6 其他）
        /// </summary>
        public sbyte OrderType { get; set; }
        /// <summary>
        /// 订单状态（10已提交 20已确认 30已完成 40已取消）
        /// </summary>
        public sbyte OrderStatus { get; set; }

        /// <summary>
        /// 订单业务状态
        /// </summary>
        public string BusinessStatus { get; set; } = string.Empty;

        /// <summary>
        /// 库存类型
        /// </summary>
        public StockTypeEnum StockType { get; set; }

        /// <summary>
        /// 下单时间
        /// </summary>
        public DateTime OrderTime { get; set; } = new DateTime(1900, 1, 1);

        /// <summary>
        /// 商品总数
        /// </summary>
        public int TotalProductNum { get; set; }

        /// <summary>
        /// 总价
        /// </summary>
        public decimal TotalAmount => ActualAmount;
        /// <summary>
        /// 实付金额
        /// </summary>
        public decimal ActualAmount { get; set; }
        /// <summary>
        /// 物流单号
        /// </summary>
        public string DeliveryCode { get; set; } = string.Empty;

        /// <summary>
        /// 支付状态（未支付 已支付）
        /// </summary>
        public sbyte PayStatus { get; set; }


        /// <summary>
        /// 配送类型（0未设置 1配送到店 2配送到家）
        /// </summary>
        public sbyte DeliveryType { get; set; }


        /// <summary>
        /// 配送状态（未配送 已配送）
        /// </summary>
        public sbyte DeliveryStatus { get; set; }


        /// <summary>
        /// 派工状态（未派工 已派工）
        /// </summary>
        public sbyte DispatchStatus { get; set; }
 
        /// <summary>
        /// 退款状态（未退款 已退款）
        /// </summary>
        public sbyte RefundStatus { get; set; }
        /// <summary>
        /// 结算状态（未结算 已结算）
        /// </summary>
        public sbyte SettleStatus { get; set; }

        /// <summary>
        /// 签收状态（未签收 已签收）
        /// </summary>
        public sbyte SignStatus { get; set; }

        /// <summary>
        /// 安装服务状态（未安装 已安装）
        /// </summary>
        public sbyte InstallStatus { get; set; }
        /// <summary>
        /// 订单对账状态（0:未设置 1：未对账 2：异常 3：已对账
        /// </summary>
        public sbyte ReconciliationStatus { get; set; }
        /// <summary>
        /// 到账状态（未到账 已到账）
        /// </summary>

        public sbyte MoneyArriveStatus { get; set; }
        /// <summary>
        /// 安装时间
        /// </summary>
        public DateTime InstallTime { get; set; } = new DateTime(1900, 1, 1);

        /// <summary>
        /// 明细信息
        /// </summary>
        public List<GetOrdersProductDTO> Goods { get; set; }=new List<GetOrdersProductDTO>();
    }
}
