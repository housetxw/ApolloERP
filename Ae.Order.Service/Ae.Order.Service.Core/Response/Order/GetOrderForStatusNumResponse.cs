using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Order.Service.Core.Response.Order
{
    public class GetOrderForStatusNumResponse
    {
        /// <summary>
        /// All,Waiting
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///数量
        /// </summary>
        public int Count { get; set; }
    }
}
