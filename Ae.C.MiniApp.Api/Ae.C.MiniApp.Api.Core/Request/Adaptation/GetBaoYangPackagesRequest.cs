using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;

namespace Ae.C.MiniApp.Api.Core.Request.Adaptation
{
    /// <summary>
    /// 保养适配首页接口
    /// </summary>
    public class GetBaoYangPackagesRequest
    {
        /// <summary>
        /// 车型
        /// </summary>
        [Required(ErrorMessage = "Vehicle不能为空")]
        public VehicleRequest Vehicle { get; set; }

        /// <summary>
        /// 用户Id
        /// </summary>
        [IgnoreDataMember]
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
        [IgnoreDataMember]
        public string Channel { get; set; }

        /// <summary>
        /// 门店编号
        /// </summary>
        public int ShopId { get; set; }
    }
}
