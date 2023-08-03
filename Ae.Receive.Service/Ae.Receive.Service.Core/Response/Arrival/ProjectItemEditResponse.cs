using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Receive.Service.Core.Response.Arrival
{
    public class ProjectItemEditResponse
    {
        /// <summary>
        /// 项目Id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 价格
        /// </summary>
        public string Price { get; set; }
    }
}
