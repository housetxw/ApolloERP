using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Ae.C.MiniApp.Api.Common.Constant;
using Ae.C.MiniApp.Api.Core.Enums;
using Ae.C.MiniApp.Api.Core.Model.Coupon;

namespace Ae.C.MiniApp.Api.Core.Request.Coupon
{
    public class UserCouponReqVO { }
    
    public class UserCouponPageReqByUserIdVO : BaseOnlyPageRequest
    {
        [Required(ErrorMessage = "请输入用户Id"),
         RegularExpression(CommonConstant.PatternGuid, ErrorMessage = "用户Id格式不正确")]
        public string UserId { get; set; }

        /// <summary>
        /// 订单可用优惠券UserCouponId集合
        /// ！！！字段用途说明！！！
        /// 仅当UserCouponStatus(Status)为0时，此字段才有用，且目前只能用于订单确认页的入参中
        /// </summary>
        public List<long> UserCouponId { get; set; } = new List<long>();

        [Required(ErrorMessage = "请输入优惠券列表入口类型")]
        [Range(0, 1, ErrorMessage = "请输入合适的优惠券列表入口类型")]
        public CouponListEntranceType EntranceType { get; set; }
    }

    public class ExchangeCouponReqByCodeVO
    {
        [Required(ErrorMessage = "请输入用户Id"),
         RegularExpression(CommonConstant.PatternGuid, ErrorMessage = "用户Id格式不正确")]
        public string UserId { get; set; }

        [Required(ErrorMessage = "请输入用户优惠券兑换码")]
        public string Code { get; set; }
    }

    public class IntegralExchangeCouponReqVO
    {
        [Required(ErrorMessage = "请输入用户Id"),
         RegularExpression(CommonConstant.PatternGuid, ErrorMessage = "用户Id格式不正确")]
        public string UserId { get; set; }

        [Required(ErrorMessage = "请输入优惠券活动Id")]
        [Range(1, long.MaxValue, ErrorMessage = "优惠券活动Id必须大于0")]
        public long CouponActivityId { get; set; }
    }



}
