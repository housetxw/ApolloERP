using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Api.Core.Model
{
    public class ReceivePackageDTO
    {
        /// <summary>
        /// 订单号/铺货单号
        /// </summary>
        public string RelationNo { get; set; }

        public List<PackageProductDTO> PackageProducts { get; set; } = new List<PackageProductDTO>();
    }

    public class PackageProductDTO
    {
        public string PackageNo { get; set; }

        public List<ProductDTO> Products { get; set; } = new List<ProductDTO>();

    }

    public class ProductDTO
    {
        public string ProductId { get; set; }

        public string ProductName { get; set; }

        /// <summary>
        /// 应收数量
        /// </summary>
        public int Num { get; set; }

        /// <summary>
        /// 实收数量
        /// </summary>
        public int ActualNum { get; set; }

        public string Url { get; set; }
    }
}
