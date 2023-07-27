using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.Shop.Service.Core.Request.ShopCustomer
{
    /// <summary>
    /// 编辑会员标签
    /// </summary>
    public class EditMemberTagRequest
    {
        /// <summary>
        /// 门店Id
        /// </summary>
        [Range(1, Int32.MaxValue, ErrorMessage = "ShopId必须大于0")]
        public long ShopId { get; set; }

        /// <summary>
        /// 用户Id
        /// </summary>
        [Required(ErrorMessage = "UserId不能为空")]
        public string UserId { get; set; }

        /// <summary>
        /// 提交人
        /// </summary>
        [Required(ErrorMessage = "提交人不能为空")]
        public string SubmitBy { get; set; }

        /// <summary>
        /// 会员标签值
        /// </summary>
        public int MemberTag { get; set; }
    }
}
