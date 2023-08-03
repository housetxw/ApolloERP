using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.MiniApp.Api.Core.Model
{
    public class BaseOrderCommentSubmitVO : BaseOrderOperationCondition
    {
        /// <summary>
        /// 手写评价内容
        /// </summary>
        public string Connent { get; set; }
        /// <summary>
        /// 是否匿名（客户选择，对技师展示时默认匿名不由此控制）
        /// </summary>
        public bool IsAnonymous { get; set; }
        /// <summary>
        /// 图片集合
        /// </summary>
        public List<string> ImageUrls { get; set; }
    }
}
