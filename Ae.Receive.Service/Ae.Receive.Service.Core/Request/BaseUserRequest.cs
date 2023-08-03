using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Receive.Service.Core.Request
{
    public class BaseUserRequest
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public string UserId { get; set; }

        public long ShopId { get; set; }

        public string CarId { get; set; }
    }
}
