using Ae.Order.Service.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;


namespace Ae.Order.Service.Core.Request.Order
{
    public class GetOrderForStatusNumRequest
    {
        /// <summary>
        /// 门店ShopId
        /// </summary>
        [Required(ErrorMessage = "门店Id不能为空")]
        public int ShopId { get; set; }

        ///// <summary>
        ///// 订单状态
        ///// </summary>
        //[Required(ErrorMessage = "业务状态不能为空")]
        //public OrderBusinessStatusEnum OrderBusinessStatus { get; set; }
    }
}
