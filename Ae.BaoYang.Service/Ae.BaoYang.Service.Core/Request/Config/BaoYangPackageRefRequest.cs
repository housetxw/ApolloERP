using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.BaoYang.Service.Core.Request.Config
{
    /// <summary>
    /// 车型关联套餐列表request
    /// </summary>
    public class BaoYangPackageRefRequest
    {
        /// <summary>
        /// 五级车型Tid
        /// </summary>
        public List<string> TidList { get; set; }

        /// <summary>
        /// 套餐Id
        /// </summary>
        public string PackageId { get; set; }
    }
}
