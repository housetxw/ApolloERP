using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Service.Client.Model
{
   public class VenderDTO
    {
        /// <summary>
        /// 主键
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 供应商简称
        /// </summary>
        public string VenderShortName { get; set; } = string.Empty;
    }
}
