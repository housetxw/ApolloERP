using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Service.Core.Model
{
    public class TechPerformanceDto
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
        /// 手机号码
        /// </summary>
        public string Mobile { get; set; }

        /// <summary>
        ///  日期
        /// </summary>
        public DateTime InstallDate { get; set; }

        /// <summary>
        /// 项目
        /// </summary>
        public string ItemName { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public decimal ItemNum { get; set; }
        /// <summary>
        /// 金额 
        /// </summary>
        public decimal ItemAmt { get; set; }
        /// <summary>
        /// 绩效
        /// </summary>
        public decimal ItemJiXiao { get; set; }

    }
}
