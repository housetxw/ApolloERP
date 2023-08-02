using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Activity.Api.Client.Model
{
    public class PromoteContentAttachmentClientDTO
    {
        /// <summary>
        /// 主键
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 推广内容ID
        /// </summary>
        public long PromoteContentId { get; set; }
        /// <summary>
        /// 附件类型（0未设置 1图片）
        /// </summary>
        public sbyte AttachmentType { get; set; }
        /// <summary>
        /// 附件名称
        /// </summary>
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// 附件资源地址
        /// </summary>
        public string Url { get; set; } = string.Empty;
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
    }

}
