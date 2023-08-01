using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.BaoYang.Service.Core.Model.Config
{
    /// <summary>
    /// 保养套餐返回
    /// </summary>
    public class BaoYangPackageVo
    {
        /// <summary>
        /// 套餐pid
        /// </summary>
        public string PackagePid { get; set; }

        /// <summary>
        /// 套餐名称
        /// </summary>
        public string PackageName { get; set; }
    }
}
