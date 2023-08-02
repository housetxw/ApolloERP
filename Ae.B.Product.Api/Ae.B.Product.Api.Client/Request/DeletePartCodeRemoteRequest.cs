using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Product.Api.Client.Request
{
    public class DeletePartCodeRemoteRequest
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
        /// 主键删除
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// OE号删除
        /// </summary>
        public string OePartCode { get; set; }

        /// <summary>
        /// 特殊配件 种类删除
        /// </summary>
        public string PartType { get; set; }

        public string SubmitBy { get; set; }
    }
}
