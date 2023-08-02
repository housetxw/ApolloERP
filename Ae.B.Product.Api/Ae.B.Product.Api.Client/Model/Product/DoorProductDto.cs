using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Product.Api.Client.Model.Product
{
    public class DoorProductDto
    {
        /// <summary>
        /// 主键
        /// </summary>
        public long Id { get; set; }

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
        /// 免上门费
        /// </summary>
        public sbyte FreeDoorFee { get; set; }
    }
}
