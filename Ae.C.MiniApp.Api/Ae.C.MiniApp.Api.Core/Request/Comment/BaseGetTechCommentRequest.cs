using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.MiniApp.Api.Core.Request
{
    public class BaseGetTechCommentRequest : BaseGetRequest
    {
        /// <summary>
        /// 技师员工Id
        /// </summary>
        public string EmployeeId { get; set; }
    }
}
