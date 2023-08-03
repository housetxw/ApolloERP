using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.ConsumerOrder.Service.Core.Model
{
    /// <summary>
    /// 再次购买商品信息
    /// </summary>
    public class BuyAgainProductDTO
    {
        /// <summary>
        /// 产品Id
        /// </summary>
        public string Pid { get; set; }
        /// <summary>
        /// 购买数量
        /// </summary>
        public int Number { get; set; }
    }
}
