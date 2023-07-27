using System;
using System.Collections.Generic;
using System.Text;
using Ae.Account.Api.Client.Model;

namespace Ae.Account.Api.Client.Response
{
    public class AccountResDTO { }

    public class AccountKeyInfoEntityDTO
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public AccountState State { get; set; }

        public bool IsDeleted { get; set; }
    }

    public class AccountKeyInfoListDTO
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

    public class AccountResetPasswordResByIdDTO
    {
        public bool flag { get; set; }

        public string ResetPassword { get; set; }

        public string Message { get; set; }
    }



}
