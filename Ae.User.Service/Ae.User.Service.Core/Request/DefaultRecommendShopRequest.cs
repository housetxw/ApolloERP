using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.User.Service.Core.Request
{
    /// <summary>
    /// DefaultRecommendShopRequest
    /// </summary>
    public class DefaultRecommendShopRequest
    {
        /// <summary>
        /// 当前用户Id
        /// </summary>
        [Required(ErrorMessage = "用户Id不能为空")]
        public string UserId { get; set; }

        /// <summary>
        /// 门店Id - 通过分享的带shopId
        /// </summary>
        public long ShopId { get; set; }
    }
}
