using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Order.Service.Dal.Condition
{
    /// <summary>
    /// 基本的分页请求
    /// </summary>
    public class BasePageCondition
    {
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
