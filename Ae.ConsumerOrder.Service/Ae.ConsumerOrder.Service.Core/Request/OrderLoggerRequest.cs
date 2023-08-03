using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.ConsumerOrder.Service.Core.Request
{
    public class OrderLoggerRequest
    {
        /// <summary>
        /// 主键
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 订单Id
        /// </summary>
        public long OrderId { get; set; }
        /// <summary>
        /// 自定义类型1
        /// </summary>
        public string Type1 { get; set; } = string.Empty;
        /// <summary>
        /// 自定义类型2
        /// </summary>
        public string Type2 { get; set; } = string.Empty;
        /// <summary>
        /// 自定义过滤1
        /// </summary>
        public string Filter1 { get; set; } = string.Empty;
        /// <summary>
        /// 自定义过滤2
        /// </summary>
        public string Filter2 { get; set; } = string.Empty;
        /// <summary>
        /// 操作摘要
        /// </summary>
        public string Summary { get; set; } = string.Empty;
        /// <summary>
        /// 操作前值
        /// </summary>
        public string BeforeValue { get; set; } = string.Empty;
        /// <summary>
        /// 操作后值
        /// </summary>
        public string AfterValue { get; set; } = string.Empty;
        /// <summary>
        /// 删除标识
        /// </summary>
        public sbyte IsDeleted { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public string CreateBy { get; set; } = string.Empty;
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 修改人
        /// </summary>
        public string UpdateBy { get; set; } = string.Empty;
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime UpdateTime { get; set; } = new DateTime(1900, 1, 1);

        /// <summary>
        /// 订单的产品信息
        /// </summary>
        public bool IsRecordOrderProduct { get; set; } = false;
    }
}
