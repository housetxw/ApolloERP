using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.MiniApp.Api.Core.Model
{
    /// <summary>
    /// 服务简单信息
    /// </summary>
    public class SimpleServiceVO
    {
        /// <summary>
        /// 服务ID
        /// </summary>
        public long ServiceId { get; set; }
        /// <summary>
        /// 服务ICON图片
        /// </summary>
        public string ServiceIcon { get; set; }
        /// <summary>
        /// 服务名称
        /// </summary>
        public string ServiceName { get; set; }
        /// <summary>
        /// 服务CODE
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 适配名称
        /// </summary>
        public string FitName { get; set; }
        /// <summary>
        /// 适配URL
        /// </summary>
        public string FitUrl { get; set; }
    }
}
