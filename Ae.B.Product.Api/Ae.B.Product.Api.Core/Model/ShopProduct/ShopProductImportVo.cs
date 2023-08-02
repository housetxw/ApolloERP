using Ae.B.Product.Api.Common.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Product.Api.Core.Model.ShopProduct
{
    public class ShopProductImportVo
    {
        /// <summary>
        /// 门店Id
        /// </summary>
        [ExcelColumn(Index = 0, Title = "门店Id")]
        public string ShopId { get; set; }
        /// <summary>
        /// 类目Id
        /// </summary>
        [ExcelColumn(Index =1, Title = "类目Id")]
        public string CategoryId { get; set; }

        /// <summary>
        /// 商品名称
        /// </summary>
        [ExcelColumn(Index = 2, Title = "商品名称")]
        public string ProductName { get; set; } = string.Empty;
        /// <summary>
        /// 小程序显示名称
        /// </summary>
        [ExcelColumn(Index = 3, Title = "小程序显示名称")]
        public string DisplayName { get; set; } = string.Empty;
        /// <summary>
        /// 产品描述
        /// </summary>
        [ExcelColumn(Index = 4, Title = "产品描述")]
        public string Description { get; set; } = string.Empty;
        /// <summary>
        /// 销售价
        /// </summary>
        [ExcelColumn(Index = 5, Title = "销售价")]
        public string SalesPrice { get; set; }

        /// <summary>
        /// 缩率图
        /// </summary>
        [ExcelColumn(Index = 6, Title = "缩率图")]
        public string Icon { get; set; } = string.Empty;

        /// <summary>
        /// 错误信息
        /// </summary>
        public string ErrorMessage { get; set; }

    }
}
