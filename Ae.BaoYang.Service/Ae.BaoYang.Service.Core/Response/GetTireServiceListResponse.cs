using Ae.BaoYang.Service.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.BaoYang.Service.Core.Response
{
    /// <summary>
    /// 轮胎服务接口
    /// </summary>
    public class TireServiceListResponse
    {
        /// <summary>
        /// 轮胎分类
        /// </summary>
        public List<TireCategoryVo> TireCategory { get; set; }

        /// <summary>
        /// 默认轮胎尺寸
        /// </summary>
        public string DefaultTireSize { get; set; }

        /// <summary>
        /// 更多轮胎尺寸
        /// </summary>
        public List<string> TireSizes { get; set; }
    }
}
