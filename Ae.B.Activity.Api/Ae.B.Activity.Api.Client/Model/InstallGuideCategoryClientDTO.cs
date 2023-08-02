using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Activity.Api.Client.Model
{
    public class InstallGuideCategoryClientDTO
    {
        /// <summary>
        /// 主键 安装指导分类
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 分类名称
        /// </summary>
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// 图表地址
        /// </summary>
        public string IconUrl { get; set; } = string.Empty;
        /// <summary>
        /// 是否删除 0否 1是
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
    }
}