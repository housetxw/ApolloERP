using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.Shop.Service.Core.Request
{
    public class ModifyShopServiceRequest
    {
        /// <summary>
        /// 门店服务ID
        /// </summary>
        [Range(1, int.MaxValue, ErrorMessage = "门店服务ID必须大于0")]
        public long Id { get; set; }
        /// <summary>
        /// 成本价
        /// </summary>
        public decimal CostPrice { get; set; }
        /// <summary>
        /// 销售价
        /// </summary>
        public decimal SalePrice { get; set; }
        /// <summary>
        /// 上下架状态 0下架  1上架
        /// </summary>
        public sbyte Status { get; set; }
        /// <summary>
        /// 修改人
        /// </summary>
        public string UpdateBy { get; set; }
    }
}
