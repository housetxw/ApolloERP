using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.MiniApp.Api.Core.Model.Product
{
    /// <summary>
    /// 商品组合信息
    /// </summary>
    public class ProductCombinationVo
    {
        /// <summary>
        ///  产品编码
        /// </summary>
        public string ProductCode { get; set; }
        /// <summary>
        /// 商品名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        ///  市场价
        /// </summary>
        public decimal SalesPrice { get; set; }

        /// <summary>
        /// 规格组合
        /// </summary>
        public string Difference { get; set; }
    }
}
