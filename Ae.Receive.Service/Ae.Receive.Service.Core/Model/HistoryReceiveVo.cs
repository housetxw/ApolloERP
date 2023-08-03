using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Receive.Service.Core.Model
{
    /// <summary>
    /// 历史到店Model
    /// </summary>
    public class HistoryReceiveVo
    {
        /// <summary>
        /// 到店Id
        /// </summary>
        public long ReceiveId { get; set; }

        /// <summary>
        /// 服务类型
        /// </summary>
        public string ServiceType { get; set; }

        /// <summary>
        /// 车牌
        /// </summary>
        public string CarPlate { get; set; }

        /// <summary>
        /// 到店时间
        /// </summary>
        public string ArrivalTime { get; set; }

        /// <summary>
        /// 服务项目
        /// </summary>
        public List<ReserveProject> Projects { get; set; }
    }
}
