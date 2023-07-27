using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Service.Core.Model
{
    public class ShopCodeNameDTO
    {
        /// <summary>
        /// CODE
        /// </summary>
        public int Code { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
    }
    public class ShopCodeName 
    {
        /// <summary>
        /// 职级
        /// </summary>
        public static List<ShopCodeNameDTO> ShopJobLevel = new List<ShopCodeNameDTO>()
        {
            new ShopCodeNameDTO{ Code = 0,Name = "初级"},
            new ShopCodeNameDTO{ Code = 1,Name = "中级"},
            new ShopCodeNameDTO{ Code = 2,Name = "高级"}
        };
        /// <summary>
        /// 门店工种级别
        /// </summary>
        public static List<ShopCodeNameDTO> ShopWorkKindLevel = new List<ShopCodeNameDTO>()
        {
            new ShopCodeNameDTO{ Code = 0,Name = "学徒"},
            new ShopCodeNameDTO{ Code = 1,Name = "小工"},
            new ShopCodeNameDTO{ Code = 2,Name = "中工"},
            new ShopCodeNameDTO{ Code = 2,Name = "大工"}
        };

    }
}
