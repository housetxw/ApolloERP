using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Product.Service.Core.Request
{
    /// <summary>
    /// 
    /// </summary>
    public class BatchGenerationProductsRequest
    {
        /// <summary>
        /// 类目Id
        /// </summary>
        public List<string> CategoryId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int MinId { get; set; }
    }
}
