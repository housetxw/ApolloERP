using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Activity.Api.Core.Request
{
  public  class UpdatePromoteStatusRequest
    {
        public List<long> Ids { get; set; }

        public string UpdateBy { get; set; }

        /// <summary>
        /// 1 上架   2 下架  
        /// </summary>
        public int Type { get; set; }
    }
}
