using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.Shop.Service.Core.Request
{
    public class AddShopServiceRequest
    {
        /// <summary>
        /// 门店ID
        /// </summary>
        [Range(1, int.MaxValue, ErrorMessage = "门店ID必须大于0")]
        public long ShopId { get; set; }
        /// <summary>
        /// 基础服务ID
        /// </summary>
        [Range(1, int.MaxValue, ErrorMessage = "服务ID必须大于0")]
        public long BaseServiceId { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public string CreateBy { get; set; }
    }
}
