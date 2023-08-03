using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.MiniApp.Api.Core.Request
{
    public class BaseGetShopCommentRequest : BaseGetRequest
    {
        /// <summary>
        /// 门店Id
        /// </summary>
        public long ShopId { get; set; }
    }
}
