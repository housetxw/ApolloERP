using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Api.Core.Request.Reserve
{
    /// <summary>
    /// 
    /// </summary>
    public class EditReserveRequest
    {
        /// <summary>
        /// 客户姓名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 预约Id
        /// </summary>
        public long ReserveId { get; set; }

        /// <summary>
        /// 预约类型
        /// </summary>
        public string ReserveType { get; set; }

        /// <summary>
        /// 预约时间
        /// </summary>
        public string ReserveTime { get; set; }

        /// <summary>
        /// 技师Id
        /// </summary>
        public string TechId { get; set; }

        /// <summary>
        /// 技师姓名
        /// </summary>
        public string TechName { get; set; }

        /// <summary>
        /// 预约备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 预约车辆信息
        /// </summary>
        public ReserveVehicleRequest Vehicle { get; set; }
    }

    /// <summary>
    /// 预约车辆Request
    /// </summary>
    public class ReserveVehicleRequest
    {
        /// <summary>
        /// 车Id
        /// </summary>
        public string CarId { get; set; } = string.Empty;

        /// <summary>
        /// 车牌信息
        /// </summary>
        public string CarPlate { get; set; } = string.Empty;

        /// <summary>
        /// 品牌
        /// </summary>
        public string Brand { get; set; } = string.Empty;

        /// <summary>
        /// 车系
        /// </summary>
        public string Vehicle { get; set; } = string.Empty;

        /// <summary>
        /// 车系Id
        /// </summary>
        public string VehicleId { get; set; } = string.Empty;

        /// <summary>
        /// 排量
        /// </summary>
        public string PaiLiang { get; set; } = string.Empty;

        /// <summary>
        /// 生产年份
        /// </summary>
        public string Nian { get; set; } = string.Empty;

        /// <summary>
        /// 款型
        /// </summary>
        public string SalesName { get; set; } = string.Empty;

        /// <summary>
        /// 五级车型Tid
        /// </summary>
        public string Tid { get; set; } = string.Empty;
    }
}
