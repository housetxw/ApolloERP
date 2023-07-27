using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Service.Dal.Model
{
    public class ShopCategoryServiceDO
    {
        /// <summary>
        /// 大类ID
        /// </summary>
        public long CategoryId { get; set; }
        /// <summary>
        /// 门店ID
        /// </summary>
        public long ShopId { get; set; }
        /// <summary>
        /// 大类
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 分类标识
        /// </summary>
        public string Code { get; set; }
    }
}
