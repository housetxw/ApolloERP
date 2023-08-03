using Ae.C.MiniApp.Api.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.MiniApp.Api.Core.Response
{
    public class GetReserveInfoV3Response
    {
        /// <summary>
        /// 可预约订单
        /// </summary>
        public List<CanReserveOrderVO> ReserveOrders { get; set; }
        /// <summary>
        /// 车型信息
        /// </summary>
        public MyCarInfoVO CarInfo { get; set; }
        /// <summary>
        /// 门店Id
        /// </summary>
        public long ShopId { get; set; }
        /// <summary>
        /// 门店名称
        /// </summary>
        public string SimpleName { get; set; }
        /// <summary>
        /// 预约ID
        /// </summary>
        public long ReserveId { get; set; }
        /// <summary>
        /// 开始日期
        /// </summary>
        public int[] SurplusBegin { get; set; } = new int[3];
        /// <summary>
        /// 结束日期
        /// </summary>
        public int[] SurplusEnd { get; set; } = new int[3];
        /// <summary>
        /// 预约日历
        /// </summary>
        public List<ReserveSurplusContentVO> Items { get; set; }
        /// <summary>
        /// 置灰日期
        /// </summary>
        public string[] Disabled { get; set; }
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
        /// 时间节点
        /// </summary>
        public List<ReserveTimeVO> ReserveTimeList { get; set; }
        /// <summary>
        /// 预约说明
        /// </summary>
        public string ReserveExplain { get; set; }
    }
}
