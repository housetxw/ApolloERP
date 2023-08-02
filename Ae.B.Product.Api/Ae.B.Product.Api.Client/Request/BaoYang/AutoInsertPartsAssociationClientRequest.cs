using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Product.Api.Client.Request.BaoYang
{
    /// <summary>
    /// 
    /// </summary>
    public class AutoInsertPartsAssociationClientRequest
    {
        /// <summary>
        /// Pid
        /// </summary>
        public string Pid { get; set; }

        /// <summary>
        /// 提交人
        /// </summary>
        public string SubmitBy { get; set; } = string.Empty;
    }
}
