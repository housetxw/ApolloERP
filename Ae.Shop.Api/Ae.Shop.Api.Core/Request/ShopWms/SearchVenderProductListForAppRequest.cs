using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Api.Core.Request
{
    public class SearchVenderProductListForAppRequest : BasePageRequest
    {
        /// <summary>
        /// 供应商Id
        /// </summary>
        public long VenderId { get; set; }
    }
}
