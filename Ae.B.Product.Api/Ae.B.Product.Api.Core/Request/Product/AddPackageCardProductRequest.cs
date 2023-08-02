using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Product.Api.Core.Request.Product
{
    public class AddPackageCardProductRequest
    {
        /// <summary>
        /// 产品
        /// </summary>
        public List<PackageCardProductReq> Products { get; set; }
    }

    /// <summary>
    /// PackageCardProductReq
    /// </summary>
    public class PackageCardProductReq
    {
        /// <summary>
        /// 产品pid
        /// </summary>
        public string Pid { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Rank { get; set; }

        /// <summary>
        /// 套餐卡类型
        /// </summary>
        public sbyte Type { get; set; }
    }
}
