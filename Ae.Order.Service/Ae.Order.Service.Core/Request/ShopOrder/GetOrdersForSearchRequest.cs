using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Ae.Order.Service.Core.Enums;


namespace Ae.Order.Service.Core.Request.ShopOrder
{
    /// <summary>
    /// 订单搜索的请求对象
    /// </summary>
    public class GetOrdersForSearchRequest: BasePageRequest
    {
        /// <summary>
        /// 门店ShopId
        /// </summary>
        [Required(ErrorMessage = "门店Id不能为空")]
        public int ShopId { get; set; }

        /// <summary>
        /// 搜索的类型
        /// </summary>
        [Required(ErrorMessage = "搜索类型不能为空")]
        public GetOrdersTypeEnum SearchType { get; set; }

        /// <summary>
        /// 内容。
        /// </summary>
        [Required(ErrorMessage = "搜索内容不能为空")]
        public string Content { get; set; }
    }
}
