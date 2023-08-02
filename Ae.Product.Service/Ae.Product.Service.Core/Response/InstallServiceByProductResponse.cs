using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Ae.Product.Service.Core.Response
{
    /// <summary>
    /// InstallServiceByProductResponse
    /// </summary>
    public class InstallServiceByProductResponse
    {
        /// <summary>
        /// 产品服务对应关系
        /// </summary>
        public List<ProductRelateServiceVo> ProductInstallService { get; set; }

        /// <summary>
        /// 安装服务
        /// </summary>
        public List<InstallServiceDetailVo> InstallService { get; set; }
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
        /// 是否改变数量
        /// </summary>
        [IgnoreDataMember]
        public sbyte ChangeNum { get; set; }
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
