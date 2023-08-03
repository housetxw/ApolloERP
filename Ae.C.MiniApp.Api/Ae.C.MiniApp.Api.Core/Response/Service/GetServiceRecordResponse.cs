using Ae.C.MiniApp.Api.Core.Model.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.MiniApp.Api.Core.Response.Service
{
    /// <summary>
    /// 服务记录返回对象
    /// </summary>
    public class GetServiceRecordResponse
    {
        /// <summary>
        /// 车辆信息
        /// </summary>
        public VehicleVo Vehicle { get; set; } = null;

        /// <summary>
        /// 消费历史信息
        /// </summary>
        public ConsumptionHistoryVo ConsumptionHistory { get; set; } = null;

        /// <summary>
        /// 维修保养历史信息
        /// </summary>
        public List<MaintenanceRecordVo> MaintenanceRecordHistory { get; set; } = null;


    }
}
