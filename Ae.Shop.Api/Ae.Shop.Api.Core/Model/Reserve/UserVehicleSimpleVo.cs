using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Api.Core.Model.Reserve
{
    /// <summary>
    /// 用户车型
    /// </summary>
    public class UserVehicleSimpleVo
    {
        /// <summary>
        /// 车Id
        /// </summary>
        public string CarId { get; set; }

        /// <summary>
        /// 用户Id
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// 车牌
        /// </summary>
        public string CarPlate { get; set; }

        /// <summary>
        /// 品牌
        /// </summary>
        public string Brand { get; set; }

        /// <summary>
        /// 品牌图片地址
        /// </summary>
        public string BrandUrl { get; set; }

        /// <summary>
        /// 车系不能为空
        /// </summary>
        public string Vehicle { get; set; }

        /// <summary>
        /// 车系Id
        /// </summary>
        public string VehicleId { get; set; }

        /// <summary>
        /// 排量
        /// </summary>
        public string PaiLiang { get; set; }

        /// <summary>
        /// 生产年份
        /// </summary>
        public string Nian { get; set; }

        /// <summary>
        /// 五级车型Tid
        /// </summary>
        public string Tid { get; set; }

        /// <summary>
        /// 款型
        /// </summary>
        public string SalesName { get; set; }

        /// <summary>
        /// 公里数
        /// </summary>
        public int TotalMileage { get; set; }

        /// <summary>
        /// 车架号
        /// </summary>
        public string VinCode { get; set; }

        /// <summary>
        /// 默认车型
        /// </summary>
        public bool DefaultCar { get; set; }

        /// <summary>
        /// 车型展示拼接
        /// </summary>
        public string CarType { get; set; }
    }
}
