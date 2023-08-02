using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Activity.Api.Core.Request
{
    public class BaseEntityPostRequest<T> : BaseGetRequest
    {
        public T Data { get; set; }
    }
}
