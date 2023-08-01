using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.BaoYang.Service.Core.Model.Config
{
    /// <summary>
    /// OeCodeMapVo
    /// </summary>
    public class OeCodeMapVo
    {
        /// <summary>
        /// 配件类型
        /// </summary>
        public string PartName { get; set; }

        /// <summary>
        /// OE件号
        /// </summary>
        public string OePartCode { get; set; }

        /// <summary>
        /// 配件号
        /// </summary>
        public string PartCode { get; set; }

        /// <summary>
        /// 车型品牌
        /// </summary>
        public string VehicleBrand { get; set; }
    }
}
