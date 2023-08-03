﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.C.MiniApp.Api.Core.Request
{
    /// <summary>
    /// 获取订单列表请求参数
    /// </summary>
    public class GetOrderListRequest : BasePageRequest
    {
        /// <summary>
        /// 订单类型(0未设置 1轮胎 2保养 3美容 4 钣金维修 5 汽车改装 6 其他)
        /// </summary>
        public sbyte OrderType { get; set; }
        /// <summary>
        /// 订单列表状态（0全部 1待付款 2待收货 3待安装 4待评价 5售后/退货）
        /// </summary>
        public sbyte OrderListStatus { get; set; }

        /// <summary>
        /// 产生类型（0普通产生 1购买核销码产生 2使用核销码产生 3再次购买产生 4追加下单产生 5 上门服务 6 会员卡）
        /// </summary>
        public sbyte ProductType { get; set; } = -1;
    }
}
