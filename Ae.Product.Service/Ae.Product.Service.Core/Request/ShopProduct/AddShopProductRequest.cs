using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.Product.Service.Core.Request.ShopProduct
{
    /// <summary>
    /// 添加门店外采产品
    /// </summary>
    public class AddShopProductRequest
    {
        /// <summary>
        /// 商品Pid
        /// </summary>
        public string ProductCode { get; set; }

        /// <summary>
        /// 商品名称
        /// </summary>
        [Required(ErrorMessage = "产品名称不能为空")]
        public string ProductName { get; set; }

        /// <summary>
        /// 商品型号
        /// </summary>
        public string Specification { get; set; }

        /// <summary>
        /// 类目Id
        /// </summary>
        public int CategoryId { get; set; }

        /// <summary>
        /// 销售价
        /// </summary>
        public decimal SalesPrice { get; set; }

        /// <summary>
        /// 采购单价
        /// </summary>
        public decimal PurchasePrice { get; set; }

        /// <summary>
        /// 单位
        /// </summary>
        public string Unit { get; set; }

        /// <summary>
        /// 原厂编号
        /// </summary>
        [Required(ErrorMessage = "原厂编号不能为空")]
        public string OeNumber { get; set; }

        /// <summary>
        /// 门店Id
        /// </summary>
        public int ShopId { get; set; }

        /// <summary>
        /// 提交人
        /// </summary>
        [Required(ErrorMessage = "提交人不能为空")]
        public string SubmitBy { get; set; }
    }
}
