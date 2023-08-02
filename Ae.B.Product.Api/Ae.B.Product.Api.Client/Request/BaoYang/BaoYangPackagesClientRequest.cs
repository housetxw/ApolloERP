using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Product.Api.Client.Request.BaoYang
{
    public class BaoYangPackagesClientRequest
    {
        /// <summary>
        /// 车型
        /// </summary>
        public VehicleClientRequest Vehicle { get; set; }

        /// <summary>
        /// 用户Id
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// 保养类型（Eg:xby,ys……）
        /// </summary>
        public List<string> BaoYangType { get; set; }

        /// <summary>
        /// 产品pid
        /// </summary>
        public List<string> ProductId { get; set; }

        /// <summary>
        /// 省Id
        /// </summary>
        public string ProvinceId { get; set; }

        /// <summary>
        /// 市Id
        /// </summary>
        public string CityId { get; set; }

        /// <summary>
        /// 渠道
        /// </summary>
        public string Channel { get; set; }

        /// <summary>
        /// 门店编号
        /// </summary>
        public int ShopId { get; set; }
    }
}
