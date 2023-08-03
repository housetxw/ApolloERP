using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Api.Core.Model
{
    public class VenderProductForAppVo
    {
        /// <summary>
        /// 供应商编号
        /// </summary>
        public long VenderId { get; set; }

        /// <summary>
        /// 供应商简称
        /// </summary>
        public string VenderShortName { get; set; } = string.Empty;

        /// <summary>
        /// 产品编号
        /// </summary>
        public string ProductId { get; set; } = string.Empty;

        /// <summary>
        /// 产品名称
        /// </summary>
        public string ProductName { get; set; } = string.Empty;

        /// <summary>
        /// 品牌
        /// </summary>
        public string Brand { get; set; } = string.Empty;

        public int Num { get; set; }
    }

}
