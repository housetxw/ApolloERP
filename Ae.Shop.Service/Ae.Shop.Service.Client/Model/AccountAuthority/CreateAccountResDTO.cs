using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Service.Client.Model
{
    public class CreateAccountResDTO
    {
        /// <summary>
        /// 账户ID
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 信息
        /// </summary>
        public string Message { get; set; }

        public string InitPassword { get; set; }

        /// <summary>
        /// 是否已存在账户
        /// </summary>
        public bool HasAccount { get; set; }
    }

    public class CheckAccountExistDTO
    {
        public string AccountId { get; set; }

        public string Message { get; set; }

        public bool HasAccount { get; set; }
    }

}
