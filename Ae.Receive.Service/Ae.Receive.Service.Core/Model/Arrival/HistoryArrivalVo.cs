using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Receive.Service.Core.Model.Arrival
{
    /// <summary>
    /// 历史到店记录
    /// </summary>
    public class HistoryArrivalVo
    {
        /// <summary>
        /// 到店时间
        /// </summary>
        public string ShowArrivalTime { get; set; }

        /// <summary>
        /// 服务类型
        /// </summary>
        public string ServiceType { get; set; }

        /// <summary>
        /// 车No
        /// </summary>
        public string CarNo { get; set; }

        /// <summary>
        /// 项目列表
        /// </summary>
        public List<ProjectItemVo> ProjectItems { get; set; }

        /// <summary>
        /// 到店Id
        /// </summary>
        public long RecId { get; set; }

        /// <summary>
        /// 接车技师
        /// </summary>
        public string TechName { get; set; }
    }
}
