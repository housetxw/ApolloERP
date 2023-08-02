using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.Product.Service.Core.Request.Product
{
    /// <summary>
    /// 服务大类 商品列表
    /// </summary>
    public class ProductListByServiceTypeRequest
    {
        /// <summary>
        /// 搜索关键字
        /// </summary>
        public string SearchContent { get; set; }

        /// <summary>
        ///  分类ID
        /// </summary>
        public int CategoryId { get; set; }

        /// <summary>
        /// 车型
        /// </summary>
        //[Required(ErrorMessage = "Vehicle不能为空")]
        public VehicleRequest Vehicle { get; set; }

        /// <summary>
        /// 用户Id
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// shopId
        /// </summary>
        public int ShopId { get; set; }

        /// <summary>
        /// 渠道：MiniApp,ShopApp
        /// </summary>
        public string Channel { get; set; }

        /// <summary>
        /// 入口类型（默认0  1：显示折扣率）
        /// </summary>
        public int EntranceType { get; set; }
    }

    /// <summary>
    /// 车型数据
    /// </summary>
    public class VehicleRequest
    {
        /// <summary>
        /// 车系Id
        /// </summary>
        [Required(ErrorMessage = "VehicleId不能为空")]
        public string VehicleId { get; set; }

        /// <summary>
        /// 排量
        /// </summary>
        [Required(ErrorMessage = "PaiLiang不能为空")]
        public string PaiLiang { get; set; }

        /// <summary>
        /// 生产年份
        /// </summary>
        [Required(ErrorMessage = "Nian不能为空")]
        public string Nian { get; set; }

        /// <summary>
        /// 五级车型Tid
        /// </summary>
        [Required(ErrorMessage = "Tid不能为空")]
        public string Tid { get; set; }

        /// <summary>
        /// 五级属性
        /// </summary>
        public List<VehicleProperty> Properties { get; set; }

        /// <summary>
        /// 里程数
        /// </summary>
        public int Distance { get; set; }

        /// <summary>
        /// 上路时间（Eg:2018-09）
        /// </summary>
        public string OnRoadTime { get; set; }

        /// <summary>
        /// 轮胎尺寸
        /// </summary>
        public string TireSize { get; set; }
    }

    /// <summary>
    /// 车型五级属性
    /// </summary>
    public class VehicleProperty
    {
        /// <summary>
        /// 属性名
        /// </summary>
        [Required(ErrorMessage = "PropertyKey不能为空")]
        public string PropertyKey { get; set; }

        /// <summary>
        /// 属性值
        /// </summary>
        [Required(ErrorMessage = "PropertyValue不能为空")]
        public string PropertyValue { get; set; }
    }
}
