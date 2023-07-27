using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Ae.Account.Api.Common.Constant;
using Ae.Account.Api.Common.Http;

namespace Ae.Account.Api.Client.Request
{
    public class AccountReqDTO { }

    public class AccountListReqDTO
    {
        public List<string> Id { get; set; }

        public string Name { get; set; }

        public int IsDeleted { get; set; } = -1;
    }

    public class AccountPageReqDTO : BaseOnlyPageRequest
    {
        public string Name { get; set; }

        public int IsDeleted { get; set; } = -1;
    }

    public class AccountOptCommonReqDTO
    {
        public string Id { get; set; }

        public string UpdateBy { get; set; }
    }

    public class AccountOptCommonReqByIdsDTO
    {
        public List<string> Id { get; set; }

        public string UpdateBy { get; set; }
    }

    public class AccountUnlockReqByIdDTO : AccountOptCommonReqDTO { }

    public class AccountLockReqByIdDTO : AccountOptCommonReqByIdsDTO { }

    public class AccountBatchDeleteReqByIdDTO : AccountOptCommonReqByIdsDTO { }

    public class AccountResetPasswordReqByIdDTO : AccountOptCommonReqDTO { }

    public class AccountUpdatePasswordEntityDTO
    {
        [Required(ErrorMessage = "请输入登录账号")]
        public string Name { get; set; }

        [Required(ErrorMessage = "请输入Password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "请输入新的Password")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "请输入操作人名称")]
        public string UpdateBy { get; set; } = string.Empty;

        [Required(ErrorMessage = "请输入操作人Id")]
        public string UpdateById { get; set; } = string.Empty;
    }

}
