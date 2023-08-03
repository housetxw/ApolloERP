using Ae.Receive.Service.Core.Model.Arrival;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Receive.Service.Core.Response.Arrival
{
    /// <summary>
    /// 快速排队返回对象
    /// </summary>
    public class QuickQueueResponse
    {
        /// <summary>
        /// 用户名称
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 用户手机号
        /// </summary>
        public string Telephone { get; set; }

        public string ShopName { get; set; }

        /// <summary>
        /// 车辆信息
        /// </summary>
        public VehicleVo Vehicle { get; set; }

        /// <summary>
        /// 服务选项
        /// </summary>
        public List<KeyValuePair<string, string>> ServiceTypeSelected { get; set; }


    }
}
