using Ae.Receive.Service.Core.Model;
using Ae.Receive.Service.Core.Model.Reserve;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Receive.Service.Core.Response
{
    public class GetReserveInfoForOrderResponse
    {
        /// <summary>
        /// 可预约订单
        /// </summary>
        public CanReserveOrderDTO ReserveOrders { get; set; }
        /// <summary>
        /// 车型信息
        /// </summary>
        public MyCarInfoDTO CarInfo { get; set; }
        /// <summary>
        /// 门店Id
        /// </summary>
        public long ShopId { get; set; }
        /// <summary>
        /// 门店名称
        /// </summary>
        public string SimpleName { get; set; }
        /// <summary>
        /// 开始日期
        /// </summary>
        public Array SurplusBegin { get; set; }
        /// <summary>
        /// 结束日期
        /// </summary>
        public Array SurplusEnd { get; set; }
        /// <summary>
        /// 预约日历
        /// </summary>
        public List<ReserveSurplusContentDTO> Items { get; set; }
        /// <summary>
        /// 置灰日期
        /// </summary>
        public Array Disabled { get; set; }
        /// <summary>
        /// 年份
        /// </summary>
        public int Year { get; set; }
        /// <summary>
        /// 月
        /// </summary>
        public int Month { get; set; }
        /// <summary>
        /// 日
        /// </summary>
        public int Day { get; set; }
        /// <summary>
        /// 预约时间
        /// </summary>
        public string ReserveTime { get; set; }
        /// <summary>
        /// 时间节点
        /// </summary>
        public List<ReserveTimeDTO> ReserveTimeList { get; set; }
        /// <summary>
        /// 预约说明
        /// </summary>
        public string ReserveExplain { get; set; }
    }
}
