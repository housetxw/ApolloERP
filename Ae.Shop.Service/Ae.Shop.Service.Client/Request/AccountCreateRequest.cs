using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.Shop.Service.Client.Request
{
    public class AccountCreateRequest
    {
        /// <summary>
        /// 用户名称
        /// </summary>
        public string Name { get; set; }

        public string Password { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public string CreateBy { get; set; }

        /// <summary>
        /// 创建人ID
        /// </summary>
        public string CreateById { get; set; }
    }

    public class AccountEntityReqByNameDTO
    {
        [Required(ErrorMessage = "请输入登录账号")]
        public string Name { get; set; }
    }

}
