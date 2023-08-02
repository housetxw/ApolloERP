using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Product.Api.Core.Model.BaoYang
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
