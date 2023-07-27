using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Service.Core.Model
{
    /// <summary>
    /// 门店积分
    /// </summary>
    public class ShopPointVo
    {
        /// <summary>
        /// 当前积分
        /// </summary>
        public int CurrentPoint { get; set; }

        /// <summary>
        /// 积分详情
        /// </summary>
        public List<ShopPointDetailVo> PointDetail { get; set; } = new List<ShopPointDetailVo>();
    }

    /// <summary>
    /// 积分详情Vo
    /// </summary>
    public class ShopPointDetailVo
    {
        /// <summary>
        /// 积分描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public string CreatedDate { get; set; }

        /// <summary>
        /// 积分值
        /// </summary>
        public string PointValueDisplay { get; set; }
    }
}
