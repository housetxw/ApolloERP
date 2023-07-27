using System;
using System.Collections.Generic;
using System.Text;
using Ae.Shop.Service.Core.Model;

namespace Ae.Shop.Service.Core.Response.ShopServer
{
    public class JobResDTO { }

    public class JobListResDTO : JobListDTO { }

    public class JobSelDTO
    {
        public long Value { get; set; }
        public string Label { get; set; }

        public List<JobSelDTO> Children { get; set; }
    }

}
