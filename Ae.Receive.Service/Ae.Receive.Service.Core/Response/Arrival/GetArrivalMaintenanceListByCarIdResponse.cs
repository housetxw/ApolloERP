using Ae.Receive.Service.Core.Model.Arrival;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Receive.Service.Core.Response.Arrival
{
    public class GetArrivalMaintenanceListByCarIdResponse
    {
        public List<GetArrivalMaintenanceListByCarIdVo> ManitenanceHead { get; set; }

        public List<ArrivalMaintenanceProjectVo> Items { get; set; }
    }
}
