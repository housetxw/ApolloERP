using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.Order.Service.Core.Request.Order
{
    public class GetOrderListForMiniAppRequest : BasePageRequest
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        [Required(ErrorMessage = "UserId不能为空")]
        public string UserId { get; set; }
        /// <summary>
        /// 订单类型（0未设置 1轮胎 2保养 3美容 4 钣金维修 5 汽车改装 6 其他）
        /// </summary>
        public sbyte OrderType { get; set; }
        /// <summary>
        /// 订单列表状态（0全部 1待付款 2待收货 3待安装 4待评价 5售后/退货）
        /// </summary>
        public sbyte OrderListStatus { get; set; }
        /// <summary>
        /// 小程序类型（0未设置 1C端小程序 2B端小程序）
        /// </summary>
        public sbyte MiniAppType { get; set; }
    }
}
