using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Ae.Account.Api.Common.Constant;

namespace Ae.Account.Api.Core.Request
{
    public class AccountReqVO { }

    public class AccountOptCommonReqVO
    {
        [Required(ErrorMessage = "请输入账号Id"),
         RegularExpression(CommonConstant.PatternGuid, ErrorMessage = "账号Id格式不正确")]
        public string Id { get; set; }

        public string UpdateBy { get; set; }
    }

    public class AccountOptCommonReqByIdsVO
    {
        [Required(ErrorMessage = "请输入账号Id")]
        [MinLength(1, ErrorMessage = "请输入至少一个账号")]
        [MaxLength(10, ErrorMessage = "一次最多只能禁用10个账号")]
        public List<string> Id { get; set; }

        public string UpdateBy { get; set; }
    }

    public class AccountUnlockReqByIdVO : AccountOptCommonReqVO { }

    public class AccountLockReqByIdVO : AccountOptCommonReqByIdsVO { }

    public class AccountBatchDeleteReqByIdVO : AccountOptCommonReqByIdsVO { }

    public class AccountResetPasswordReqByIdVO : AccountOptCommonReqVO { }



}
