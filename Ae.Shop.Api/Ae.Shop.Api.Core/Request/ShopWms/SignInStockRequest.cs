using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Api.Core.Request
{
    public class SignInStockRequest
    {
        public List<string> PackageNos { get; set; } = new List<string>();

        /// <summary>
        /// 是否仅清点
        /// </summary>
        public bool IsOnlyInventory { get; set; } = false;

        public string UpdateBy { get; set; }

        public long ShopId { get; set; }
    }
}
