using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Api.Core.Model
{
    public class InventoryExceptionFileDTO
    {
        /// <summary>
        /// 主键
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 关联单号
        /// </summary>
        public long RelationId { get; set; }
        /// <summary>
        /// 关联类型
        /// </summary>
        public string RelationType { get; set; } = string.Empty;
        /// <summary>
        /// 文件名
        /// </summary>
        public string FileName { get; set; } = string.Empty;
        /// <summary>
        /// 文件地址
        /// </summary>
        public string FileUrl { get; set; } = string.Empty;
        /// <summary>
        /// 文件类型(图片 文件)
        /// </summary>
        public string FileType { get; set; } = string.Empty;
        /// <summary>
        /// 是否有效
        /// </summary>
        public sbyte IsActive { get; set; }
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
    }
}