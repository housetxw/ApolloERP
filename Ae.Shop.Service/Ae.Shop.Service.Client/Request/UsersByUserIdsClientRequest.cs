using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Service.Client.Request
{
    /// <summary>
    /// UsersByUserIdsClientRequest
    /// </summary>
    public class UsersByUserIdsClientRequest
    {
        /// <summary>
        /// 用户Id  最大限制100
        /// </summary>
        public List<string> UserIds { get; set; }
    }
}
