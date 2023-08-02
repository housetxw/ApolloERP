using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Order.Api.Core.Response
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

        public string VerifyShopName { get; set; }

        public DateTime VerifyTime { get; set; }

        public string ProductId { get; set; }

        public string ProductName { get; set; }
    }

}
