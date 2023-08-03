using Ae.C.MiniApp.Api.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.MiniApp.Api.Core.Response.Adaptation
{
    /// <summary>
    /// 轮胎服务接口
    /// </summary>
    public class GetTireServiceListResponse
    {
        /// <summary>
        /// 轮胎分类
        /// </summary>
        public List<TireCategoryVO> TireCategory { get; set; }

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
