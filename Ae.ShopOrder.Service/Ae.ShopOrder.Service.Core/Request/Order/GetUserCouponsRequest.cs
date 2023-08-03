using Ae.ShopOrder.Service.Core.Model.Order;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.ShopOrder.Service.Core.Request.Order
{
    public class GetUserCouponsRequest : BasePageRequest
    {
        public string UserId { get; set; }

        public string CarId { get; set; }

        /// <summary>
        /// 选择的门店Id
        /// </summary>
        public long ShopId { get; set; }

        public List<SelectedProductInfoDTO> ProductInfos { get; set; }


        /// <summary>
        /// 产生类型（0普通产生 1购买核销码产生 2使用核销码产生 3再次购买产生 4追加下单产生）
        /// </summary>
        //[Range(0, 4, ErrorMessage = "产生类型范围错误")]
        public sbyte ProduceType { get; set; }

        /// <summary>
        /// 促销类型
        /// </summary>
        public string PromotionChannel { get; set; }
    }

  
}
