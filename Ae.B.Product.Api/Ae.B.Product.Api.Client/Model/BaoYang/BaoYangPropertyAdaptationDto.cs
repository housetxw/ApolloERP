using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Product.Api.Client.Model.BaoYang
{
    /// <summary>
    /// 五级属性适配列表
    /// </summary>
    public class BaoYangPropertyAdaptationDto
    {
        /// <summary>
        ///
        /// 五级车型tid
        /// </summary>
        public string Tid { get; set; }

        /// <summary>
        /// OE件号
        /// </summary>
        public string OeCode { get; set; }

        /// <summary>
        /// 零件号
        /// </summary>
        public string PartCode { get; set; }

        /// <summary>
        /// 属性
        /// </summary>
        public string Property { get; set; }

        /// <summary>
        /// 属性值
        /// </summary>
        public string PropertyValue { get; set; }

        /// <summary>
        /// 属性图片地址
        /// </summary>
        public string ImageUrl { get; set; }

        /// <summary>
        /// 主键
        /// </summary>
        public int Id { get; set; }
    }
}
