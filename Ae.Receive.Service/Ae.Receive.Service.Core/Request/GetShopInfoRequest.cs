using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.Receive.Service.Core.Request
{
    public class GetShopInfoRequest : BaseGetRequest
    {
        /// <summary>
        /// 门店ID
        /// </summary>
        [Required(ErrorMessage = "门店ID不能为空")]
        public int ShopId { get; set; }
        /// <summary>
        /// 门店名称
        /// </summary>
        public string ShopName { get; set; }
    }
}
