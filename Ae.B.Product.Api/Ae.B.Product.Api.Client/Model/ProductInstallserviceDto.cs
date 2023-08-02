using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Product.Api.Client.Model
{
    public class ProductInstallserviceDto
    {
        /// <summary>
        /// 主键
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 商品id
        /// </summary>
        public string ProductId { get; set; }

        /// <summary>
        /// 产品pid
        /// </summary>
        public string Pid { get; set; }

        /// <summary>
        /// 服务id
        /// </summary>
        public string ServiceId { get; set; }

        /// <summary>
        /// 服务名称
        /// </summary>
        public string ServiceName { get; set; }

        /// <summary>
        /// 服务价格
        /// </summary>
        public decimal ServicePrice { get; set; }

        /// <summary>
        /// 顺序
        /// </summary>
        public int Sort { get; set; }

        /// <summary>
        /// 包安装
        /// </summary>
        public sbyte FreeInstall { get; set; }

        /// <summary>
        /// 服务数量
        /// </summary>
        public sbyte ChangeNum { get; set; }

        /// <summary>
        /// 是否 删除 0 否  1是  
        /// </summary>
        public sbyte IsDeleted { get; set; } = 0;
    }
}
