using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.BaoYang.Service.Dal.Model.Condition
{
    public class BaoYangInstallFeeConfigPageListCondition
    {
        /// <summary>
        /// 服务pid
        /// </summary>
        public string ServiceId { get; set; }

        /// <summary>
        /// 指导价
        /// </summary>
        public decimal? GuidePrice { get; set; }

        /// <summary>
        /// 页码
        /// </summary>
        public int PageIndex { get; set; } = 1;

        /// <summary>
        /// 页大小
        /// </summary>
        public int PageSize { get; set; } = 20;
    }
}
