using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Api.Core.Model
{
    public class OccupyQueueDTO
    {
        /// <summary>
        /// 序号
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 类型(Order,OrderMQ)
        /// </summary>
        public string QueueType { get; set; } = string.Empty;
        /// <summary>
        /// 关联的单号
        /// </summary>
        public string SourceNo { get; set; } = string.Empty;
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; } = string.Empty;
        /// <summary>
        /// 是否是处理中
        /// </summary>
        public string IsProcessing { get; set; }
        /// <summary>
        /// 占用状态(1新建,2未占用，3已占用，4已取消)
        /// </summary>
        public string Status { get; set; } = string.Empty;
        /// <summary>
        /// 占库优先级
        /// </summary>
        public short Pripority { get; set; }
        /// <summary>
        /// 占库规则id
        /// </summary>
        public long RuleId { get; set; }
        /// <summary>
        /// 删除标识
        /// </summary>
        public sbyte IsDeleted { get; set; }
        /// <summary>
        /// 创建用户ID
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
    }
}