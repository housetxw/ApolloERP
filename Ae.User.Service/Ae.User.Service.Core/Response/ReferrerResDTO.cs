using Ae.User.Service.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.User.Service.Core.Response
{
    public class ShopReferrerCustomerResDTO : Statistics
    {
        public long ShopId { get; set; }

        public List<string> UserIds { get; set; } = new List<string>();
    }

    public class EmployeeReferrerCustomerResDTO : Statistics
    {
        public string EmployeeId { get; set; }

        public List<string> UserIds { get; set; } = new List<string>();
    }


}
