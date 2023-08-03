using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Order.Service.Core.Request
{
    public class GetOrderPackageCardsRequest:BasePageRequest
    {
        public string UserId { get; set; }

        public string UserPhone { get; set; }

        public string SourceOrderNo { get; set; }

        public string ProductName { get; set; }
        /// <summary>
        /// 状态（0未使用 1已使用 2已过期）-1未使用含过期
        /// </summary>
        public int Status { get; set; }

        public string Code { get; set; }

    }

}
