using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.Shop.Service.Core.Request
{
    public class TechPerformanceRequest
    {
        [Required(ErrorMessage = "门店ID不能为空")]
        public long ShopId { get; set; }

        /// <summary>
        /// 查询类型
        /// </summary>
        public PerformanceGroupType GroupType { get; set; }

        /// <summary>
        /// 项目类型
        /// </summary>
        public PerformanceItemType ItemType { get; set; }

        /// <summary>
        /// 技师ID
        /// </summary>
        public string EmployeeId { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string EmployeeName { get; set; }

        /// <summary>
        /// 技师手机号
        /// </summary>
        public string Mobile { get; set; }

        /// <summary>
        ///  开始日期
        /// </summary>
        [Required(ErrorMessage = "开始日期不能为空")]
        public DateTime StartDate { get; set; }

        /// <summary>
        ///  结束日期
        /// </summary>
        [Required(ErrorMessage = "结束日期不能为空")]
        public DateTime EndDate { get; set; }
    }

    /// <summary>
    /// 绩效项目类型枚举
    /// </summary>
    public enum PerformanceItemType
    {
        /// <summary>
        /// 套餐卡
        /// </summary>
        PackageCard = 1,
        /// <summary>
        /// 次数项目
        /// </summary>
        NumItem = 2,
        /// <summary>
        /// 金额项目
        /// </summary>
        AmtItem = 0
    }

    /// <summary>
    /// 绩效查询类型枚举
    /// </summary>
    public enum PerformanceGroupType
    {
        /// <summary>
        /// 全部汇总
        /// </summary>
        GroupByAll = 0,
        /// <summary>
        /// 按日汇总
        /// </summary>
        GroupByDay = 1
    }

}
