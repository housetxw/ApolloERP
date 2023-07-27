using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.Shop.Service.Core.Request
{
    public class ModifyShopConfigExpenseRequest
    {
        /// <summary>
        /// 门店id
        /// </summary>
        [Range(1, int.MaxValue, ErrorMessage = "门店ID必须大于0")]
        public long ShopId { get; set; }
        /// <summary>
        /// 修改人
        /// </summary>
        public string UpdateBy { get; set; }
        /// <summary>
        /// 换轮胎费用
        /// </summary>
        public decimal FeePerTire { get; set; }
        /// <summary>
        /// 前刹车片费用
        /// </summary>
        public decimal FeePerFrontBrake { get; set; }
        /// <summary>
        /// 后刹车片费用
        /// </summary>
        public decimal FeePerRearBrake { get; set; }
        /// <summary>
        /// 前刹车盘费用
        /// </summary>
        public decimal FeePerFrontDisc { get; set; }
        /// <summary>
        /// 后刹车盘费用
        /// </summary>
        public decimal FeePerRearDisc { get; set; }
        /// <summary>
        /// 保养服务费用
        /// </summary>
        public decimal FeePerMaintain { get; set; }
        /// <summary>
        /// 四轮定位费用
        /// </summary>
        public decimal FeePer4Wheel { get; set; }
        /// <summary>
        /// PM2.5滤芯安装费用
        /// </summary>
        public decimal FeePmInstallation { get; set; }
        /// <summary>
        /// 每日保养约单量
        /// </summary>
        public int DailyOrderQuantity { get; set; }
        /// <summary>
        /// 每日保养爆单量
        /// </summary>
        public int DailyOrderUpperLimit { get; set; }
        /// <summary>
        /// 保养爆单量/约单量比例
        /// </summary>
        public decimal OptionOrderCount { get; set; }
        /// <summary>
        /// 每日轮胎约单量
        /// </summary>
        public int DailyTireOrderQuantity { get; set; }
        /// <summary>
        /// 每日轮胎爆单量
        /// </summary>
        public int DailyTireOrderUpperLimit { get; set; }
        /// <summary>
        /// 轮胎爆单量/约单量比例
        /// </summary>
        public decimal OptionTireOrderCount { get; set; }
    }
}
