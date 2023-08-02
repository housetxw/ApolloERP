﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Order.Api.Core.Response
{
    public class GetOrderListResponse
    {
        /// <summary>
        /// 订单号（RGC或RGS前缀）
        /// </summary>
        public string OrderNo { get; set; } = string.Empty;
        /// <summary>
        /// 订单状态（10已提交 20已确认 30已完成 40已取消）
        /// </summary>
        public sbyte OrderStatus { get; set; }
        /// <summary>
        /// 渠道（0未设置 1C端 2门店）
        /// </summary>
        public sbyte Channel { get; set; }
        /// <summary>
        /// 订单类型（0未设置 1轮胎 2保养 3美容）
        /// </summary>
        public sbyte OrderType { get; set; }
        /// <summary>
        /// 实付款
        /// </summary>
        public decimal ActualAmount { get; set; }
        /// <summary>
        /// 下单人姓名
        /// </summary>
        public string UserName { get; set; } = string.Empty;
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 安装时间
        /// </summary>
        public DateTime InstallTime { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 配送方式（0未设置 1自配 2快递）
        /// </summary>
        public sbyte DeliveryMethod { get; set; }
        /// <summary>
        /// 配送状态（0未配送 1已配送）
        /// </summary>
        public sbyte DeliveryStatus { get; set; }
        /// <summary>
        /// 库存状态（0未占库 1占库中 2已占库 3释放中 4已释放）
        /// </summary>
        public sbyte StockStatus { get; set; }

        /// <summary>
        /// 支付状态
        /// </summary>
        public sbyte PayStatus { get; set; }

        /// <summary>
        /// 支付方式
        /// </summary>
        public sbyte PayMethod { get; set; }

        /// <summary>
        /// 安装状态
        /// </summary>
        public sbyte InstallStatus { get; set; }

        /// <summary>
        /// 安装类型
        /// </summary>
        public sbyte InstallType { get; set; }

        public sbyte SignStatus { get; set; }

        /// <summary>
        /// 联系人电话
        /// </summary>
        public string ContactPhone { get; set; }


        /// <summary>
        /// 订单分类
        /// </summary>
        public int ProductType { get; set; } = -1;

        public long ShopId { get; set; }

        public string ShopName { get; set; }

        public string CarInfo { get; set; }
    }
}
