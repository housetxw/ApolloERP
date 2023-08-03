using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.OrderComment.Service.Core.Request
{
    public class BaseGetProductCommentRequest
    {
        /// <summary>
        /// 产品Id
        /// </summary>
        [Required(ErrorMessage = "产品Id不能为空")]
        public string ProductId { get; set; }
    }
}
