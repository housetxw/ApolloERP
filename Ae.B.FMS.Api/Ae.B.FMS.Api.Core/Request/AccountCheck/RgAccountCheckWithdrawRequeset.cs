using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.FMS.Api.Core.Request
{
   public class RgAccountCheckWithdrawRequeset
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
