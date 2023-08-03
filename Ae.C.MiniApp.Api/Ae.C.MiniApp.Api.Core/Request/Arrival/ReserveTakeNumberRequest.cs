using Ae.C.MiniApp.Api.Core.Model.Arrival;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.C.MiniApp.Api.Core.Request.Arrival
{
    /// <summary>
    /// 预约排队拿号
    /// </summary>
    public class ReserveTakeNumberRequest
    {
        /// <summary>
        /// 车型的信息
        /// </summary>
        public VehicleVo Vechicle { get; set; }

        /// <summary>
        /// 服务类型
        /// </summary>
        public string ServiceType { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// UserId
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// ShopId
        /// </summary>
        [Required]
        public long ShopId { get; set; }

        /// <summary>
        /// 门店Name
        /// </summary>
        public string ShopName { get; set; }

        /// <summary>
        /// 预约号
        /// </summary>
        [Required]
        public int ReserverNo { get; set; }

    }
}
