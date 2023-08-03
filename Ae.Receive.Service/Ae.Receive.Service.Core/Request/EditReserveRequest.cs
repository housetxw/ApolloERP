using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;

namespace Ae.Receive.Service.Core.Request
{
    /// <summary>
    /// 编辑预约Request
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
        [Range(1,Int32.MaxValue,ErrorMessage = "预约Id必须大于0")]
        public long ReserveId { get; set; }

        /// <summary>
        /// 预约类型
        /// </summary>
        public string ReserveType { get; set; } = string.Empty;

        /// <summary>
        /// 预约时间
        /// </summary>
        [Required(ErrorMessage = "预约时间不能为空")]
        public string ReserveTime { get; set; }

        /// <summary>
        /// 技师Id
        /// </summary>
        public string TechId { get; set; } = Guid.Empty.ToString();

        /// <summary>
        /// 技师姓名
        /// </summary>
        public string TechName { get; set; } = string.Empty;

        /// <summary>
        /// 预约备注
        /// </summary>
        public string Remark { get; set; } = string.Empty;

        /// <summary>
        /// 预约车辆信息
        /// </summary>
        public ReserveVehicleRequest Vehicle { get; set; }

        /// <summary>
        /// 操作人
        /// </summary>
        [Required(ErrorMessage = "操作人不能为空")]
        public string SubmitBy { get; set; }
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

        /// <summary>
        /// Vin码
        /// </summary>
        [IgnoreDataMember]
        public string VinCode { get; set; } = string.Empty;
    }
}
