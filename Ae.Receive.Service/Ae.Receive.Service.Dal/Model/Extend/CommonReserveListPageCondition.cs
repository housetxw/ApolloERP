using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Receive.Service.Dal.Model.Extend
{
    public class CommonReserveListPageCondition
    {
        public int ShopId { get; set; }

        public string UserTel { get; set; }

        public string CarPlate { get; set; }

        public List<long> ReserveIds { get; set; }

        public string ReserveType { get; set; }

        public int ReserveChannel { get; set; }

        public int Status { get; set; }

        public DateTime? StarTime { get; set; }

        public DateTime? EndTime { get; set; }

        public bool ReserveTech { get; set; }

        public int PageIndex { get; set; }

        public int PageSize { get; set; }
    }
}
