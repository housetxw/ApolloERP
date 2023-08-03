using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.MiniApp.Api.Core.Response
{
    public class BaseCommentLabelListResponse
    {
        /// <summary>
        /// 标签Id
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 标签名称
        /// </summary>
        public string LabelName { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public int Num { get; set; }
    }
}
