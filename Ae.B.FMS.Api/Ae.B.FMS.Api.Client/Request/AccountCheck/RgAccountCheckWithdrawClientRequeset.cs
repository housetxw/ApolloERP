using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.FMS.Api.Client.Request
{
   public class RgAccountCheckWithdrawClientRequeset
    {
        public List<string> OrderNos { get; set; }

        public int LocationId { get; set; }
        public string LocationName { get; set; }

        public string UpdateBy { get; set; }

        /// <summary>
        /// 提现状态
        /// </summary>
        public string WithdrawStatus { get; set; }
    }
}
