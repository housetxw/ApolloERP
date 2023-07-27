using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Service.Core.Model
{
    public class SimpleShopCarDTO
    {
        /// <summary>
        /// 门店车ID
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 状态 0正常 1禁用
        /// </summary>
        public sbyte Status { get; set; }
        /// <summary>
        /// 车牌
        /// </summary>
        public string CarNumber { get; set; } = string.Empty;
        /// <summary>
        /// 品牌
        /// </summary>
        public string Brand { get; set; } = string.Empty;
        /// <summary>
        /// 车系
        /// </summary>
        public string Vehicle { get; set; } = string.Empty;
        /// <summary>
        /// 排量
        /// </summary>
        public string PaiLiang { get; set; } = string.Empty;
        /// <summary>
        /// 年
        /// </summary>
        public string Nian { get; set; } = string.Empty;
        /// <summary>
        /// 款型
        /// </summary>
        public string SalesName { get; set; } = string.Empty;
        /// <summary>
        /// 五级车型
        /// </summary>
        public string VehicleCar { get; set; } = string.Empty;
        /// <summary>
        /// 创建人
        /// </summary>
        public string CreateBy { get; set; } = string.Empty;
        /// <summary>
        /// 修改人
        /// </summary>
        public string UpdateBy { get; set; } = string.Empty;
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime UpdateTime { get; set; } = new DateTime(1900, 1, 1);
    }
}
