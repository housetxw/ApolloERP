using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Product.Service.Core.Request.Product
{
    /// <summary>
    /// CheckDoorProductRequest
    /// </summary>
    public class CheckDoorProductRequest
    {
        /// <summary>
        /// 产品pid
        /// </summary>
        public List<string> PidList { get; set; }
    }
}
