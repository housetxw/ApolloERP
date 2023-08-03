using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Receive.Service.Core.Model.Arrival
{
    public class ArrivalOrderVO
    {
        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderNO { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public int Count { get; set; }
        /// <summary>
        /// 总价
        /// </summary>
        public decimal TotalPrice { get; set; }
        /// <summary>
        /// 标签
        /// </summary>
        public List<string> Tags { get; set; }
        /// <summary>
        /// 派工状态
        /// </summary>
        public string DispatchStatus { get; set; }
        /// <summary>
        /// 物流状态
        /// </summary>
        public string LogisticsStatus { get; set; }
        /// <summary>
        /// 订单状态
        /// </summary>
        public string OrderStatus { get; set; }
        /// <summary>
        /// 渠道
        /// </summary>
        public string Channel { get; set; }
        /// <summary>
        /// 服务名
        /// </summary>
        public List<string> ServiceName { get; set; }
        /// <summary>
        /// 是否相同车型
        /// </summary>
        public bool IsSameCar { get; set; } = false;
        /// <summary>
        /// 是否关联其他到店了
        /// </summary>
        public bool IsRelateOtherArrival { get; set; } = false;
        /// <summary>
        /// 是否关联当前到店记录
        /// </summary>
        public bool IsRelateArrival { get; set; } = false;
        /// <summary>
        /// 关联到店记录ID
        /// </summary>
        public string ArrivalId { get; set; }

        public int PayStatus { get; set; }
        /// <summary>
        /// 车系（A4L)
        /// </summary>
        public string Vehicle { get; set; } = string.Empty;
        /// <summary>
        /// 订单日期
        /// </summary>
        public string ShowOrderTime { get; set; } = string.Empty;
    }
}
