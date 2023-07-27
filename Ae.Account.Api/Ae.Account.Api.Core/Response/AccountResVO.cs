using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.Account.Api.Core.Response
{
    public class AccountResVO { }

    public class AccountResetPasswordResByIdVO
    {
        public bool flag { get; set; }

        public string ResetPassword { get; set; }

        public string Message { get; set; }
    }

    public class AccountUpdatePasswordEntityVO
    {
        [Required(ErrorMessage = "请输入登录账号")]
        public string Name { get; set; }

        [Required(ErrorMessage = "请输入Password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "请输入新的Password")]
        public string NewPassword { get; set; }

        public string UpdateBy { get; set; } = string.Empty;
    }


}
