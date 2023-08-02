using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.Product.Service.Core.Request.Config
{
    /// <summary>
    /// QueryConfigAdvertisementRequest
    /// </summary>
    public class QueryConfigAdvertisementRequest
    {
        /// <summary>
        /// 终端类型：ShopApp(B端APP)/CbJApplet(B端小程序)/YcJApplet(C端小程序)/QpJApple(其他小程序)
        /// </summary>
        [Required(ErrorMessage = "终端类型不能为空")]
        public string TerminalType { get; set; }
    }
}
