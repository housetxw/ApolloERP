using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.FMS.Service.Client.Request
{
    /// <summary>
    /// 得到订单的产品请求
    /// </summary>
    public class GetOrderProductRequest
    {

        public List<string> OrderNos { get; set; }

    }
}
