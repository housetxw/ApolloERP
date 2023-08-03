using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.FMS.Service.Core.Request
{
   public class RgAccountCheckConfirmRequest
    {
        public List<string> OrderNos { get; set; }

        public string RgCheckResult { get; set; }

        public int LocationId { get; set; }

        public string UpdateBy { get; set; }

        /// <summary>
        /// 提现状态
        /// </summary>
        public string WithdrawStatus { get; set; }



    }
}

  