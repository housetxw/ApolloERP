using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Ae.AccountAuthority.Service.Common.Constant;
using Ae.AccountAuthority.Service.Core.Base.Request;
using Ae.AccountAuthority.Service.Core.Model;

namespace Ae.AccountAuthority.Service.Core.Request
{
    public class AccountReqDTO { }

    public class AccountCreateEntityDTO
    {
        [Required(ErrorMessage = "请输入登录账号")]
        public string Name { get; set; }

        public string Password { get; set; } = string.Empty;

        public string CreateBy { get; set; }

        public string CreateById { get; set; }
    }

    public class UpdateInfo
    {
        [Required(ErrorMessage = "请输入操作人名称")]
        public string UpdateBy { get; set; } = string.Empty;

        [Required(ErrorMessage = "请输入操作人Id")]
        public string UpdateById { get; set; } = string.Empty;
    }

    public class AccountUpdatePasswordEntityDTO : UpdateInfo
    {
        [Required(ErrorMessage = "请输入登录账号")]
        public string Name { get; set; }

        [Required(ErrorMessage = "请输入Password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "请输入新的Password")]
        //[MinLength(8, ErrorMessage = "密码长度至少是8位")]
        //[MaxLength(18, ErrorMessage = "密码长度不能超过18位")]
        public string NewPassword { get; set; }
    }

    public class AccountEntityReqByNameDTO
    {
        [Required(ErrorMessage = "请输入登录账号")]
        public string Name { get; set; }
    }

    public class AccountPageReqDTO : BaseOnlyPageRequest
    {
        public string Name { get; set; }

        public int IsDeleted { get; set; } = -1;
    }

    public class AccountListReqDTO
    {
        public List<string> Id { get; set; } = new List<string>();

        public string Name { get; set; }

        public int IsDeleted { get; set; } = -1;
    }

    public class AccountEntityReqDTO
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public int IsDeleted { get; set; } = -1;
    }

    public class AccountOptCommonReqDTO
    {
        [Required(ErrorMessage = "请输入账号Id"),
         RegularExpression(CommonConstant.PatternGuid, ErrorMessage = "账号Id格式不正确")]
        public string Id { get; set; }

        [Required(ErrorMessage = "请输入操作人名称")]
        public string UpdateBy { get; set; }
    }

    public class AccountUnlockReqByIdDTO : AccountOptCommonReqDTO { }

    public class AccountLockReqByIdDTO : AccountOptCommonReqByIdsDTO { }

    public class AccountDeleteReqByIdDTO : AccountOptCommonReqDTO { }

    public class AccountOptCommonReqByIdsDTO
    {
        [Required(ErrorMessage = "请输入账号Id")]
        [MinLength(1, ErrorMessage = "请输入至少一个账号")]
        [MaxLength(10, ErrorMessage = "一次最多只能禁用10个账号")]
        public List<string> Id { get; set; }

        [Required(ErrorMessage = "请输入操作人名称")]
        public string UpdateBy { get; set; }
    }

    public class AccountBatchDeleteReqByIdDTO : AccountOptCommonReqByIdsDTO { }

    public class AccountResetPasswordReqByIdDTO : AccountOptCommonReqDTO { }

    public class AccountResetPasswordReqByIdIntlDTO : AccountOptCommonReqDTO
    {
        public string Password { get; set; }

        public string PasswordSalt { get; set; }

        public int PasswordIteration { get; set; }
    }


}
