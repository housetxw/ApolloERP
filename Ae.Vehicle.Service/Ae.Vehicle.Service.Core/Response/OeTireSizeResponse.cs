using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Vehicle.Service.Core.Response
{
    /// <summary>
    /// 原配轮胎尺寸
    /// </summary>
    public class OeTireSizeResponse
    {
        /// <summary>
        /// 前轮尺寸
        /// </summary>
        public string FrontTireSize { get; set; }

        /// <summary>
        /// 后轮尺寸
        /// </summary>
        public string RearTireSize { get; set; }
    }
}
