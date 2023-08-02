using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Product.Api.Client.Model.BaoYang
{
    /// <summary>
    /// 辅料配置数据
    /// </summary>
    public class BaoYangPartAccessoryDto
    {
        /// <summary>
        /// 五级车型Tid
        /// </summary>
        public string Tid { get; set; }

        /// <summary>
        /// 配置详情
        /// </summary>
        public List<PartAccessoryDto> Details { get; set; }
    }

    public class PartAccessoryDto
    {
        /// <summary>
        /// 辅料类型
        /// </summary>
        public string AccessoryName { get; set; }

        /// <summary>
        /// 容量
        /// </summary>
        public string Volume { get; set; }

        /// <summary>
        /// 级别
        /// </summary>
        public string Level { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// 接口
        /// </summary>
        public string Interface { get; set; }

        /// <summary>
        /// 规格
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 大小
        /// </summary>
        public string Size { get; set; }

        /// <summary>
        /// 粘度
        /// </summary>
        public string Viscosity { get; set; }

        public string Grade { get; set; }
    }
}
