using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Product.Api.Client.Model
{
    public class ProductSecurityCodeDto
    {
        /// <summary>
        /// 防伪码
        /// </summary>
        public string SecurityCode { get; set; }

        /// <summary>
        /// 查询次数
        /// </summary>
        public int SearchCount { get; set; }

        /// <summary>
        /// 首查时间
        /// </summary>
        public DateTime FirstSearchTime { get; set; }
    }
}
