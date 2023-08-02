using Ae.B.Order.Api.Core.Request.OrderCommand;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.B.Order.Api.Core.Request.OrderQuery
{
    public class TrialCalcOrderAmountRequest
    {
        /// <summary>
        /// 入口类型（0未设置 1轮胎 2保养 3美容）
        /// </summary>
        //[Range(1, 3, ErrorMessage = "入口类型范围错误")]
        public sbyte OrderType { get; set; }
        /// <summary>
        /// 产生类型（0普通产生 1购买核销码产生 2使用核销码产生 3再次购买产生 4追加下单产生  5 上门服务）
        /// </summary>
        //[Range(0, 4, ErrorMessage = "产生类型范围错误")]
        public sbyte ProduceType { get; set; }
        /// <summary>
        /// 下单人用户Id
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// 下单车辆Id
        /// </summary>
        public string CarId { get; set; }
        /// <summary>
        /// 选择的门店Id
        /// </summary>
        public long ShopId { get; set; }

        /// <summary>
        /// 选择购买的产品信息集合
        /// </summary>
        [Required(ErrorMessage = "请选择商品")]
        [MinLength(1, ErrorMessage = "请至少选购一个商品")]
        public List<SelectedProductInfoDTO> ProductInfos { get; set; }

        /// <summary>
        /// 选择使用的用户优惠券Id（0未选择）
        /// </summary>
        public long UserCouponId { get; set; }

    }
}
