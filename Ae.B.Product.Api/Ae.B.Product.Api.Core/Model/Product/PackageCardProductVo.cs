using Ae.B.Product.Api.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Product.Api.Core.Model.Product
{
    public class PackageCardProductVo : ProductBaseInfoVo
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

        /// <summary>
        /// 套餐卡类型 显示
        /// </summary>
        public string TypeDisplay { get; set; }
    }
}
