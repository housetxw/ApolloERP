using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Ae.BaoYang.Service.Core.Model;

namespace Ae.BaoYang.Service.Core.Request
{
    /// <summary>
    /// 轮胎适配列表Request
    /// </summary>
    public class TireListRequest
    {
        /// <summary>
        /// 分类类型
        /// </summary>
        [Required(ErrorMessage = "CategoryType不能为空")]
        public string CategoryType { get; set; }

        /// <summary>
        /// 省Id
        /// </summary>
        public string ProvinceId { get; set; }

        /// <summary>
        /// 市Id
        /// </summary>
        public string CityId { get; set; }

        /// <summary>
        /// 车系Id
        /// </summary>
        [Required(ErrorMessage = "车系不能为空")]
        public string VehicleId { get; set; }

        /// <summary>
        /// 当前商品信息
        /// </summary>
        public List<PidCountVo> PidCount { get; set; }

        /// <summary>
        /// 五级车型Tid
        /// </summary>
        public string Tid { get; set; }

        /// <summary>
        /// 轮胎尺寸
        /// </summary>
        [Required(ErrorMessage = "轮胎尺寸不能为空")]
        public string TireSize { get; set; }

        /// <summary>
        /// 用户Id
        /// </summary>
        public string UserId { get; set; }
    }
}
