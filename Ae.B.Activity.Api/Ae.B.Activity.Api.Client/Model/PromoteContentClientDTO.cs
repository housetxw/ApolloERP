using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Activity.Api.Client.Model
{
    public class PromoteContentClientDTO
    {
        
        /// <summary>
      /// 主键
      /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 类型（0未设置 1文章 2海报 3段子）
        /// </summary>
        public sbyte Type { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; } = string.Empty;
        /// <summary>
        /// 内容（类型为海报时为其地址，其他为富文本）
        /// </summary>
        public string Content { get; set; } = string.Empty;
        /// <summary>
        /// 描述（选填）
        /// </summary>
        public string Description { get; set; } = string.Empty;
        /// <summary>
        /// 列表缩略图地址
        /// </summary>
        public string ThumbnailUrl { get; set; } = string.Empty;
        /// <summary>
        /// 是否含附件（0否 1是）
        /// </summary>
        public sbyte IsContainAttachment { get; set; }
        /// <summary>
        /// 审核状态（0待审核 1审核通过 2审核不通过）
        /// </summary>
        public sbyte CheckStatus { get; set; }
        /// <summary>
        /// 上下架状态（0下架 1上架）
        /// </summary>
        public sbyte OnlineStatus { get; set; }
        /// <summary>
        /// 发布时间
        /// </summary>
        public DateTime PublishTime { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 转发次数
        /// </summary>
        public int ForwardNum { get; set; }
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
        public string No { get; set; }

        public List<PromoteContentAttachmentClientDTO> Attachments { get; set; } = new List<PromoteContentAttachmentClientDTO>();

    }
}
