using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;

namespace Ae.C.MiniApp.Api.Core.Request
{
   public class CreateUserAddressTagRequest
    {
        [IgnoreDataMember]
        public string UserId { get; set; }

        /// <summary>
        /// 标签
        /// </summary>
        [Required(ErrorMessage = "标签不能为空")]
        public string TagName { get; set; }
    }
}
