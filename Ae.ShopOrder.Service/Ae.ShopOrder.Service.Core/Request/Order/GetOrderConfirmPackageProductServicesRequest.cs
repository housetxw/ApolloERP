using Ae.ShopOrder.Service.Core.Model.Order;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.ShopOrder.Service.Core.Request.Order
{
    public class GetOrderConfirmPackageProductServicesRequest
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// 车辆ID
        /// </summary>
        //[Required(ErrorMessage = "车辆ID不能为空")]
        public string CarId { get; set; }
        /// <summary>
        /// 选择购买的产品信息集合
        /// </summary>
        [Required(ErrorMessage = "选择购买的产品信息集合不能为空")]
        public List<SelectedProductInfoDTO> ProductInfos { get; set; }
        /// <summary>
        /// 省ID
        /// </summary>
        public string ProvinceId { get; set; }
        /// <summary>
        /// 城市ID
        /// </summary>
        public string CityId { get; set; }
    }
}
