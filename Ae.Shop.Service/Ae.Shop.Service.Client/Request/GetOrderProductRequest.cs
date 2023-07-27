using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Service.Client.Request
{

    /// <summary>
    /// 得到订单的产品请求
    /// </summary>
    public class GetOrderProductRequest : BaseGetRequest
    {
      
        public List<string> OrderNos { get; set; }

    }
}
