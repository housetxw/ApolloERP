using Ae.C.MiniApp.Api.Core.Request.Adaptation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.C.MiniApp.Api.Core.Request.ShopProduct
{
    /// <summary>
    /// 商品详情页Request （自营商品、门店服务项目）
    /// </summary>
    public class ProductListByServiceTypeRequest
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
        //[Required(ErrorMessage = "车型不能为空")]
        public VehicleRequest Vehicle { get; set; }

        /// <summary>
        /// shopId
        /// </summary>
        public int ShopId { get; set; }
    }
}
