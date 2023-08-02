using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Product.Api.Client.Request
{
    public class UpdateOePartCodeRemoteRequest
    {
        /// <summary>
        /// 五级车型Tid
        /// </summary>
        public string Tid { get; set; }

        /// <summary>
        /// 配件类型
        /// </summary>
        public string PartName { get; set; }

        /// <summary>
        /// 原OE号
        /// </summary>
        public string OriginalOePartCode { get; set; }

        /// <summary>
        /// Oe号
        /// </summary>
        public string OePartCode { get; set; }

        /// <summary>
        /// 操作人
        /// </summary>
        public string SubmitBy { get; set; }
    }
}
