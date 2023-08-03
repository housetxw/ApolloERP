using System;
using System.Collections.Generic;
using System.Text;
using ApolloErp.Data.DapperExtensions.Att;
using MySqlX.XDevAPI.Relational;

namespace Ae.C.Login.Api.Dal.Model
{
    public class UserInfoDO
    {

        public int ThirdId { get; set; }
        /// <summary>
        /// 用户id
        /// </summary>
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string NickName { get; set; }
        public string Email { get; set; }
        public string MobileNumber { get; set; }
        public string HeadUrl { get; set; }
        public string OpenId { get; set; }
        public int State { get; set; }
      
    }
}
