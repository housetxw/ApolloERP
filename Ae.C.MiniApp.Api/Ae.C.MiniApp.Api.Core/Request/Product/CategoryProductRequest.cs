using Ae.C.MiniApp.Api.Core.Request.Adaptation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Ae.C.MiniApp.Api.Core.Request.Product
{
    public class CategoryProductRequest
    {
        /// <summary>
        /// 类目Id
        /// </summary>
        [Description("类目Id")]
        public int CategoryId { get; set; }

        /// <summary>
        /// 门店Id
        /// </summary>
        public int? ShopId { get; set; }

        /// <summary>
        /// 车型
        /// </summary>
        public VehicleRequest Vehicle { get; set; }
    }
}
