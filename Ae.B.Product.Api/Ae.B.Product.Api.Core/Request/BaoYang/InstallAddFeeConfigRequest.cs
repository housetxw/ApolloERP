using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Product.Api.Core.Request.BaoYang
{
    /// <summary>
    /// InstallAddFeeConfigRequest
    /// </summary>
    public class InstallAddFeeConfigRequest : BasePageRequest
    {
        /// <summary>
        /// 服务pid
        /// </summary>
        public string ServiceId { get; set; }

        /// <summary>
        /// 指导价
        /// </summary>
        public decimal? GuidePrice { get; set; }
    }
}
