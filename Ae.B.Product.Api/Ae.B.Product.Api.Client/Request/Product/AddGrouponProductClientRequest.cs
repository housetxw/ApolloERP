using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Product.Api.Client.Request.Product
{
    public class AddGrouponProductClientRequest
    {
        /// <summary>
        /// 团购商品
        /// </summary>
        public List<GrouponProductClientRequest> GrouponProduct { get; set; }

        /// <summary>
        /// 提交人
        /// </summary>
        public string SubmitBy { get; set; }
    }

    /// <summary>
    /// GrouponProductRequest
    /// </summary>
    public class GrouponProductClientRequest
    {
        /// <summary>
        /// 产品pid
        /// </summary>
        public string Pid { get; set; }

        /// <summary>
        /// 最小价格
        /// </summary>
        public decimal MinPrice { get; set; }

        /// <summary>
        /// 最大价格
        /// </summary>
        public decimal MaxPrice { get; set; }
    }
}
