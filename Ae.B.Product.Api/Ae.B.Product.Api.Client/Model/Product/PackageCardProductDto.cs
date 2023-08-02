using Ae.B.Product.Api.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Product.Api.Client.Model.Product
{
    public class PackageCardProductDto : ProductBaseInfoDto
    {
        /// <summary>
        /// 套餐卡id
        /// </summary>
        public long PackageCardId { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Rank { get; set; }

        /// <summary>
        /// 套餐卡类型
        /// </summary>
        public PackageCardEnum Type { get; set; }
    }
}
