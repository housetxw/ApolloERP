using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Api.Core.Model
{
    public class TempProductDTO
    {
        public string ProductId { get; set; } = string.Empty;

        public string ProductName { get; set; } = string.Empty;

        public string Unit { get; set; } = string.Empty;

        /// <summary>
        /// 产品类型 Company.总部产品 Shop.其他产品
        /// </summary>
        public string ProductType { get; set; } = string.Empty;
             
    }
}
