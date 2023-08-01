using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.User.Service.Core.Request
{
    /// <summary>
    /// 搜索用户
    /// </summary>
    public class SearchUserInfoRequest : BasePageRequest
    {
        /// <summary>
        /// 客户姓名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 客户手机号
        /// </summary>
        public string UserTel { get; set; }
    }
}
