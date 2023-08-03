using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Receive.Service.Client.Model.Order
{
    public class OrderAddressDto
    {
        public int OrderId { get; set; }

        public string Province { get; set; }

        public string City { get; set; }

        public string District { get; set; }

        public string DetailAddress { get; set; }
    }
}
