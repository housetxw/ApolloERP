using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Service.Core.Model
{
    public class ShopServiceTypeDTO
    {
        /// <summary>
        /// 门店ID
        /// </summary>
        public long ShopId { get; set; }
        /// <summary>
        /// 项目ID
        /// </summary>
        public long ShopServiceTypeId { get; set; }
        /// <summary>
        /// ID
        /// </summary>
        public long ServiceTypeId { get; set; }
        /// <summary>
        /// 项目图标
        /// </summary>
        public string ImageUrl { get; set; }
        /// <summary>
        /// 项目描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 项目名称
        /// </summary>
        public string DisplayName { get; set; }
        /// <summary>
        /// 项目CODE
        /// </summary>
        public string ServiceType { get; set; }
        /// <summary>
        /// 跳转链接
        /// </summary>
        public string RouteUrl { get; set; }


        /// <summary>
        /// 订单类型（历史原因 产品大类与订单类型设置的不一致）
        /// </summary>
        public long OrderType { get; set; }
    }
}
