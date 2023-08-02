using Ae.B.Product.Api.Common.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Product.Api.Core.Model
{
    public class ProductAttributeImportVo
    {
        /// <summary>
        /// 通用 - 标准配件号
        /// </summary>
        [ExcelColumn(Index = 0, Title = "配件编码")]
        public string PartCode { get; set; }
        /// 属性名
        /// </summary>
        [ExcelColumn(Index = 1, Title = "属性名")]
        public string AttributeName { get; set; }
        /// <summary>
        /// 商品属性值
        /// </summary>
        [ExcelColumn(Index = 2, Title = "商品属性值")]
        public string AttributeValue { get; set; }

        /// <summary>
        /// 错误信息
        /// </summary>
        public string ErrorMessage { get; set; }

    }
}
