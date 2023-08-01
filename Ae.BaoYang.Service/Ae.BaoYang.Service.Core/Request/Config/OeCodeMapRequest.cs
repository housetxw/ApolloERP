using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.BaoYang.Service.Core.Request.Config
{
    /// <summary>
    ///Oe配件映射关系
    /// </summary>
    public class OeCodeMapRequest:BasePageRequest
    {
        /// <summary>
        /// OE件号
        /// </summary>
        public string OeCode { get; set; }

        /// <summary>
        /// 配件号
        /// </summary>
        public string PartCode { get; set; }

        /// <summary>
        /// 配件类型
        /// </summary>
        public string PartType { get; set; }
    }
}
