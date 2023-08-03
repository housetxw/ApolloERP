using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.MiniApp.Api.Core.Model.Product
{
    /// <summary>
    /// 商品主表信息
    /// </summary>
    public class ProductDetalInfoVo
    {
        /// <summary>
        /// 主键
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///  产品编码
        /// </summary>
        public string ProductCode { get; set; }

        /// <summary>
        /// 商品名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 产品显示名称
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// 品牌
        /// </summary>
        public string Brand { get; set; }

        /// <summary>
        ///  税率
        /// </summary>
        public decimal TaxRate { get; set; }

        /// <summary>
        /// 基准价格
        /// </summary>
        public decimal StandardPrice { get; set; }

        /// <summary>
        /// 销售价
        /// </summary>
        public decimal SalesPrice { get; set; }

        /// <summary>
        ///  单位
        /// </summary>
        public string Unit { get; set; }


        /// <summary>
        ///  产品性质，分为 0 实物产品、1 套餐产品、2 服务产品、3 数字产品
        /// </summary>
        public int ProductAttribute { get; set; }

        /// <summary>
        ///  上下架 
        /// </summary>
        public int OnSale { get; set; }

        /// <summary>
        /// 是否缺货  1 是 0否
        /// </summary>
        public int Stockout { get; set; }

        /// <summary>
        /// 是否有库存 1 是 0否
        /// </summary>
        public int HasStock { get; set; }

        /// <summary>
        ///  是否显示  -  0 否 1 是 用于控制产品是否在搜索及产品列表页显示。
        /// </summary>
        public int IsShow { get; set; }


        /// <summary>
        ///  是否有保险 0 否 1 是
        /// </summary>
        public int IsInsurance { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 通用 - 标准配件号
        /// </summary>
        public string PartCode { get; set; } = string.Empty;

        /// <summary>
        /// 好评率
        /// </summary>
        public decimal FavorableRate { get; set; }

        /// <summary>
        /// 广告促销语
        /// </summary>
        public string Advertisement { get; set; } = string.Empty;
    }
}
