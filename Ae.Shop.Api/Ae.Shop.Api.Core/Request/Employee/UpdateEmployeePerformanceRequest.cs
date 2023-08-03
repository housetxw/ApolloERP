using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.Shop.Api.Core.Request
{
    public class UpdateEmployeePerformanceRequest
    {
        //[Required(ErrorMessage = "门店ID不能为空")]
        public long ShopId { get; set; } = 0;

        /// <summary>
        /// 订单号
        /// </summary>
        public List<string> OrderNo { get; set; } 

        /// <summary>
        ///  订单日期
        /// </summary>
        //[Required(ErrorMessage = "日期不能为空")]
        public string OrderDate { get; set; }

        /// <summary>
        /// 修改人
        /// </summary>
        public string UpdateBy { get; set; } = string.Empty;
    }

}
