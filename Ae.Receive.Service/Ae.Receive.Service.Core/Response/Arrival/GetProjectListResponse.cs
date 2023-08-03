using Ae.Receive.Service.Core.Model.Arrival;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Receive.Service.Core.Response.Arrival
{
    /// <summary>
    /// 项目列表返回对象
    /// </summary>
    public class GetProjectListResponse
    {
        /// <summary>
        /// 大项目类目
        /// </summary>
        public string BigProjectType { get; set; }

        /// <summary>
        /// Icon
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 子项目
        /// </summary>
        public List<ProjectVo> ChildProjectItems { get; set; }
    }
}
