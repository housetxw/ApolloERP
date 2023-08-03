using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.MiniApp.Api.Core.Model
{
    /// <summary>
    /// 选择购买的产品信息
    /// </summary>
    public class SelectedProductInfoVO
    {
        /// <summary>
        /// 产品编号
        /// </summary>
        public string Pid { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public int Number { get; set; }

        /// <summary>
        /// 商品的自营属性(0:自营，1：shop)
        /// </summary>
        public int ProductOwnAttri { get; set; }
    }
}
