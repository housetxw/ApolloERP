using Ae.C.MiniApp.Api.Core.Model.Adaptation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;

namespace Ae.C.MiniApp.Api.Core.Request.Adaptation
{
    /// <summary>
    /// 轮胎适配列表Request
    /// </summary>
    public class GetTireListRequest
    {
        /// <summary>
        /// 分类类型
        /// </summary>
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
        public string TireSize { get; set; }

        /// <summary>
        /// 用户Id
        /// </summary>
        [IgnoreDataMember]
        public string UserId { get; set; }
    }
}
