using Ae.BaoYang.Service.Core.Model;
using Ae.BaoYang.Service.Imp.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.BaoYang.Service.Imp.Model
{
    public class VehicleAdaptationResultModel
    {
        /// <summary>
        /// 套餐数据
        /// </summary>
        public List<BaoYangPackageRefModel> PackageRef { get; set; }

        /// <summary>
        /// 辅料配置
        /// </summary>
        public List<BaoYangAccessoryModel> AccessoryTypeDic { get; set; }

        /// <summary>
        /// 配件配置
        /// </summary>
        public List<BaoYangPartModel> PartTypeDic { get; set; }

        /// <summary>
        /// 产品优先级展示
        /// </summary>
        public List<BaoYangProductPriorityModel> ProductPriority { get; set; }
    }

    /// <summary>
    /// 适配套餐Model
    /// </summary>
    public class BaoYangPackageRefModel
    {
        public string Tid { get; set; }

        /// <summary>
        /// 保养项目
        /// </summary>
        public string BaoYangType { get; set; }

        /// <summary>
        /// 商品Pids
        /// </summary>
        public List<string> Pids { get; set; }
    }
}
