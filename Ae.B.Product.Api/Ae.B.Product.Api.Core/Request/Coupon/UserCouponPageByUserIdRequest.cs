using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Ae.B.Product.Api.Common.Constant;

namespace Ae.B.Product.Api.Core.Request.Coupon
{
    /// <summary>
    /// 
    /// </summary>
    public class UserCouponPageByUserIdRequest
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        [Required(ErrorMessage = "请输入用户Id")]
        public string UserId { get; set; }

        /// <summary>
        /// 订单可用优惠券UserCouponId集合
        /// ！！！字段用途说明！！！
        /// 仅当UserCouponStatus(Status)为0时，此字段才有用，且目前只能用于订单确认页的入参中
        /// </summary>
        public List<long> UserCouponId { get; set; } = new List<long>();

        /// <summary>
        /// 入口
        /// </summary>
        [Required(ErrorMessage = "请输入优惠券列表入口类型")]
        [Range(0, 1, ErrorMessage = "请输入合适的优惠券列表入口类型")]
        public CouponListEntranceType EntranceType { get; set; }

        /// <summary>
        /// 页码
        /// </summary>
        [Range(1, int.MaxValue, ErrorMessage = "页码必须大于0")]
        public int PageIndex { get; set; } = 1;

        /// <summary>
        /// 页大小
        /// </summary>
        [Range(1, int.MaxValue, ErrorMessage = "页大小必须大于0")]
        public int PageSize { get; set; } = 20;
    }

    /// <summary>
    /// 优惠券入口
    /// </summary>
    public enum CouponListEntranceType
    {
        /// <summary>
        /// 订单确认页
        /// </summary>
        [Description("订单确认页")] Order = 0,

        /// <summary>
        /// 我的页面
        /// </summary>
        [Description("我的页面")] Mine = 1
    }
}
