using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.BaoYang.Service.Core.Response
{
    /// <summary>
    /// 配件类型返回实体
    /// </summary>
    public class GetPartNameResponse
    {
        /// <summary>
        /// 配件显示名
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// 品牌
        /// </summary>
        public List<string> Brands { get; set; }

        /// <summary>
        /// 配件号
        /// </summary>
        public List<string> PartNames { get; set; }

        /// <summary>
        /// 是否特殊配件
        /// </summary>
        public bool IsSpecialPart { get; set; }
    }
}
