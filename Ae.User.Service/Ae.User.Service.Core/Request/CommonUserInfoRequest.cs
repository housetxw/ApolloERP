using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.User.Service.Core.Request
{
    /// <summary>
    /// 通用用户查询
    /// </summary>
    public class CommonUserInfoRequest
    {
        /// <summary>
        /// 用户列表数组
        /// </summary>
        [MaxLength(50,ErrorMessage = "UserId最大查询不能超过50")]
        public List<string> UserIdList { get; set; }

        /// <summary>
        /// 姓名或手机号搜索
        /// </summary>
        public string SearchContent { get; set; }

        /// <summary>
        /// 员工Id
        /// </summary>
        public string EmployeeId { get; set; }

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
