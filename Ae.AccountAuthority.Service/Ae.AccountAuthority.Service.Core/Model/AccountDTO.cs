using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Ae.AccountAuthority.Service.Core.Model
{
    public class AccountDTO
    {
        public string Id { get; set; }

        /// <summary>
        /// 账号；目前仅支持手机号登录
        /// </summary>
        public string Name { get; set; }

        public string Password { get; set; }

        public string PasswordSalt { get; set; }

        public int PasswordIteration { get; set; }

        public AccountState State { get; set; }

        public int ErrorCount { get; set; }

        public bool IsDeleted { get; set; }

        public string CreateBy { get; set; }

        public DateTime CreateTime { get; set; } = DateTime.Now;

        public string UpdateBy { get; set; }

        public DateTime UpdateTime { get; set; } = DateTime.Now;
    }

    public enum AccountState
    {
        None = -1,

        [Description("正常")]
        Normal = 0,

        [Description("异常")]
        Exception = 1,

        [Description("锁定")]
        Lock = 2
    }

    public class AccountPageDTO : AccountDTO { }


}
