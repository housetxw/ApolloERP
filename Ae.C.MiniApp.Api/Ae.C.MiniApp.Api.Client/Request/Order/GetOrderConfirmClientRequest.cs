using Ae.C.MiniApp.Api.Client.Model.Order;
using Ae.C.MiniApp.Api.Client.Request;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ae.C.MiniApp.Api.Client.Request.Order
{
    public class GetOrderConfirmClientRequest : BaseUserClientRequest
    {
        /// <summary>
        /// 车辆ID
        /// </summary>
        //[Required(ErrorMessage = "请选择车辆")]
        public string CarId { get; set; }
        /// <summary>
        /// 门店ID
        /// </summary>
        public long ShopId { get; set; }
        /// <summary>
        /// 选择购买的产品信息集合
        /// </summary>
        [Required(ErrorMessage = "请选择商品")]
        [MinLength(1, ErrorMessage = "请至少选购一个商品")]
        public List<SelectedProductInfoDTO> ProductInfos { get; set; }

        public sbyte ProduceType { get; set; }
    }
}
