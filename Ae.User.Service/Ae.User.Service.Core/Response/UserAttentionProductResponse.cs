using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.User.Service.Core.Response
{
    /// <summary>
    /// 用户关注商品
    /// </summary>
    public class UserAttentionProductResponse
    {
        /// <summary>
        /// 商品Pid
        /// </summary>
        public List<string> PidList { get; set; }
    }
}
