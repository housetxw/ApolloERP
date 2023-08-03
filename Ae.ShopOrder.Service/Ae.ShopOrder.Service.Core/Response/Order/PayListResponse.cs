using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.ShopOrder.Service.Core.Response.Order
{
    public class PayListResponse
    {
        /// <summary>
        /// 总价格
        /// </summary>
        public string SumPrice { get; set; }

        /// <summary>
        /// 待支付价格
        /// </summary>
        public string WaitingPayPrice { get; set; }

        public List<PayListVo> Items { get; set; }

    }

    public class PayListVo
    {
        /// <summary>
        /// 订单No
        /// </summary>
        public string OrderNo { get; set; }

        /// <summary>
        /// 显示的状态
        /// </summary>
        public string ShowOrderStatus { get; set; }

        /// <summary>
        ///支付状态
        /// </summary>
        public bool PayStatus { get; set; }

        /// <summary>
        /// 订单类型
        /// </summary>
        public string OrderType { get; set; }

        /// <summary>
        /// 总价格
        /// </summary>
        public string SumPrice { get; set; }

        public List<PayListProductVo> Products { get; set; }
    }

    public class PayListProductVo
    {
        /// <summary>
        /// 项目名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public int Num { get; set; }

        /// <summary>
        /// 价格
        /// </summary>
        public string Price { get; set; }
    }
}
