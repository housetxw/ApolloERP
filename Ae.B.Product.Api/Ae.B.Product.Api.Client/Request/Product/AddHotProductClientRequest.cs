using Ae.B.Product.Api.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Product.Api.Client.Request.Product
{
    public class AddHotProductClientRequest
    {
        /// <summary>
        /// 终端类型：ShopApp(B端APP)/CbJApplet(B端小程序)/YcJApplet(C端小程序)/QpJApple(其他小程序)
        /// </summary>
        public TerminalType TerminalType { get; set; }

        /// <summary>
        /// 产品
        /// </summary>
        public List<HotProductClientReq> Products { get; set; }

        /// <summary>
        /// 提交人
        /// </summary>
        public string SubmitBy { get; set; }
    }

    /// <summary>
    /// HotProductRequest
    /// </summary>
    public class HotProductClientReq
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
