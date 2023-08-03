using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.MiniApp.Api.Core.Model
{
    /// <summary>
    /// 再次购买商品信息
    /// </summary>
    public class BuyAgainProductVO
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
