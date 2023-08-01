using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.User.Service.Core.Model
{
    /// <summary>
    /// UserExpandShopVo
    /// </summary>
    public class UserExpandShopVo
    {
        /// <summary>
        /// UserId
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// 店长用户shopId
        /// </summary>
        public long RelationShopId { get; set; }

        /// <summary>
        /// 推荐店shopId
        /// </summary>
        public long RecommendShopId { get; set; }
    }
}
