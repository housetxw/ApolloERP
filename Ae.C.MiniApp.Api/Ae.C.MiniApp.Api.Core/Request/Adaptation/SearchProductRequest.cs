using Ae.C.MiniApp.Api.Core.Model.Adaptation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;

namespace Ae.C.MiniApp.Api.Core.Request.Adaptation
{
    /// <summary>
    /// 更多商品列表
    /// </summary>
    public class SearchProductRequest
    {
        /// <summary>
        /// 车型
        /// </summary>
        [Required(ErrorMessage = "Vehicle不能为空")]
        public VehicleRequest Vehicle { get; set; }

        /// <summary>
        /// 保养大项目 xby
        /// </summary>
        [Required(ErrorMessage = "PackageType不能为空")]
        public string PackageType { get; set; }

        /// <summary>
        /// 保养项目 jiyou
        /// </summary>
        [Required(ErrorMessage = "BaoYangType不能为空")]
        public string BaoYangType { get; set; }

        /// <summary>
        /// 当前默认需要数量
        /// "OL-CA-XciHu-0W-20|1": 1
        /// </summary>
        [Required(ErrorMessage = "PidCount不能为空")]
        [MinLength(1, ErrorMessage = "PidCount不能为空")]
        public List<PidCountVo> PidCount { get; set; }

        /// <summary>
        /// 筛选条件
        /// 前端用户选择的条件
        /// </summary>
        public Dictionary<string, List<string>> Conditions { get; set; }

        /// <summary>
        /// 省Id
        /// </summary>
        public string ProvinceId { get; set; }

        /// <summary>
        /// 市Id
        /// </summary>
        public string CityId { get; set; }

        /// <summary>
        /// 用户Id
        /// </summary>
        [IgnoreDataMember]
        public string UserId { get; set; }

        /// <summary>
        /// 渠道
        /// </summary>
        [IgnoreDataMember]
        public string Channel { get; set; }

        /// <summary>
        /// 门店Id
        /// </summary>
        public int ShopId { get; set; }
    }
}
