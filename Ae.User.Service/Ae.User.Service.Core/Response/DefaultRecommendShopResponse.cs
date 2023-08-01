using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.User.Service.Core.Response
{
    /// <summary>
    /// DefaultRecommendShopResponse
    /// </summary>
    public class DefaultRecommendShopResponse
    {
        /// <summary>
        /// 当前用户是否店长
        /// </summary>
        public int IsShopManager { get; set; }

        /// <summary>
        /// 默认门店Id
        /// </summary>
        public long ShopId { get; set; }

        /// <summary>
        /// 默认店长昵称
        /// </summary>
        public string ShopManagerName { get; set; }

        /// <summary>
        /// 默认店长头像
        /// </summary>
        public string HeadUrl { get; set; }
    }
}
