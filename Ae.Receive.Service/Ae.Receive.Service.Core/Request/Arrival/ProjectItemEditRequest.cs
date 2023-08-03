using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Receive.Service.Core.Request.Arrival
{
    public class ProjectItemEditRequest : BaseGetRequest
    {
        /// <summary>
        /// 到店记录Id
        /// </summary>
        public string ArrivalId { get; set; }

        /// <summary>
        /// 项目Id
        /// </summary>
        public string ProjectId { get; set; }
    }
}
