using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.Shop.Service.Core.Request
{
    public class UpdateShopCarStatusRequest
    {
        /// <summary>
        /// 车辆ID
        /// </summary>
        [Range(1, int.MaxValue, ErrorMessage = "车辆ID必须大于0")]
        public long Id { get; set; }

        /// <summary>
        /// 状态 0正常 1禁用
        /// </summary>
        public sbyte Status { get; set; }
        /// <summary>
        /// 修改人
        /// </summary>
        public string UpdateBy { get; set; } = string.Empty;
    }
}
