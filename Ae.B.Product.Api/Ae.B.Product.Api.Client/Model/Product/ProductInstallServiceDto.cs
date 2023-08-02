using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Product.Api.Client.Model.Product
{
    public class ProductInstallServiceDto
    {
        /// <summary>
        /// 产品Id
        /// </summary>
        public long ProductId { get; set; }

        /// <summary>
        /// 产品Pid
        /// </summary>
        public string Pid { get; set; }

        /// <summary>
        /// 产品名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 品牌
        /// </summary>
        public string Brand { get; set; }

        /// <summary>
        /// 销售价
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// 是否上架
        /// </summary>
        public sbyte OnSale { get; set; }

        /// <summary>
        /// 安装服务
        /// </summary>
        public List<InstallServiceDto> InstallService { get; set; }
    }

    /// <summary>
    /// InstallServiceVo
    /// </summary>
    public class InstallServiceDto
    {
        /// <summary>
        /// 主键
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 安装服务pid
        /// </summary>
        public string ServiceId { get; set; }

        /// <summary>
        /// 安装服务名称
        /// </summary>
        public string ServiceName { get; set; }

        /// <summary>
        /// 价格
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// 包安装
        /// </summary>
        public sbyte FreeInstall { get; set; }

        /// <summary>
        /// 安装数量
        /// </summary>
        public sbyte ChangeNum { get; set; }
    }
}
