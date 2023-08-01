using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.BaoYang.Service.Core.Request.Config
{
    /// <summary>
    /// SavePropertyAdaptationRequest
    /// </summary>
    public class SavePropertyAdaptationRequest
    {
        /// <summary>
        /// 主键
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Tid
        /// </summary>
        public string Tid { get; set; }

        /// <summary>
        /// 配件类型
        /// </summary>
        public string BaoYangType { get; set; }

        /// <summary>
        /// 五级属性
        /// </summary>
        public string Property { get; set; }

        /// <summary>
        /// 五级属性 值
        /// </summary>
        public string PropertyValue { get; set; }

        /// <summary>
        /// OE号
        /// </summary>
        public string OePartCode { get; set; }

        /// <summary>
        /// 配件号
        /// </summary>
        public string PartCode { get; set; }

        /// <summary>
        /// 图片Url
        /// </summary>
        public string ImageUrl { get; set; }

        /// <summary>
        /// 提交人
        /// </summary>
        public string SubmitBy { get; set; }
    }
}
