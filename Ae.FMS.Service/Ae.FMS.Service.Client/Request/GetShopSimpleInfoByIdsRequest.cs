using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.FMS.Service.Client.Request
{
   public class GetShopSimpleInfoByIdsRequest
    { /// <summary>
      /// 门店ID
      /// </summary>
        public List<long> ShopIds { get; set; }
    }
}
