using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.ShopOrder.Service.Core.Model.Order
{
    /// <summary>
    /// 操作类型
    /// </summary>
    public class HandleModeListVO
    {
        /// <summary>
        /// Key
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Code
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 是否选中
        /// </summary>
        public bool Check { get; set; }
    }
}
