using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.BaoYang.Service.Core.Request.Config
{
    /// <summary>
    /// 辅料配置Request
    /// </summary>
    public class BaoYangPartAccessoryRequest
    {
        /// <summary>
        /// 五级车型TidList
        /// </summary>
        public List<string> TidList { get; set; }
    }
}
