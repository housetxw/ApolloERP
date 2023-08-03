using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Ae.Order.Service.Filters;

namespace Ae.Order.Service.Controllers
{
    /// <summary>
    /// 订单操作ForApp
    /// </summary>
    [Route("[controller]/[action]")]
   // [Filter(nameof(OrderCommandForAppController))]
    public class OrderCommandForAppController : Controller
    {
    }
}
