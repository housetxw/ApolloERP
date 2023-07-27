using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.Shop.Service.Core.Request
{
    public class GetNoticeInfoRequest
    {
        /// <summary>
        /// 公告ID
        /// </summary>
        [Required(ErrorMessage = "公告ID不能为空")]
        public long Id { get; set; }
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
        /// <summary>
        /// 用户
        /// </summary>
        public string CreateBy { get; set; }
    }
}
