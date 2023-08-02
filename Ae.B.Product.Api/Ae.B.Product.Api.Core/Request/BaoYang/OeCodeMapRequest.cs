using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Product.Api.Core.Request.BaoYang
{
    /// <summary>
    /// 
    /// </summary>
    public class OeCodeMapRequest : BasePageRequest
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
