using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Product.Api.Client.Request.BaoYang
{
    public class InsertPartsAssociationClientRequest
    {
        /// <summary>
        /// 
        /// </summary>
        public List<PartProductRefClientRequest> PartProductRef { get; set; }

        /// <summary>
        /// 操作人
        /// </summary>
        public string SubmitBy { get; set; }
    }

    public class PartProductRefClientRequest
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
