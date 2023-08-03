using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Api.Core.Response.Arrival
{
    /// <summary>
    /// 到店查询条件获取
    /// </summary>
    public class ArrivalListConditionResponse
    {
        /// <summary>
        /// 状态
        /// </summary>
        public List<StatusEnum> Status { get; set; }
    }

    /// <summary>
    /// 状态枚举
    /// </summary>
    public class StatusEnum
    {
        /// <summary>
        /// 值
        /// </summary>
        public object Value { get; set; }

        /// <summary>
        /// 显示
        /// </summary>
        public string DisplayName { get; set; }
    }
}
