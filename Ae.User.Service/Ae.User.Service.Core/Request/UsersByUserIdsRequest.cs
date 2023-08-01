using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.User.Service.Core.Request
{
    /// <summary>
    /// userId批量查询用户信息
    /// </summary>
    public class UsersByUserIdsRequest
    {
        /// <summary>
        /// 用户Id  最大限制100
        /// </summary>
        [MaxLength(100, ErrorMessage = "批量查询最大限制100")]
        public List<string> UserIds { get; set; }
    }
}
