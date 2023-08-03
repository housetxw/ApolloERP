using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Ae.Shop.Api.Core.Model.ShopCost
{
    [Table("shop_cost_type")]
    public class ShopCostTypeDO
    {
        /// <summary>
        /// 编号
        /// </summary>
        [Column("code")]
        public int Code { get; set; }
        [Column("name")]
        public string Name { get; set; }
    }
}
