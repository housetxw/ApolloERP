using Ae.C.MiniApp.Api.Client.Model;
using System;
using System.ComponentModel.DataAnnotations;

namespace Ae.C.MiniApp.Api.Client.Request
{
    /// <summary>
    /// 用户关联门店Request
    /// </summary>
    public class AddShopUserRelationRequest : ReferrerInfoDTO
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
    }
}
