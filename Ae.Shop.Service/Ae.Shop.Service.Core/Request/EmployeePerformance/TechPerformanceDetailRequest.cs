using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Service.Core.Request
{
    public class TechPerformanceDetailRequest : TechPerformanceRequest
    {
        /// <summary>
        /// 项目
        /// </summary>
        public string ItemName { get; set; }

    }
}
