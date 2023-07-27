using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Service.Client.Request
{
    public class ReceiveByIdsClientRequest
    {
        /// <summary>
        /// 到店记录Id
        /// </summary>
        public List<long> ArriveIdList { get; set; }
    }
}
