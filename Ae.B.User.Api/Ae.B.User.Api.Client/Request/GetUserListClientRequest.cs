using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.User.Api.Client.Request
{
    public class GetUserListClientRequest
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// 用户手机号
        /// </summary>
        public string UserTel { get; set; }

        /// <summary>
        /// 用户姓名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 页码
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// 步长
        /// </summary>
        public int PageSize { get; set; }
    }
}
