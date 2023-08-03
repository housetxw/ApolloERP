using Ae.Shop.Api.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Api.Core.Request
{
    public class CheckInStockRequest
    {
        public long ShopId { get; set; }
        public string Operator { get; set; }

        public List<PackageProductInfo> PackageProducts { get; set; } = new List<PackageProductInfo>();
    }

    /// <summary>
    /// 记录包裹下每个产品的收货数量
    /// </summary>
    public class PackageProductInfo
    {
        public string PackageNo { get; set; }

        public List<ProductInfo> Products { get; set; } = new List<ProductInfo>();
    }

    public class ProductInfo
    {
        public string ProductId { get; set; }

        /// <summary>
        /// 实收数量(不包含良品)
        /// </summary>
        public int ActualNum { get; set; }

        /// <summary>
        /// 是否货损
        /// </summary>
        public bool IsDamage { get; set; }

        /// <summary>
        /// 货损数量
        /// </summary>
        public int DamageNum { get; set; }

        /// <summary>
        /// 货损描述
        /// </summary>
        public string DamageRemark { get; set; } = string.Empty;

        public List<string> FileList { get; set; } = new List<string>();

    }
}
