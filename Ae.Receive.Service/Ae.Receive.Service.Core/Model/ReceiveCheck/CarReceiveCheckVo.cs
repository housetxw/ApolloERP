using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Receive.Service.Core.Model.ReceiveCheck
{
    /// <summary>
    /// 车辆历史检测报告
    /// </summary>
    public class CarReceiveCheckVo
    {
        /// <summary>
        /// 到店Id
        /// </summary>
        public long RecId { get; set; }

        /// <summary>
        /// 到店日期
        /// </summary>
        public string ReceiveDate { get; set; }

        /// <summary>
        /// 门店简单名
        /// </summary>
        public string ShopSimpleName { get; set; }

        /// <summary>
        /// 检查报告
        /// </summary>
        public CarCheckReportVo CheckReport { get; set; }
    }

    /// <summary>
    /// 车辆报告
    /// </summary>
    public class CarCheckReportVo : CheckStatisticsData
    {
        /// <summary>
        /// 检查Id
        /// </summary>
        public long CheckId { get; set; }
    }
}
