using Ae.C.Login.Api.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.C.Login.Api.Core.Request
{
    /// <summary>
    /// 获取微信开放id
    /// </summary>
    public class GetWxOpenIdRequest
    {
        /// <summary>
        /// 微信CODE
        /// </summary>
        [Required(ErrorMessage = "微信code不能为空")]
        public string WxCode { get; set; }
        /// <summary>
        /// 加密数据
        /// </summary>
        public string EncryptedData { get; set; }
        /// <summary>
        /// 加密算法的初始向量
        /// </summary>
        public string Iv { get; set; }

        /// <summary>
        /// 登录平台
        /// </summary>
        [Required(ErrorMessage = "登录平台不能为空")]
        public int PlatForm { get; set; }
    }
}
