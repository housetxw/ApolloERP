using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.MiniApp.Api.Core.Response.Adaptation
{
    /// <summary>
    /// 验证商品是否适配Response
    /// </summary>
    public class VerifyAdaptiveProductResponse
    {
        /// <summary>
        /// 0 不适配  1适配  2不确定是否适配
        /// </summary>
        public int Result { get; set; }

        /// <summary>
        /// 提示文案
        /// </summary>
        public string Tip { get; set; }
    }
}
