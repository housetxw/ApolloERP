using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.FMS.Service.Core.Request
{
    public class CreateAccountCheckLogRequest
    {
        /// <summary>
        /// 关联单号
        /// </summary>
        public string RelationNo { get; set; } = string.Empty;
        /// <summary>
        /// 关联类型
        /// </summary>
        public string RelationType { get; set; } = string.Empty;
        /// <summary>
        /// 门店编号
        /// </summary>
        public long LocationId { get; set; }
        /// <summary>
        /// 操作前值
        /// </summary>
        public string BeforeValue { get; set; } = string.Empty;
        /// <summary>
        /// 操作后值
        /// </summary>
        public string AfterValue { get; set; } = string.Empty;

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; } = string.Empty;

        /// <summary>
        /// 创建人
        /// </summary>
        public string CreateBy { get; set; } = string.Empty;
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; } = new DateTime(1900, 1, 1);
    }
}
