using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Product.Api.Core.Model.BaoYang
{
    /// <summary>
    /// BaoYangInstallServiceVo
    /// </summary>
    public class BaoYangInstallServiceVo
    {
        /// <summary>
        ///  产品编码
        /// </summary>
        public string ProductCode { get; set; } = string.Empty;

        /// <summary>
        /// 商品名称
        /// </summary>
        public string Name { get; set; } = string.Empty;
    }
}
