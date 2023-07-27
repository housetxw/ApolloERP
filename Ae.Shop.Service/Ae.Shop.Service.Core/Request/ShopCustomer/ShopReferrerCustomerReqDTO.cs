using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.Shop.Service.Core.Request
{
    public class ShopReferrerCustomerReqDTO
    {
        [Required(ErrorMessage = "门店Id不能为空")]
        [Range(1, long.MaxValue, ErrorMessage = "门店Id必须大于0")]
        public long ShopId { get; set; }
    }

    public class DrainageStatisticsPageReqDTO : BaseOnlyPageRequest
    {
        [Required(ErrorMessage = "门店Id不能为空")]
        [Range(1, long.MaxValue, ErrorMessage = "门店Id必须大于0")]
        public long ShopId { get; set; }

        public DrainageStatisticsType DrainageStatisticsType { get; set; } = DrainageStatisticsType.Employee;

        [Required(ErrorMessage = "请输入开始时间")]
        public string StartTime { get; set; }

        [Required(ErrorMessage = "请输入结束时间")]
        public string EndTime { get; set; }
    }

    public enum DrainageStatisticsType
    {
        [Description("员工")]
        Employee = 1,
        [Description("分享类型")]
        ShareType = 2
    }

}
