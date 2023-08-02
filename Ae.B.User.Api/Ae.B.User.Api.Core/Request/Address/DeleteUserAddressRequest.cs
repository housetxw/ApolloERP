using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.B.User.Api.Core.Request.Address
{
    /// <summary>
    /// 
    /// </summary>
    public class DeleteUserAddressRequest
    {
        /// <summary>
        /// 地址Id
        /// </summary>
        [Required(ErrorMessage = "地址Id不能为空")]
        public long AddressId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required(ErrorMessage = "用户Id不能为空")]
        public string UserId { get; set; }
    }
}
