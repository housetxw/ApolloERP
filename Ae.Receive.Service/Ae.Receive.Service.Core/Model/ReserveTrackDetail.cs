using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Receive.Service.Core.Model
{
    /// <summary>
    /// 预约日志详细
    /// </summary>
    public class ReserveTrackDetail
    {
        /// <summary>
        /// 字段
        /// </summary>
        public string Field { get; set; }

        /// <summary>
        /// 字段注释
        /// </summary>
        public string FieldName { get; set; }

        /// <summary>
        /// 修改之前的值
        /// </summary>
        public string BeforeValue { get; set; } = string.Empty;

        /// <summary>
        /// 修改之后的值
        /// </summary>
        public string AfterValue { get; set; } = string.Empty;
    }
}
