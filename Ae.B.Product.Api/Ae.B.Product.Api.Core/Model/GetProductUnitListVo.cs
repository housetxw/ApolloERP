using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Product.Api.Core.Model
{
    /// <summary>
    /// 产品单位
    /// </summary>
    public class GetProductUnitListVo
    {
        /// <summary>
        /// 主键
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 单位名称
        /// </summary>
        public string UnitName { get; set; } = string.Empty;
        /// <summary>
        /// 标记删除
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
        /// 更新人
        /// </summary>
        public string UpdateBy { get; set; } = string.Empty;
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateTime { get; set; } = new DateTime(1900, 1, 1);
    }
}
