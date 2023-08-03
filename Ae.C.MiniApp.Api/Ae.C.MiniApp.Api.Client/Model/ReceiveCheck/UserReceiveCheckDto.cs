using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.MiniApp.Api.Client.Model.ReceiveCheck
{
    public class UserReceiveCheckDto
    {
        /// <summary>
        /// 到店Id
        /// </summary>
        public long RecId { get; set; }

        /// <summary>
        /// 车品牌
        /// </summary>
        public string Brand { get; set; }

        /// <summary>
        /// 品牌Url
        /// </summary>
        public string BrandUrl { get; set; }

        /// <summary>
        /// 车牌
        /// </summary>
        public string CarPlate { get; set; }

        /// <summary>
        /// 到店日期
        /// </summary>
        public string ReceiveDate { get; set; }

        /// <summary>
        /// 检查报告
        /// </summary>
        public List<ReceiveCheckStatisticsDto> ReceiveCheck { get; set; }

        /// <summary>
        /// 车况检查报告
        /// </summary>
        public string CarReportUrl { get; set; }
    }

    /// <summary>
    /// 车况检查统计
    /// </summary>
    public class ReceiveCheckStatisticsDto
    {
        /// <summary>
        /// 检查Id
        /// </summary>
        public long CheckId { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// 检查报告类型名称
        /// </summary>
        public string CategoryName { get; set; }

        /// <summary>
        /// 提示信息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 统计数据
        /// </summary>
        public CheckStatisticsDataDto StatisticsData { get; set; }
    }

    /// <summary>
    /// 检查统计数据
    /// </summary>
    public class CheckStatisticsDataDto
    {
        /// <summary>
        /// 正常项 
        /// </summary>
        public int NormalCount { get; set; }

        /// <summary>
        /// 异常项
        /// </summary>
        public int AbNormalCount { get; set; }

        /// <summary>
        /// 未检查项
        /// </summary>
        public int UncheckCount { get; set; }
    }
}
