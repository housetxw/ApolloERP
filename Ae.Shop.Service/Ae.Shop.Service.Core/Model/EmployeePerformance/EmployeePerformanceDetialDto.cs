using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Service.Core.Model
{
    /// <summary>
    /// 绩效详情
    /// </summary>
    public class EmployeePerformanceDetialDto: EmployeePerformanceDto
    {
        /// <summary>
        /// 照片地址
        /// </summary>
        public string Avatar { get; set; } = string.Empty;

        /// <summary>
        /// 岗位名称
        /// </summary>
        public string Job { get; set; }

        /// <summary>
        /// 描述信息
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 同比去年
        /// </summary>
        public decimal ComparedLastYearPointPercent { get; set; }

        /// <summary>
        /// 环比上期
        /// </summary>
        public decimal ComparedLastWeekPointPercent { get; set; }
    }
}
