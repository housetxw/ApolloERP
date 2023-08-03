using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.ShopOrder.Service.Core.Model
{
    /// <summary>
    /// 待签收包裹
    /// </summary>
    public class WaitingSignPackageVO
    {
        /// <summary>
        /// 包裹号
        /// </summary>
        public string PackageNo { get; set; }

        public string Status { get; set; }

        public int Code { get; set; }
    }
}
