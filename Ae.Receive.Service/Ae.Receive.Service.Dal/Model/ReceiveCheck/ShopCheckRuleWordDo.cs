using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Receive.Service.Dal.Model.ReceiveCheck
{
    public class ShopCheckRuleWordDo
    {
        public long SubItemId { get; set; }

        public string Suggestion { get; set; }

        public long RuleId { get; set; }

        public int ResultWordId { get; set; }
    }
}
