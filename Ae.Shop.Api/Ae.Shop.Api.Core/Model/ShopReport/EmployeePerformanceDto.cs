using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Api.Core.Model.ShopReport
{

    public class EmployeePerformanceDto
    {
        /// <summary>
        /// 员工id
        /// </summary>
        public string EmployeeId { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string EmployeeName { get; set; }


        /// <summary>
        /// 安装绩效
        /// </summary>
        public decimal InstallPoint { get; set; }
        /// <summary>
        /// 新客绩效
        /// </summary>
        public decimal PullNewPoint { get; set; }
        /// <summary>
        /// 新客首消费绩效
        /// </summary>
        public decimal CunsumePoint { get; set; }
        /// <summary>
        /// 总绩效
        /// </summary>
        public decimal TotalPoint { get; set; }
        /// <summary>
        /// 手机号码
        /// </summary>
        public string Mobile { get; set; }
    }
}
