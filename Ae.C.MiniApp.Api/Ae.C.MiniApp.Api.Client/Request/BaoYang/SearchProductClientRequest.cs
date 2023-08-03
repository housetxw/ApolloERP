using System;
using System.Collections.Generic;
using System.Text;
using Ae.C.MiniApp.Api.Client.Model.BaoYang;

namespace Ae.C.MiniApp.Api.Client.Request.BaoYang
{
    public class SearchProductClientRequest
    {
        /// <summary>
        /// 车型
        /// </summary>
        public VehicleClientRequest Vehicle { get; set; }

        /// <summary>
        /// 保养大项目 xby
        /// </summary>
        public string PackageType { get; set; }

        /// <summary>
        /// 保养项目 jiyou
        /// </summary>
        public string BaoYangType { get; set; }

        /// <summary>
        /// 当前默认需要数量
        /// "OL-CA-XciHu-0W-20|1": 1
        /// </summary>
        public List<PidCountDto> PidCount { get; set; }

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
        public string UserId { get; set; }

        /// <summary>
        /// 渠道
        /// </summary>
        public string Channel { get; set; }

        /// <summary>
        /// 门店Id
        /// </summary>
        public int ShopId { get; set; }
    }
}
