using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Activity.Api.Client.Response
{
    public class ShopInfoDTO
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
