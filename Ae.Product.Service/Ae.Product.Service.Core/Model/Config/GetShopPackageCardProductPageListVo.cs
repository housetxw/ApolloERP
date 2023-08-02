using Ae.Product.Service.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Product.Service.Core.Model.Config
{
    public class GetShopPackageCardProductPageListVo: ProductBaseInfoVo
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

        public string ShopIds { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public long Id { get; set; }
    }
}
