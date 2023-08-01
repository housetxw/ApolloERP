using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.BaoYang.Service.Core.Model.Config
{
    /// <summary>
    /// 辅料配置
    /// </summary>
    public class BaoYangAccessoryConfig
    {
        /// <summary>
        /// 辅料配置
        /// </summary>
        public List<BaoYangAccessory> PartAccessories { get; set; }
    }

    /// <summary>
    /// 辅料Item
    /// </summary>
    public class BaoYangAccessory
    {
        /// <summary>
        /// 类型名称
        /// </summary>
        public string PartName { get; set; }

        /// <summary>
        /// 需要配置的属性
        /// </summary>
        public List<Item> Display { get; set; }

        /// <summary>
        /// 级别
        /// </summary>
        public string Level { get; set; }

        /// <summary>
        /// 粘度级别
        /// </summary>
        public string Viscosity { get; set; }

        /// <summary>
        /// 接口
        /// </summary>
        public string Interface { get; set; }

        /// <summary>
        /// 规格
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 容量
        /// </summary>
        public Standard Volume { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public Standard Quantity { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Grade { get; set; }
    }

    /// <summary>
    /// 配置属性
    /// </summary>
    public class Item
    {
        /// <summary>
        /// Key
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// Value
        /// </summary>
        public string Value { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class Standard
    {
        /// <summary>
        /// 最小
        /// </summary>
        public int MinValue { get; set; }

        /// <summary>
        /// 最大
        /// </summary>
        public int MaxValue { get; set; }

        /// <summary>
        /// 单位
        /// </summary>
        public string Unit { get; set; }
    }
}
