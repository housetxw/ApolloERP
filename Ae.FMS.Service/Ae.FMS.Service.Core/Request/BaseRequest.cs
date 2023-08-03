using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.FMS.Service.Core.Request
{
    public class BaseRequest
    {
        public string Channel { get; set; }

        public string DeviceId { get; set; }
        public string ApiVersion { get; set; }
    }

    public class BaseEntityPostRequest<T> : BaseRequest
    {
        public T Data { get; set; }
    }
}
