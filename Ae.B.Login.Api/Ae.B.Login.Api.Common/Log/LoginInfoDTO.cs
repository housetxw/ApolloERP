using System;
using System.Collections.Generic;
using System.Text;
using Ae.B.Login.Api.Common.Http;

namespace Ae.B.Login.Api.Common.Log
{
    public class LoginInfoDTO : BaseRequest
    {
        public long Id { get; set; }

        public string LoginName { get; set; }

        public string OS { get; set; }

        public string IpAddress { get; set; }

        public string LoginLocation { get; set; }

        public string Browser { get; set; }

        public string MachineName { get; set; }

        public DateTime LoginTime { get; set; } = DateTime.Now;
    }


}
