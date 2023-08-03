using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Order.Service.Core.Response
{
    public class GetOrderPackageCardsResponse
    {
        public string UserPhone { get; set; }

        public string SourceOrderNo { get; set; }

        public string Remark { get; set; }

        public string Channel { get; set; }

        public string Code { get; set; }

        public string Status { get; set; }

        public DateTime StartValidTime { get; set; }

        public DateTime EndValidTime { get; set; }

        public string VerifyOrderNo { get; set; }

        public string VerifyShopName { get; set; } = string.Empty;

        public DateTime VerifyTime { get; set; }
        /// <summary>
        /// 套餐卡名称
        /// </summary>
        public string PackageName { get; set; } = string.Empty;

        public string ProductId { get; set; }

        public string ProductName { get; set; }
   }
}
