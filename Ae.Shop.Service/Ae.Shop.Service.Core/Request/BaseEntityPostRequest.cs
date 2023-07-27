using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Service.Core.Request
{
    public class BaseEntityPostRequest<T> : BaseGetRequest
    {
        public T Data { get; set; }
    }
}
