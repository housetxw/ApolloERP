using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.BaoYang.Service.Core.Request
{
    /// <summary>
    /// 保养适配配置查询请求实体
    /// </summary>
    public class GetBaoYangPartAdaptationsRequest
    {
        /// <summary>
        /// 款型Id
        /// </summary>
        public List<string> TidList { get; set; }
    }
}
