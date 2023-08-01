using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.User.Service.Dal.Model.Request
{
    public class SearchUserInfoCondition
    {
        public string UserName { get; set; }

        public string UserTel { get; set; }

        public int PageIndex { get; set; }

        public int PageSize { get; set; }
    }
}
