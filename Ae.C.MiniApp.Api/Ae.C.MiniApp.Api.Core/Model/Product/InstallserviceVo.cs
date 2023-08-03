using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.MiniApp.Api.Core.Model.Product
{
   public class InstallserviceVo
    {  
        /// <summary>
        /// 服务id
        /// </summary>
        public string ServiceId { get; set; } = string.Empty;
        /// <summary>
        /// 服务名称
        /// </summary>
        public string ServiceName { get; set; } = string.Empty;
        /// <summary>
        /// 服务价格
        /// </summary>
        public decimal ServicePrice { get; set; }
        /// <summary>
        /// 顺序
        /// </summary>
        public int Sort { get; set; }
     
    }
}
