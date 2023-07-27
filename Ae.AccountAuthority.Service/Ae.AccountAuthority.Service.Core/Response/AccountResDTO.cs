using System;
using System.Collections.Generic;
using System.Text;
using Ae.AccountAuthority.Service.Core.Model;

namespace Ae.AccountAuthority.Service.Core.Response
{
    public class AccountResDTO { }

    public class AccountPageResDTO
    {
        public string Id { get; set; }

        /// <summary>
        /// 账号；目前仅支持手机号登录
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 状态是否正常；如账号正常，账号异常，密码锁定等等状态
        /// </summary>
        public AccountState State { get; set; }

        /// <summary>
        /// 密码错误次数统计；每次登陆成功此字段更新为0
        /// </summary>
        public int ErrorCount { get; set; }

        public DateTime PasswordUpdateTime { get; set; } = new DateTime(1900, 1, 1);

        public bool IsDeleted { get; set; }

        public string CreateBy { get; set; }

        public DateTime CreateTime { get; set; } = DateTime.Now;

        public string UpdateBy { get; set; }

        public DateTime UpdateTime { get; set; } = DateTime.Now;
    }

    public class AccountKeyInfoEntityResDTO
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public AccountState State { get; set; }

        public int ErrorCount { get; set; }

        public bool IsDeleted { get; set; }
    }

    public class AccountKeyInfoWithPwdEntityResDTO
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Password { get; set; }

        public string PasswordSalt { get; set; }

        public int PasswordIteration { get; set; }

        public AccountState State { get; set; }

        public int ErrorCount { get; set; }

        public bool IsDeleted { get; set; }
    }

    public class AccountKeyInfoListResDTO
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public AccountState State { get; set; }

        public int ErrorCount { get; set; }

        public bool IsDeleted { get; set; }

        public string CreateBy { get; set; }

        public DateTime CreateTime { get; set; }

        public string UpdateBy { get; set; }

        public DateTime UpdateTime { get; set; }
    }

    public class CreateAccountResDTO
    {
        public string Id { get; set; }

        public string Message { get; set; }

        public string InitPassword { get; set; }

        public bool HasAccount { get; set; }
    }

    public class CheckAccountExistDTO
    {
        public string AccountId { get; set; }

        public string Message { get; set; }

        public bool HasAccount { get; set; }
    }

    public class AccountResetPasswordResByIdDTO
    {
        public bool flag { get; set; }

        public string ResetPassword { get; set; }

        public string Message { get; set; }
    }


}
