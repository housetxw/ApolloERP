using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Service.Core.Request
{
    public class GetShopJoinListRequest : BasePageRequest
    {
        /// <summary>
        /// 手机号
        /// </summary>
        public string CreatorPhone { get; set; }

        public int Status { get; set; }
    }
}
