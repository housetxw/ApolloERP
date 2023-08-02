using Ae.Product.Service.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.Product.Service.Core.Request.Product
{
    /// <summary>
    /// AddHotProductRequest
    /// </summary>
    public class AddHotProductRequest
    {
        /// <summary>
        /// 终端类型：ShopApp(B端APP)/CbJApplet(B端小程序)/YcJApplet(C端小程序)/QpJApple(其他小程序)
        /// </summary>
        public TerminalType TerminalType { get; set; }

        /// <summary>
        /// 产品
        /// </summary>
        public List<HotProductReq> Products { get; set; }

        /// <summary>
        /// 提交人
        /// </summary>
        [Required(ErrorMessage = "提交人不能为空")]
        public string SubmitBy { get; set; }
    }

    /// <summary>
    /// HotProductRequest
    /// </summary>
    public class HotProductReq
    {
        /// <summary>
        /// 产品pid
        /// </summary>
        public string Pid { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Rank { get; set; }
    }
}
