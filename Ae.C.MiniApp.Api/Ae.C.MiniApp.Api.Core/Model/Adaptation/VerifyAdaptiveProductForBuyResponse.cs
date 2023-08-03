using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.MiniApp.Api.Core.Model.Adaptation
{
    /// <summary>
    /// 
    /// </summary>
    public class VerifyAdaptiveProductForBuyResponse
    {
        /// <summary>
        /// 需要选择的五级属性
        /// </summary>
        public PropertySelectModel Property { get; set; }

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
