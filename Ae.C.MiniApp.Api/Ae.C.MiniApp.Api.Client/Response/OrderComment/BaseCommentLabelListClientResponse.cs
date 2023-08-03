using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.MiniApp.Api.Client.Response
{
    public class BaseCommentLabelListClientResponse
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
