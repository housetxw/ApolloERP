using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Service.Client.Request
{
    public class AccountDeleteByIdRequest
    {
        /// <summary>
        /// 账户ID
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 修改人
        /// </summary>
        public string UpdateBy { get; set; }
    }
}
