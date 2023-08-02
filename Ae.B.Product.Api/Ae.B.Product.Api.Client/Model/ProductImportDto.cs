using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Product.Api.Client.Model
{
    /// <summary>
    /// 导入商品模板信息
    /// </summary>
    public class ProductImportDto
    {
        /// <summary>
        /// 类别（ 2子产品，4 父产品）
        /// </summary>
        public int ClassType { get; set; }
        /// <summary>
        /// 关联类目Id
        /// </summary>

        public int CategoryId { get; set; }

        /// <summary>
        /// 商品名称
        /// </summary>
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// 产品标题
        /// </summary>
        public string DisplayName { get; set; } = string.Empty;

        /// <summary>
        ///  产品性质，分为 0 实物产品、1 套餐产品、2 服务产品、3 数字产品
        /// </summary>

        public int ProductAttribute { get; set; }
        /// <summary>
        ///  单位
        /// </summary>
        public string Unit { get; set; } = string.Empty;
        /// <summary>
        /// 品牌
        /// </summary>
        public string Brand { get; set; } = string.Empty;

        /// <summary>
        ///  税率
        /// </summary>
        public decimal TaxRate { get; set; }

        /// <summary>
        /// 市场价
        /// </summary>

        public decimal StandardPrice { get; set; }
        /// <summary>
        /// 销售价
        /// </summary>
        public decimal SalesPrice { get; set; }

        /// <summary>
        ///  长
        /// </summary>
        public decimal Length { get; set; }
        /// <summary>
        ///  宽
        /// </summary>
        public decimal Width { get; set; }
        /// <summary>
        ///  高
        /// </summary>

        public decimal Height { get; set; }

        /// <summary>
        /// 通用 - 标准配件号
        /// </summary>
        public string PartCode { get; set; } = string.Empty;

    }
}
