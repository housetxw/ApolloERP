using Ae.ConsumerOrder.Service.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.ConsumerOrder.Service.Client.Request
{
    public class OrderApplicableCouponListReqDTO
    {
        public string UserId { get; set; }

        public long ShopId { get; set; }

        public int ShopType { get; set; }

        public ShopRegion ShopRegion { get; set; }

        public PayMethod PayMethod { get; set; }

        public decimal TotalAmount { get; set; }

        public List<Product> ProductList { get; set; }
    }

    public class ShopRegion
    {
        public string ProvinceId { get; set; }
        public string CityId { get; set; }
        public string DistrictId { get; set; }
    }

    public class Product
    {
        public string Pid { get; set; }
        public string Category { get; set; }
        public string Brand { get; set; }
        public int Total { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
