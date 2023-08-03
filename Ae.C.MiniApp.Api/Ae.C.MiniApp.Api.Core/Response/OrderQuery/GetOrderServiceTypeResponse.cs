using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.MiniApp.Api.Core.Response.OrderQuery
{
    public class GetOrderServiceTypeResponse
    {
        //public string Code { get; set; }

        //public string Text { get; set; }

        //public bool IsOptional { get; set; }

        public bool DoorToDoor { get; set; }

        public bool ArrivalToShop { get; set; }
    }
}
