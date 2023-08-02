using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.B.Activity.Api.Client.Request
{
    public class GetShopInfoClientRequest
    {
        /// <summary>
        /// 门店ID
        /// </summary>
        [Required(ErrorMessage = "门店ID不能为空")]
        public int ShopId { get; set; }
        public string ShopName { get; set; }
    }
}
