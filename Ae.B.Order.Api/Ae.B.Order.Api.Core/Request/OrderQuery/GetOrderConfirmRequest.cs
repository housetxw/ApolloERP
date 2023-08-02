using Ae.B.Order.Api.Core.Model.OrderDetail;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.B.Order.Api.Core.Request.OrderQuery
{
    public class GetOrderConfirmRequest
    {
        /// <summary>
        /// 车辆ID
        /// </summary>
        [Required(ErrorMessage = "请选择车辆")]
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
        public List<SelectedProductInfoVO> ProductInfos { get; set; }

        /// <summary>
        /// 产生类型（0普通产生 1购买核销码产生 2使用核销码产生 3再次购买产生 4追加下单产生 5 上门服务 6 会员卡）
        /// </summary>
        //[Range(0, 5, ErrorMessage = "产生类型范围错误")]
        public sbyte ProduceType { get; set; }

        /// <summary>
        /// 入口类型（0未设置 1轮胎 2保养 3美容）
        /// </summary>
       // [Required(ErrorMessage = "入口类型不能为空")]
        //  [Range(1, 3, ErrorMessage = "入口类型范围错误")]
        public sbyte OrderType { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        [Required(ErrorMessage = "用户ID不能为空")]
        public string UserId { get; set; }
    }
}
