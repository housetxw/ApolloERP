using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Product.Api.Client.Request.BaoYang
{
    /// <summary>
    /// 根据OE号查询零件号
    /// </summary>
    public class PartCodeAndBrandByOeClientRequest
    {
        /// <summary>
        /// OE号
        /// </summary>
        public string OeCode { get; set; }

        /// <summary>
        /// 配件类型
        /// </summary>
        public string PartName { get; set; }
    }
}
