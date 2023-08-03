using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;

namespace Ae.C.MiniApp.Api.Core.Request
{
  public  class DeleteUserAddressRequest
    {
        /// <summary>
        /// 地址Id
        /// </summary>
        [Required(ErrorMessage = "地址Id不能为空")]
        public long AddressId { get; set; }

        [IgnoreDataMember]
        public string UserId { get; set; }
    }
}
