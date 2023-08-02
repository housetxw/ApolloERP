using Ae.B.Activity.Api.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Activity.Api.Core.Response
{
    public class GetShopInfoResponse
    {
        public int PKID { get; set; }
        public int ShopID { get; set; }
        public string Supplier { get; set; }
        /// <summary>
        /// 电话
        /// </summary>
        public string Tel { get; set; }
    }
}
