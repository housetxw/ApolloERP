using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Product.Api.Client.Request.BaoYang
{
    public class UpdateAccessoryClientRequest
    {
        /// <summary>
        /// 五级车型Tid
        /// </summary>
        public string Tid { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        public string AccessoryName { get; set; }

        /// <summary>
        /// 属性名
        /// </summary>
        public string AttributeName { get; set; }

        /// <summary>
        /// 属性值
        /// </summary>
        public string AttributeValue { get; set; }

        /// <summary>
        /// 操作人
        /// </summary>
        public string SubmitBy { get; set; }
    }
}
