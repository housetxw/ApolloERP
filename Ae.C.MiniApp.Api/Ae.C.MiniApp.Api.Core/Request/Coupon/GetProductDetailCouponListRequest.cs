using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.MiniApp.Api.Core.Request
{
    /// <summary>
    /// 根据产品Id获取可用优惠券信息列表请求参数
    /// </summary>
    public class GetProductDetailCouponListRequest : BaseGetRequest
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// 门店Id
        /// </summary>
        public long ShopId { get; set; }
        /// <summary>
        /// 产品编号
        /// </summary>
        public string Pid { get; set; }
    }
}
