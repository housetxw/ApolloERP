using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Order.Api.Core.Model.OrderDetail
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
    }
}
