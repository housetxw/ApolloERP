using System;
using System.Collections.Generic;
using System.Text;
using Ae.Account.Api.Core.Model;

namespace Ae.Account.Api.Core.CommonModel
{
    public class Organization{}

    public class OrganizationSel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public RoleType Type { get; set; }

        /// <summary>
        /// 状态 0正常 1终止 2暂停
        /// </summary>
        public int Status { get; set; }
    }


}
