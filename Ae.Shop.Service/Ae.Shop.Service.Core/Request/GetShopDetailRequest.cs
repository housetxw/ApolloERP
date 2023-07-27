using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.Shop.Service.Core.Request
{
    public class GetShopDetailRequest
    {
        /// <summary>
        /// 门店ID
        /// </summary>
        [Range(1, int.MaxValue, ErrorMessage = "门店ID必须大于0")]
        public long ShopId { get; set; }
        /// <summary>
        /// 经度
        /// </summary>
        public double Longitude { get; set; }
        /// <summary>
        /// 维度
        /// </summary>
        public double Latitude { get; set; }
        /// <summary>
        /// 门店审核状态（0全部 1待审核 2已通过审核 3未通过审核）
        /// </summary>
        public int CheckStatus { get; set; }
    }
}
