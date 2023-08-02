using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.Product.Service.Core.Request.Promotion
{
    /// <summary>
    /// 更新促销数量
    /// </summary>
    public class InsertPromotionActivityOrderRequest
    {
        /// <summary>
        /// 订单号
        /// </summary>
        [Required(ErrorMessage ="订单号不能为空")]
        public string OrderNo { get; set; }

        /// <summary>
        /// 下单用户Id
        /// </summary>
        [Required(ErrorMessage ="用户Id不能为空")]
        public string UserId { get; set; }

        /// <summary>
        /// 提交人
        /// </summary>
        [Required(ErrorMessage = "提交人不能为空")]
        public string SubmitBy { get; set; }

        /// <summary>
        /// 订单商品
        /// </summary>
        public List<PromotionProductRequest> Products { get; set; }
    }

    /// <summary>
    /// 商品信息
    /// </summary>
    public class PromotionProductRequest
    {
        /// <summary>
        /// 商品Pid
        /// </summary>
        public string ProductCode { get; set; }

        /// <summary>
        /// 商品数量
        /// </summary>
        public int Num { get; set; }

        /// <summary>
        /// 活动Id
        /// </summary>
        public string ActivityId { get; set; }
    }
}
