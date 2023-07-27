using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Service.Core.Model.ShopCustomer
{
    /// <summary>
    /// ShopCustomerQpVo
    /// </summary>
    public class ShopCustomerQpVo
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// 客户姓名
        /// </summary>
        public string UserName { get; set; } = string.Empty;

        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName { get; set; } = string.Empty;

        /// <summary>
        /// 客户手机
        /// </summary>
        public string UserTel { get; set; } = string.Empty;

        /// <summary>
        /// 最后下单时间
        /// </summary>
        public string LastOrderTime { get; set; } = string.Empty;

        /// <summary>
        /// 头像
        /// </summary>
        public string HeadUrl { get; set; }
    }
}
