using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.OrderComment.Service.Core.Request
{
    /// <summary>
    /// 门店评分Request
    /// </summary>
    public class ShopScoreRequest
    {
        /// <summary>
        /// 门店Id
        /// </summary>
        public List<int> ShopIdList { get; set; }
    }
}
