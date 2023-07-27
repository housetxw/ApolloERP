using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.Shop.Service.Core.Request
{
    public class GetShopListForServiceRequest : BaseOnlyPageRequest
    {
        /// <summary>
        /// 地址
        /// </summary>
        [Required(ErrorMessage = "地址不能为空")]
        public string Address { get; set; }
        /// <summary>
        /// 门店类型  0所有  4上门养护
        /// </summary>
        public int ShopType { get; set; }
        /// <summary>
        /// 门店状态 -1所有 0正常 1终止 2暂停
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// 服务ProductId
        /// </summary>
        public List<string> ServicePId { get; set; }
        /// <summary>
        /// 经度
        /// </summary>
        public string Longitude { get; set; }
        /// <summary>
        /// 维度
        /// </summary>
        public string Latitude { get; set; }
        /// <summary>
        /// 区Id
        /// </summary>
        public string DistrictId { get; set; }
    }
}
