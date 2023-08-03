using Ae.Receive.Service.Core.Model.Arrival;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Receive.Service.Core.Response.Arrival
{
    /// <summary>
    /// 到店记录返回对象
    /// </summary>
    public class PayResponse
    {
        /// <summary>
        /// 项目列表
        /// </summary>
        public List<ProjectItemVo> ProjectItems { get; set; }

        /// <summary>
        /// 总价格
        /// </summary>
        public string SumPrice { get; set; }
    }
}
