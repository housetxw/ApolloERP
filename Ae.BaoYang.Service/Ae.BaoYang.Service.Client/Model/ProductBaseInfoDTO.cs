using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.BaoYang.Service.Client.Model
{
    public class ProductBaseInfoDTO
    {
        /// <summary>
        /// 主键
        /// </summary>
        public string Id { get; set; }
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
        ///  单位
        /// </summary>
        public string Unit { get; set; }

        /// <summary>
        /// 通用 - 标准配件号
        /// </summary>
        public string PartCode { get; set; }
        /// <summary>
        /// 通用 - 配件编号 （用于保养品）
        /// </summary>
        public string PartNo { get; set; }
        /// <summary>
        /// 主图连接
        /// </summary>
        public string Image1 { get; set; }

        /// <summary>
        /// 基准价格
        /// </summary>
        public decimal StandardPrice { get; set; }
        /// <summary>
        /// 销售价
        /// </summary>
        public decimal SalesPrice { get; set; }

        /// <summary>
        /// 是否缺货
        /// </summary>
        public bool Stockout { get; set; }

        /// <summary>
        /// 颜色
        /// </summary>
        public string Color { get; set; }

        /// <summary>
        ///  产品性质，分为 0 实物产品、1 套餐产品、2 服务产品、3 数字产品
        /// </summary>
        public sbyte ProductAttribute { get; set; }

        /// <summary>
        /// 好评率
        /// </summary>
        public decimal FavorableRate { get; set; }

        /// <summary>
        /// 商品属性 List
        /// </summary>
        public List<ProductAttributevalueDTO> Attributevalues { get; set; }

        /// <summary>
        /// 安装服务list
        /// </summary>
        public List<ProductInstallserviceDto> InstallationServices { get; set; }

        ///// <summary>
        ///// 可用库存
        ///// </summary>
        //public int AvailableStockQuantity { get; set; }

        public bool IsOriginal { get; set; }
    }
}
