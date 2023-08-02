using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Product.Service.Client.Request.BaoYang
{
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
