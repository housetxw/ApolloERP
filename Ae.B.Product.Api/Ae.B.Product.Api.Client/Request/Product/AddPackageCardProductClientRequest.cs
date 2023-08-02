using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Product.Api.Client.Request.Product
{
    public class AddPackageCardProductClientRequest
    {
        /// <summary>
        /// 产品
        /// </summary>
        public List<PackageCardProductClientReq> Products { get; set; }

        /// <summary>
        /// 提交人
        /// </summary>
        public string SubmitBy { get; set; }
    }

    /// <summary>
    /// PackageCardProductClientReq
    /// </summary>
    public class PackageCardProductClientReq
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
