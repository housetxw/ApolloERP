using Ae.C.MiniApp.Api.Client.Model.BaoYang;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.MiniApp.Api.Client.Response.BaoYang
{
    public class TireServiceListClientResponse
    {
        /// <summary>
        /// 轮胎分类
        /// </summary>
        public List<TireCategoryDto> TireCategory { get; set; }

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
