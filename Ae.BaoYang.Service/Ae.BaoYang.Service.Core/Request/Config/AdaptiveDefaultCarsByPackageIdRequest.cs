using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.BaoYang.Service.Core.Request.Config
{
    /// <summary>
    /// 套餐适配默认车型
    /// </summary>
    public class AdaptiveDefaultCarsByPackageIdRequest
    {
        /// <summary>
        /// 套餐pid
        /// </summary>
        public List<BaoYangPackageRequest> BaoYangPackage { get; set; }
    }

    /// <summary>
    /// 保养套餐
    /// </summary>
    public class BaoYangPackageRequest
    {
        /// <summary>
        /// 套餐pid
        /// </summary>
        public string PackagePid { get; set; }

        /// <summary>
        /// 保养类型
        /// </summary>
        public string BaoYangType { get; set; }
    }
}
