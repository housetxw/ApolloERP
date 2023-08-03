using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Ae.Receive.Service.Core.Request.Arrival
{
    public  class UpdateArrivalVehicleRequest
    {
        /// <summary>
        /// CarId
        /// </summary>
        public string CarId { get; set; }

        /// <summary>
        /// 品牌
        /// </summary>
        public string Brand { get; set; }

        /// <summary>
        /// 车标
        /// </summary>
        public string CarLogo { get; set; }

        /// <summary>
        /// 车系（EG:A4L-一汽大众奥迪）
        /// </summary>
        public string Vehicle { get; set; } = string.Empty;

        /// <summary>
        /// 车牌
        /// </summary>
        public string CarNo { get; set; }

        /// <summary>
        /// 到店记录Id
        /// </summary>
        public long ArrivalId { get; set; }

        /// <summary>
        /// tid
        /// </summary>
        public string Tid { get; set; } = string.Empty;

        /// <summary>
        /// 车系id
        /// </summary>
        public string VehicleId { get; set; } = string.Empty;

        /// <summary>
        /// 排量
        /// </summary>
        public string PaiLiang { get; set; } = string.Empty;

        /// <summary>
        /// 年款
        /// </summary>
        public string Nian { get; set; } = string.Empty;

        /// <summary>
        /// 款型
        /// </summary>
        public string SalesName { get; set; } = string.Empty;
    }
}
