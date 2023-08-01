using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.BaoYang.Service.Core.Model
{
    /// <summary>
    /// BaoYangPartAccessoryVo
    /// </summary>
    public class BaoYangPartAccessoryVo
    {
        /// <summary>
        /// 五级车型tid
        /// </summary>
        public string Tid { get; set; }

        /// <summary>
        /// 辅料名称
        /// </summary>
        public string AccessoryName { get; set; }

        /// <summary>
        /// 升数
        /// </summary>
        public string Volume { get; set; } = string.Empty;

        /// <summary>
        /// 等级/防冻液沸点
        /// </summary>
        public string Level { get; set; } = string.Empty;

        /// <summary>
        /// 数量
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// 大小
        /// </summary>
        public string Size { get; set; } = string.Empty;

        /// <summary>
        /// 接口
        /// </summary>
        public string Interface { get; set; } = string.Empty;

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// 粘度
        /// </summary>
        public string Viscosity { get; set; } = string.Empty;

        /// <summary>
        /// 机油等级//防冻液冰点
        /// </summary>
        public string Grade { get; set; } = string.Empty;
    }
}
