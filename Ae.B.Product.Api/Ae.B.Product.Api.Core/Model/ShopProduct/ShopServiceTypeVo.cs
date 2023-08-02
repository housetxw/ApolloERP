using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Product.Api.Core.Model.ShopProduct
{
    public class ShopServiceTypeVo
    {
        /// <summary>
        /// 项目ID
        /// </summary>
        public int ShopServiceTypeId { get; set; }
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
        /// 项目Id
        /// </summary>
        public long ServiceTypeId { get; set; }

        public string RouteUrl { get; set; }
    }
}
