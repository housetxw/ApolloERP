using Ae.Shop.Api.Client.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Api.Client.Request
{
    public class CreateAsnRequest    {
        public AsnDTO Asn { get; set; }

        public List<AsnProductDTO> Products { get; set; } = new List<AsnProductDTO>();

    }
}
