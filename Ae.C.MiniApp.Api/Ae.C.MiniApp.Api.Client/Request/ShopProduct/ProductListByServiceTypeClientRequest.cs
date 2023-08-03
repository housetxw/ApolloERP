using Ae.C.MiniApp.Api.Client.Request.BaoYang;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.MiniApp.Api.Client.Request.ShopProduct
{
    public class ProductListByServiceTypeClientRequest
    {
        /// <summary>
        /// 搜索关键字
        /// </summary>
        public string SearchContent { get; set; }

        /// <summary>
        ///  分类ID
        /// </summary>
        public int CategoryId { get; set; }

        /// <summary>
        /// 车型
        /// </summary>
        public VehicleClientRequest Vehicle { get; set; }

        /// <summary>
        /// 用户Id
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// shopId
        /// </summary>
        public int ShopId { get; set; }

        /// <summary>
        /// 渠道：MiniApp,ShopApp
        /// </summary>
        public string Channel { get; set; }
    }
}
