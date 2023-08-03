using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.Login.Api.Client.Request
{
    public class UserCouponReqDTO { }

    public class AddUserCouponReqForNewUserDTO
    {
        public string UserId { get; set; }

        public string UserIp { get; set; }
    }

}
