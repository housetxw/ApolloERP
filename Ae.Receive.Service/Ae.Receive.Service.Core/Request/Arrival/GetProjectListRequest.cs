using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Receive.Service.Core.Request.Arrival
{
    /// <summary>
    /// 得到项目列表
    /// </summary>
    public class GetProjectListRequest : BaseGetRequest
    {
        /// <summary>
        /// ShopId
        /// </summary>
        public long ShopId { get; set; }
        /// <summary>
        /// 项目名称
        /// </summary>
        public string ProjectName { get; set; }
    }
}
