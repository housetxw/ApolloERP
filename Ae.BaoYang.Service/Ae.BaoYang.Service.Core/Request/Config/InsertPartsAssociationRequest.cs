using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Ae.BaoYang.Service.Core.Request.Config
{
    /// <summary>
    /// 配件关联产品Request
    /// </summary>
    public class InsertPartsAssociationRequest
    {
        /// <summary>
        /// 
        /// </summary>
        public List<PartProductRefRequest> PartProductRef { get; set; }

        /// <summary>
        /// 操作人
        /// </summary>
        public string SubmitBy { get; set; }
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
