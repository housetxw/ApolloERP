using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Product.Api.Core.Request.BaoYang
{
    /// <summary>
    /// 
    /// </summary>
    public class InsertPartsAssociationRequest
    {
        /// <summary>
        /// 
        /// </summary>
        public List<PartProductRefRequest> PartProductRef { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class PartProductRefRequest
    {
        /// <summary>
        /// 商品Pid
        /// </summary>
        public string Pid { get; set; }

        /// <summary>
        /// 配件号
        /// </summary>
        public string PartCode { get; set; }
    }
}
