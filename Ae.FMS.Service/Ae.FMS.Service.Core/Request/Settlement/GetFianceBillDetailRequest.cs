using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Ae.FMS.Service.Core.Enums;

namespace Ae.FMS.Service.Core.Request.Settlement
{
    /// <summary>
    /// 财务账单详情详情
    /// </summary>
    public class GetFianceBillDetailRequest
    {
        /// <summary>
        /// 门店ID
        /// </summary>
        [Required]
        public  int ShopId { get; set; }
  

        /// <summary>
        /// 订单号
        /// </summary>
        [Required]
        public string OrderNo { get; set; }
    }
}
