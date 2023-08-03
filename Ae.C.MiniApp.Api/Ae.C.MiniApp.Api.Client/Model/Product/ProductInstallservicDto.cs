using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.MiniApp.Api.Client.Model.Product
{
    public class ProductInstallservicDto
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
    }
}
