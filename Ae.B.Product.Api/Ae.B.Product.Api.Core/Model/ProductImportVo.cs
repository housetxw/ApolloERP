using Ae.B.Product.Api.Common.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Product.Api.Core.Model
{
    /// <summary>
    /// 导入商品模板信息
    /// </summary>
    public class ProductImportVo
    {
        /// <summary>
        /// 关联类目Id
        /// </summary>
        [ExcelColumn(Index = 0, Title = "关联类目Id")]
        public string CategoryId { get; set; }

        /// <summary>
        /// 商品名称
        /// </summary>
        [ExcelColumn(Index = 1, Title = "商品名称")]
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// 产品标题
        /// </summary>
        [ExcelColumn(Index = 2, Title = "产品标题")]
        public string DisplayName { get; set; } = string.Empty;

        /// <summary>
        ///  产品性质，分为 0 实物产品、1 套餐产品、2 服务产品、3 数字产品
        /// </summary>
        [ExcelColumn(Index = 3, Title = "产品性质")]
        public string ProductAttribute { get; set; }
        /// <summary>
        ///  单位
        /// </summary>
        [ExcelColumn(Index = 4, Title = "单位")]
        public string Unit { get; set; } = string.Empty;
        /// <summary>
        /// 品牌
        /// </summary>
        [ExcelColumn(Index = 5, Title = "品牌")]
        public string Brand { get; set; } = string.Empty;

        /// <summary>
        ///  税率
        /// </summary>
        [ExcelColumn(Index = 6, Title = "税率")]
        public string TaxRate { get; set; }

        /// <summary>
        /// 市场价
        /// </summary>
        [ExcelColumn(Index = 7, Title = "市场价")]
        public string StandardPrice { get; set; }
        /// <summary>
        /// 销售价
        /// </summary>
        [ExcelColumn(Index = 8, Title = "销售价")]
        public string SalesPrice { get; set; }
       
        /// <summary>
        ///  长
        /// </summary>
        [ExcelColumn(Index = 9, Title = "长")]
        public string Length { get; set; }
        /// <summary>
        ///  宽
        /// </summary>
        [ExcelColumn(Index = 10, Title = "宽")]
        public string Width { get; set; }
        /// <summary>
        ///  高
        /// </summary>
        [ExcelColumn(Index = 11, Title = "高")]
        public string Height { get; set; }
      
        /// <summary>
        /// 通用 - 标准配件号
        /// </summary>
        [ExcelColumn(Index = 12, Title = "配件编码")]
        public string PartCode { get; set; } = string.Empty;

        /// <summary>
        /// 错误信息
        /// </summary>
        public string ErrorMessage { get; set; }
    }
}
