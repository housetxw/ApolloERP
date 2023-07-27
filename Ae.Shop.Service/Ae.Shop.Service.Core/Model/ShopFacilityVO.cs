using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Service.Core.Model
{
    /// <summary>
    /// 门店设施信息
    /// </summary>
    public class ShopFacilityVO
    {
        /// <summary>
        /// 休息室
        /// </summary>
        public bool IsLoungeRoom { get; set; }
        /// <summary>
        /// 卫生间
        /// </summary>
        public bool IsRestRoom { get; set; }
        /// <summary>
        /// 免费wifi
        /// </summary>
        public bool IsFreeWiFi { get; set; }
        /// <summary>
        /// 汽车故障诊断仪
        /// </summary>
        public bool CarFaultDiagnosticTool { get; set; }
        /// <summary>
        /// 柱式举升机
        /// </summary>
        public bool IsPostLift { get; set; }
    }
}
