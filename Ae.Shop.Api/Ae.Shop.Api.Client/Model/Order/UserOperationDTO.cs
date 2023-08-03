using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Api.Client.Model
{
    /// <summary>
    /// 按钮操作
    /// </summary>
    public class UserOperationDTO
    {
        /// <summary>
        /// 订单用户可执行操作枚举
        /// </summary>
        public string Function { get; set; }

        /// <summary>
        /// 订单用户可执行操作中文名
        /// </summary>
        public string ShowFunctionName { get; set; }

        /// <summary>
        /// 展示的序号
        /// </summary>
        public int Sort { get; set; }

        /// <summary>
        /// 是否突出
        /// </summary>
        public bool IsImportance { get; set; }

        /// <summary>
        /// 评论的Id
        /// </summary>
        public long CommentId { get; set; }
    }
}
