using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Api.Client.Request.Reserve
{
    public class ValidReserveClientReserve
    {
        /// <summary>
        /// 用户手机号
        /// </summary>
        public string UserTel { get; set; }

        /// <summary>
        /// 门店Id
        /// </summary>
        public int ShopId { get; set; }
    }
}
