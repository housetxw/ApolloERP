using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.BaoYang.Service.Core.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class BaoYangProductModel
    {
        /// <summary>
        /// 产品ID
        /// </summary>
        public string Pid { get; set; }

        /// <summary>
        /// 显示标题
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// 商品名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 产品图片
        /// </summary>
        public string Image { get; set; }

        /// <summary>
        /// 价格
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// 市场价
        /// </summary>
        public decimal MarketPrice { get; set; }

        /// <summary>
        /// 标缺
        /// </summary>
        public bool StockOut { get; set; }

        /// <summary>
        /// 是否原厂
        /// </summary>
        public bool IsOriginal { get; set; }

        /// <summary>
        /// 是否套餐产品
        /// </summary>
        public bool IsPackageProduct { get; set; }

        /// <summary>
        /// 机油粘度
        /// </summary>
        public string Viscosity { get; set; }

        /// <summary>
        /// 机油等级
        /// </summary>
        public string Level { get; set; }

        /// <summary>
        /// 机油规格： 半合成  全合成  矿物油……
        /// </summary>
        public string OilGrade { get; set; }

        /// <summary>
        /// 制动液等级
        /// </summary>
        public string ScyLevel { get; set; }

        /// <summary>
        /// 品牌
        /// </summary>
        public string Brand { get; set; }

        /// <summary>
        /// 品牌系列：
        /// </summary>
        public string OilSeries { get; set; }

        /// <summary>
        /// 机油规格
        /// </summary>
        public string Specification { get; set; }

        /// <summary>
        /// 单位
        /// </summary>
        public string Unit { get; set; }

        /// <summary>
        /// 防冻液冰点
        /// </summary>
        public string AntifreezePoint { get; set; }

        /// <summary>
        /// 颜色
        /// </summary>
        public string Color { get; set; }

        /// <summary>
        /// 防冻液沸点
        /// </summary>
        public string BoilingPoint { get; set; }

        /// <summary>
        /// 配件号
        /// </summary>
        public string PartNo { get; set; }

        /// <summary>
        /// 雨刷系列
        /// </summary>
        public string WiperSeries { get; set; }

        /// <summary>
        /// 减震器系列
        /// </summary>
        public string JzqSeries { get; set; }

        /// <summary>
        /// 好评率
        /// </summary>
        public decimal FavorableRate { get; set; }

        ///// <summary>
        ///// 可用库存
        ///// </summary>
        //public int AvailableStockQuantity { get; set; }

        /// <summary>
        /// 自动变数箱油系类
        /// </summary>
        public string AtoSeries { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// 二级类目Id
        /// </summary>
        public int SecondCategoryId { get; set; }
    }
}
