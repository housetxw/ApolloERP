using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Activity.Api.Core.Model
{
    public class InstallGuideFileDTO
    {
        /// <summary>
        /// 主键
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 安装指导编号
        /// </summary>
        public long InstallGuideId { get; set; }
        /// <summary>
        /// 文件名称
        /// </summary>
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// 文件链接地址
        /// </summary>
        public string Url { get; set; } = string.Empty;
        /// <summary>
        /// 文件类型  1 PDF，2 MP4
        /// </summary>
        public sbyte Type { get; set; }
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