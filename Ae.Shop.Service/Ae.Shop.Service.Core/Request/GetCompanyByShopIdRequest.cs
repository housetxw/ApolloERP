using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.Shop.Service.Core.Request
{
    public class GetCompanyByShopIdRequest
    {
        /// <summary>
        /// 公司id
        /// </summary>
        [Required(ErrorMessage = "公司ID不能为空")]
        public long ShopId { get; set; }
    }
}
