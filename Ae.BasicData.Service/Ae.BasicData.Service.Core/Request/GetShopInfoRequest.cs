using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Ae.BasicData.Service.Common.Http;

namespace Ae.BasicData.Service.Core.Request
{
    public class GetShopInfoRequest : BaseRequest
    {
        /// <summary>
        /// 门店ID
        /// </summary>
        [Required(ErrorMessage = "门店ID不能为空")]
        public int ShopId { get; set; }
        public string ShopName { get; set; }
    }
}
