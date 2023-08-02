using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Product.Api.Core.Model.BaoYang
{
    /// <summary>
    /// 保养五级属性适配列表
    /// </summary>
    public class BaoYangPropertyAdaptationVo
    {
        /// <summary>
        /// 品牌
        /// </summary>
        public string Brand { get; set; }

        /// <summary>
        /// 车系
        /// </summary>
        public string Vehicle { get; set; }

        /// <summary>
        /// 车系Id
        /// </summary>
        public string VehicleId { get; set; }

        /// <summary>
        /// 排量
        /// </summary>
        public string PaiLiang { get; set; }

        /// <summary>
        /// 年款
        /// </summary>
        public string Nian { get; set; }

        /// <summary>
        /// 款型
        /// </summary>
        public string SalesName { get; set; }

        /// <summary>
        /// 五级车型Tid
        /// </summary>
        public string Tid { get; set; }

        /// <summary>
        /// 开始生产年份
        /// </summary>
        public string ListedYear { get; set; }

        /// <summary>
        /// 停止生产年份
        /// </summary>
        public string StopProductionYear { get; set; }

        /// <summary>
        /// 机油类别：汽油，柴油，
        /// </summary>
        public string FuelType { get; set; }

        /// <summary>
        /// 发动机型号
        /// </summary>
        public string EngineNo { get; set; }

        /// <summary>
        /// OE件号
        /// </summary>
        public string OeCode { get; set; }

        /// <summary>
        /// 零件号
        /// </summary>
        public string PartCode { get; set; }

        /// <summary>
        /// 属性
        /// </summary>
        public string Property { get; set; }

        /// <summary>
        /// 属性值
        /// </summary>
        public string PropertyValue { get; set; }

        /// <summary>
        /// 属性图片地址
        /// </summary>
        public string ImageUrl { get; set; }

        /// <summary>
        /// 主键
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 保养Type
        /// </summary>
        public string BaoYangType { get; set; }

        /// <summary>
        /// 前端用
        /// </summary>
        public bool IsEdit { get; set; }
    }
}
