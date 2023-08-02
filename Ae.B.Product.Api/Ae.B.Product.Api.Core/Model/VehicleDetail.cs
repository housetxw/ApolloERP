using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Product.Api.Core.Model
{
    /// <summary>
    /// 车型信息
    /// </summary>
    public class VehicleDetail
    {
        /// <summary>
        /// 品牌
        /// </summary>
        public string Brand { get; set; }

        /// <summary>
        /// 车系表里面的品牌
        /// </summary>
        public string BrandFromVehicleType { get; set; }

        /// <summary>
        /// 车系
        /// </summary>
        public string Vehicle { get; set; }

        /// <summary>
        /// 车系表的车系
        /// </summary>
        public string VehicleFromVehicleType { get; set; }

        /// <summary>
        /// 车系Id
        /// </summary>
        public string VehicleId { get; set; }

        /// <summary>
        /// 排量
        /// </summary>
        public string PaiLiang { get; set; }

        /// <summary>
        /// 年款
        /// </summary>
        public string Nian { get; set; }

        /// <summary>
        /// 款型
        /// </summary>
        public string SalesName { get; set; }

        /// <summary>
        /// 五级车型Tid
        /// </summary>
        public string Tid { get; set; }

        /// <summary>
        /// 开始生产年份
        /// </summary>
        public string ListedYear { get; set; }

        /// <summary>
        /// 停止生产年份
        /// </summary>
        public string StopProductionYear { get; set; }

        /// <summary>
        /// 发动机型号
        /// </summary>
        public string EngineNo { get; set; }

        /// <summary>
        /// 变速箱类型(英文)
        /// </summary>
        public string TransmissionTypeE { get; set; }
    }
}
