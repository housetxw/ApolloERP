using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.ConsumerOrder.Service.Client.Model.Product
{
   public class GetShopProductByCodeDTO
    {
        public long Id { get; set; }

        public long ShopId { get; set; }

        public long CategoryId { get; set; }

        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public decimal StandardPrice { get; set; }

        public decimal SalesPrice { get; set; }

        public int SortType { get; set; }

        public int IsTop { get; set; }

        public int OnSale { get; set; }

        public string OnSaleTime { get; set; }

        public string Icon { get; set; }

        public string Remark { get; set; }

        public int IsDeleted { get; set; }

        public decimal DiscountRate { get; set; }
    }
}
