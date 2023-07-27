using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.Shop.Service.Core.Request
{
    public class GetTopNewNoticeRequest
    {
        /// <summary>
        /// 门店ID
        /// </summary>
        [Required(ErrorMessage = "门店ID不能为空")]
        public long ShopId { get; set; }
        /// <summary>
        /// 员工ID
        /// </summary>
        [Required(ErrorMessage = "员工ID不能为空")]
        public string UserId { get; set; }
    }
}
