using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Activity.Api.Core.Request
{
   public class UpdateInstallGuideStatusRequest
    {
        /// <summary>
        /// 1.删除 2.上架 3.下架
        /// </summary>
        public int Type { get; set; }

        public List<long> InstallIds { get; set; }

        public string UpdateBy { get; set; }
    }
}
