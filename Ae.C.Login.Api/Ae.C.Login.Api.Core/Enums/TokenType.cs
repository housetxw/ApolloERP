using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.C.Login.Api.Core.Enums
{
    public enum TokenType
    {
        /// <summary>
        /// 访问token
        /// </summary>
        [Display(Name = "访问token")]
        AccessToken = 1,
        /// <summary>
        /// 刷新token
        /// </summary>
        [Display(Name = "刷新token")]
        RefreshToken = 2
    }
}
