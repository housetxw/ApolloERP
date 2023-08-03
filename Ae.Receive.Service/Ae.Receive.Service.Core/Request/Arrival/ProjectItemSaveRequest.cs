using Ae.Receive.Service.Core.Model.Arrival;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Receive.Service.Core.Request.Arrival
{
    /// <summary>
    /// 项目保存请求
    /// </summary>
    public class ProjectItemSaveRequest
    {
        /// <summary>
        /// ShopId
        /// </summary>
        public long ShopId { get; set; }

        /// <summary>
        /// UserId
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// CarId
        /// </summary>
        public string CarId { get; set; }
        /// <summary>
        /// 到店记录Id
        /// </summary>
        public string ArrivalId { get; set; }

        /// <summary>
        /// 添加的项目Items
        /// </summary>
        public List<ProjectItemVo> Items { get; set; }
    }
}
