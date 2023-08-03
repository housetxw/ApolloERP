using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;

namespace Ae.C.MiniApp.Api.Core.Request.User
{
    /// <summary>
    /// 
    /// </summary>
    public class AddPersonalProductRequest
    {
        /// <summary>
        /// 商品pid
        /// </summary>
        [Required(ErrorMessage = "商品不能为空")]
        [MinLength(1, ErrorMessage = "商品不能为空")]
        public List<string> PidList { get; set; }
    }
}
