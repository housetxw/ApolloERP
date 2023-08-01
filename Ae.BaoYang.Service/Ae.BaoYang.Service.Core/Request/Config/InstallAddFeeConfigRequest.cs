using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.BaoYang.Service.Core.Request.Config
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
