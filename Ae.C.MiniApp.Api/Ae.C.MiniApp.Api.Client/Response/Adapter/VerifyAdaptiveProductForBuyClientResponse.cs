using Ae.C.MiniApp.Api.Client.Model.BaoYang;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.MiniApp.Api.Client.Response.Adapter
{
    public class VerifyAdaptiveProductForBuyClientResponse
    {
        /// <summary>
        /// 需要选择的五级属性
        /// </summary>
        public PropertySelectDto Property { get; set; }

        /// <summary>
        /// 是否适配 0 不适配  1适配  2不确定是否适配
        /// </summary>
        public int IsAdaptive { get; set; }

        /// <summary>
        /// 提示文案
        /// </summary>
        public string Tip { get; set; }
    }
}
