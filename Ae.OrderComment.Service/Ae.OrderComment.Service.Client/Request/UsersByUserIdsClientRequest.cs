using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.OrderComment.Service.Client.Request
{
    public class UsersByUserIdsClientRequest
    {
        /// <summary>
        /// 用户Id  最大限制100
        /// </summary>
        public List<string> UserIds { get; set; }
    }
}
