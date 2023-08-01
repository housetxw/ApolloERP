using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.BaoYang.Service.Core.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class BaoYangPartModel
    {
        /// <summary>
        /// 车型TID
        /// </summary>
        public string Tid { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// 保养类型
        /// </summary>
        public string BaoYangType { get; set; }

        /// <summary>
        /// 产品P
        /// </summary>
        public List<PartProduct> Pids { get; set; }

        /// <summary>
        /// 五级属性适配
        /// </summary>
        public List<PropertyAdaptive> Properties { get; set; }
    }

    /// <summary>
    /// 五级属性
    /// </summary>
    public class PropertyAdaptive
    {
        /// <summary>
        /// 属性
        /// </summary>
        public string Property { get; set; }

        /// <summary>
        /// 属性值
        /// </summary>
        public string PropertyValue { get; set; }

        /// <summary>
        /// 图片地址
        /// </summary>
        public string ImageUrl { get; set; }

        /// <summary>
        /// 产品
        /// </summary>
        public List<PartProduct> Products { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class PartProduct
    {
        /// <summary>
        /// 
        /// </summary>
        public string Pid { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Brand { get; set; }
    }

    /// <summary>
    /// BaoYangProductPriorityModel
    /// </summary>
    public class BaoYangProductPriorityModel
    {
        /// <summary>
        /// 保养项目
        /// </summary>
        public string BaoYangType { get; set; }

        /// <summary>
        /// 优先级字段
        /// </summary>
        public string PriorityField { get; set; }

        /// <summary>
        /// 优先级value
        /// </summary>
        public string PriorityValue { get; set; }

        /// <summary>
        /// 优先级：1>2>3
        /// </summary>
        public int Priority { get; set; }
    }


    /// <summary>
    /// 保养辅料Model
    /// </summary>
    public class BaoYangAccessoryModel
    {
        /// <summary>
        /// 车型TID
        /// </summary>
        public string Tid { get; set; }

        /// <summary>
        /// 保养类型
        /// </summary>
        public string BaoYangType { get; set; }

        /// <summary>
        /// 辅料名称(变速箱油)
        /// </summary>
        public string AccessoryName { get; set; }

        /// <summary>
        /// 量(6)
        /// </summary>
        public string Volume { get; set; }

        /// <summary>
        /// 等级(HX7)
        /// </summary>
        public string Level { get; set; }

        /// <summary>
        /// 数量(2)
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// 尺寸(20)
        /// </summary>
        public string Size { get; set; }

        /// <summary>
        /// 接口类型(通用接口)
        /// </summary>
        public string Interface { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 粘度
        /// </summary>
        public string Viscosity { get; set; }

        /// <summary>
        /// 全合成，半合成
        /// </summary>
        public string Grade { get; set; }
    }
}
