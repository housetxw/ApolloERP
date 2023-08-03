using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Ae.FMS.Service.Core.Enums;

namespace Ae.FMS.Service.Core.Request.Settlement
{
    /// <summary>
    /// 得到财务账单列表请求对象
    /// </summary>
    public class GetFianceReconciliationStatusListRequest:BasePageRequest
    {
        /// <summary>
        /// 门店Id
        /// </summary>
        [Required]
        public int ShopId { get; set; }
        /// <summary>
        /// 账单的状态
        /// </summary>
        [Required]
        public FinanceBillStatus Status { get; set; }

        /// <summary>
        /// 搜索的类型OrderNo、Telephone、ProductName
        /// </summary>
        public string SearchType { get; set; }

        /// <summary>
        /// 内容。
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 创建开始时间
        /// </summary>
        public string StartOrderTime { get; set; }

        /// <summary>
        /// 创建结束时间
        /// </summary>
        public string EndOrderTime { get; set; }

        /// <summary>
        /// 开始安装时间
        /// </summary>
        public string StartInstalledTime { get; set; }

        /// <summary>
        /// 结束安装时间
        /// </summary>
        public string EndInstalledTime { get; set; }


    }
}
