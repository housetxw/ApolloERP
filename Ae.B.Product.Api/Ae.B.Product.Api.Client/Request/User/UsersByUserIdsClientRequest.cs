using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Product.Api.Client.Request.User
{
    public class UsersByUserIdsClientRequest
    {
        /// <summary>
        /// 用户Id  最大限制100
        /// </summary>
        public List<string> UserIds { get; set; }
    }
}
