using Ae.Product.Service.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Product.Service.Core.Model.Config
{
    /// <summary>
    /// ConfigHotProductVo
    /// </summary>
    public class ConfigHotProductVo : ProductBaseInfoVo
    {
        /// <summary>
        /// 终端类型
        /// </summary>
        public TerminalType TerminalType { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Rank { get; set; }

        /// <summary>
        /// 热门产品主键
        /// </summary>
        public long HotProductId { get; set; }
    }
}
