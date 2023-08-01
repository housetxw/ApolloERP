using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.BaoYang.Service.Core.Model.Config
{
    /// <summary>
    /// 车型关联套餐列表response
    /// </summary>
    public class BaoYangPackageRefVo
    {
        /// <summary>
        /// 主键
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 车型Id
        /// </summary>
        public string Tid { get; set; }

        /// <summary>
        /// 套餐类型
        /// </summary>
        public string BaoYangType { get; set; }

        /// <summary>
        /// 保养套餐类型中文显示
        /// </summary>
        public string ByTypeName { get; set; }

        /// <summary>
        /// 套餐Id
        /// </summary>
        public string PackagePid { get; set; }

        /// <summary>
        /// 套餐名称
        /// </summary>
        public string PackageName { get; set; }

        /// <summary>
        /// 套餐价格
        /// </summary>
        public decimal PackagePrice { get; set; }
    }
}
