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

        public DateTime InstallDate { get; set; }

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
        /// <summary>
        /// 套餐卡数量
        /// </summary>
        public decimal CardItemNum { get; set; }
        /// <summary>
        /// 套餐卡绩效
        /// </summary>
        public decimal CardItemAmt { get; set; }
        /// <summary>
        /// 次数项目数量
        /// </summary>
        public decimal NumItemNum { get; set; }
        /// <summary>
        /// 次数项目绩效
        /// </summary>
        public decimal NumItemAmt { get; set; }
        /// <summary>
        /// 金额项目单数
        /// </summary>
        public decimal AmtItemNum { get; set; }
        /// <summary>
        /// 金额项目绩效
        /// </summary>
        public decimal AmtItemAmt { get; set; }
    }
}
