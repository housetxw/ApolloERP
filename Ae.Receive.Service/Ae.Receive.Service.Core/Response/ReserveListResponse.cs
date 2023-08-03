using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Receive.Service.Core.Response
{
    public class ReserveListResponse
    {
        /// <summary>
        /// 日期信息
        /// </summary>
        public DayInfo DayInfo { get; set; }

        /// <summary>
        /// 总预约数量
        /// </summary>
        public int TotalReserve { get; set; }

        /// <summary>
        /// 预约记录时间分组
        /// </summary>
        public List<ReserveTimeVo> ReserveTimeList { get; set; } = new List<ReserveTimeVo>();
    }

    /// <summary>
    /// 日期信息
    /// </summary>
    public class DayInfo
    {
        /// <summary>
        /// 日期 1-28
        /// </summary>
        public string MonthTitle { get; set; }

        /// <summary>
        /// 星期
        /// </summary>
        public string WeekTitle { get; set; }

        /// <summary>
        /// 今天
        /// </summary>
        public string TodayString { get; set; }
    }

    /// <summary>
    /// 预约记录时间分组
    /// </summary>
    public class ReserveTimeVo
    {
        /// <summary>
        /// 时间点 08:00
        /// </summary>
        public string Hour { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// 预约列表
        /// </summary>
        public List<ReserveListVo> ReserveList { get; set; }
    }

    /// <summary>
    /// 预约数据
    /// </summary>
    public class ReserveListVo
    {
        /// <summary>
        /// 预约Id
        /// </summary>
        public long ReserveId { get; set; }

        /// <summary>
        /// 客户姓名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 用户手机号
        /// </summary>
        public string UserTel { get; set; }

        /// <summary>
        /// 预约渠道
        /// </summary>
        public string ReserveChannel { get; set; }

        /// <summary>
        /// 预约类型
        /// </summary>
        public string ReserveTypeName { get; set; }

        /// <summary>
        /// 预约技师
        /// </summary>
        public string Technician { get; set; }

        /// <summary>
        /// 车牌
        /// </summary>
        public string CarPlate { get; set; }

        /// <summary>
        /// 车型展示
        /// </summary>
        public string CarType { get; set; }

        /// <summary>
        /// 已取消/已到店/逾期未到店/逾期已到店/有效状态
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// 状态显示
        /// </summary>
        public string StatusDisplay { get; set; }

        /// <summary>
        /// 预约时间
        /// </summary>
        public string ReserveTime { get; set; }

        /// <summary>
        /// 预约地址
        /// </summary>
        public string ReserveAddress { get; set; }
    }
}
