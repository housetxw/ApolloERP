using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.Shop.Service.Core.Request
{
    public class EmployeePerformanceRequest
    {
        [Required(ErrorMessage = "门店ID不能为空")]
        public long ShopId { get; set; }

        /// <summary>
        /// 排序方式
        /// </summary>
        public PerformanceSortType SortType { get; set; }

        /// <summary>
        /// 搜索关机键字 持搜索员工姓名（模糊搜索）+手机号（精确搜索）
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        ///  开始日期
        /// </summary>
        //[Required(ErrorMessage = "开始日期不能为空")]
        public DateTime StartDate { get; set; }

        /// <summary>
        ///  结束日期
        /// </summary>
        //[Required(ErrorMessage = "结束日期不能为空")]
        public DateTime EndDate { get; set; }
    }

    /// <summary>
    /// 员工绩效排序枚举
    /// </summary>
    public enum PerformanceSortType
    {
        /// <summary>
        /// 安装绩效
        /// </summary>
        InstallPoint = 1,
        /// <summary>
        /// 新客绩效
        /// </summary>
        PullNewPoint = 2,
        /// <summary>
        /// 新客首消费绩效
        /// </summary>
        CunsumePoint = 3,
        /// <summary>
        /// 总绩效
        /// </summary>
        TotalPoint = 0
    }

}
