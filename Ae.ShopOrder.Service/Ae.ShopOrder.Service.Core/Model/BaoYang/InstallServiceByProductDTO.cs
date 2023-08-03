using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.ShopOrder.Service.Core.Model.BaoYang
{
    /// <summary>
    /// InstallServiceByProductResponse
    /// </summary>
    public class InstallServiceByProductDTO
    {
        /// <summary>
        /// 产品服务对应关系
        /// </summary>
        public List<ProductRelateServiceVo> ProductInstallService { get; set; } = new List<ProductRelateServiceVo>();

        /// <summary>
        /// 安装服务
        /// </summary>
        public List<InstallServiceDetailVo> InstallService { get; set; } = new List<InstallServiceDetailVo>();
    }

    /// <summary>
    /// InstallServiceVo
    /// </summary>
    public class InstallServiceDetailVo
    {
        /// <summary>
        /// 服务名称
        /// </summary>
        public string ServiceName { get; set; }

        /// <summary>
        /// 服务pid
        /// </summary>
        public string ServiceId { get; set; }

        /// <summary>
        /// 价格
        /// </summary>
        public Decimal Price { get; set; }

        /// <summary>
        /// 市场价
        /// </summary>
        public Decimal MarketPrice { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// 图片
        /// </summary>
        public string ImageUrl { get; set; }

        /// <summary>
        /// 包安装
        /// </summary>
        public sbyte FreeInstall { get; set; }

        /// <summary>
        /// 车型加价金额
        /// </summary>
        public decimal AdditionalPrice { get; set; }

        /// <summary>
        /// 加价备注
        /// </summary>
        public string Remark { get; set; } = string.Empty;

        /// <summary>
        /// 服务原价（加价之前的价格）
        /// </summary>
        public decimal OriginalPrice { get; set; }
    }

    /// <summary>
    /// ProductRelateServiceVo
    /// </summary>
    public class ProductRelateServiceVo
    {
        /// <summary>
        /// 产品id(FI-TORA-TA-1758|)
        /// </summary>
        public string ProductId { get; set; }

        /// <summary>
        /// 产品对应的安装服务id(FU-BY-KONGQI|)
        /// </summary>
        public List<string> ServiceId { get; set; }
    }
}
