using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Vehicle.Service.Core.Request
{
    /// <summary>
    /// 添加车型
    /// </summary>
    public class AddVehicleRequest
    {
        /// <summary>
        /// 品牌
        /// </summary>
        public string Brand { get; set; }

        /// <summary>
        /// 车系
        /// </summary>
        public string Vehicle { get; set; }

        /// <summary>
        /// 车系Id
        /// </summary>
        public string VehicleId { get; set; }

        /// <summary>
        /// 车系描述
        /// </summary>
        public string VehicleSeries { get; set; }

        /// <summary>
        /// 发动机编号
        /// </summary>
        public string EngineNo { get; set; }

        /// <summary>
        /// 排量
        /// </summary>
        public string PaiLiang { get; set; }

        /// <summary>
        /// 年款
        /// </summary>
        public string Nian { get; set; }

        /// <summary>
        /// 开始生产年份
        /// </summary>
        public string ListedYear { get; set; }

        /// <summary>
        /// 停止生产年份
        /// </summary>
        public string StopProductionYear { get; set; }

        /// <summary>
        /// 款型
        /// </summary>
        public string SalesName { get; set; }

        /// <summary>
        /// 气缸数
        /// </summary>
        public int CylinderNumber { get; set; }

        /// <summary>
        /// 每门气缸数
        /// </summary>
        public int ValveNumPerCylinder { get; set; }
    }
}
