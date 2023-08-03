using Ae.C.MiniApp.Api.Core.Enums;
using Ae.C.MiniApp.Api.Core.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Ae.C.MiniApp.Api.Core.Request.Product
{
    public class ProductSearchRequest:BasePageRequest
    {
        /// <summary>
        /// 搜索关键字
        /// </summary>
        [Description("搜索关键字 商品编码 或者 名称 ")]
        [Required(ErrorMessage = "Key 不能为空")]
        public string Key { get; set; }

        /// <summary>
        /// 搜索排序
        /// </summary>
        public ProductSearchSortType Sort { get; set; }

        /// <summary>
        /// 开始价格
        /// </summary>
        public decimal? StartPrice { get; set; }

        /// <summary>
        /// 结束价格
        /// </summary>
        public decimal? EndPrice { get; set; }

        /// <summary>
        /// 品牌
        /// </summary>
        public string Brand { get; set; } = string.Empty;

        /// <summary>
        /// 一级类目Id
        /// </summary>
        public sbyte? MainCategoryId { get; set; }


        /// <summary>
        /// 二级类目Id
        /// </summary>
        public sbyte? SecondCategoryId { get; set; }

        /// <summary>
        /// 三级类目Id
        /// </summary>
        public sbyte? ChildCategoryId { get; set; }


        /// 适配相关
        /// <summary>
        /// 二级车型Id  登录用户，需要传入 车型ID，适配出车型适配的数据
        /// </summary>
        public string VehicleId { get; set; } = string.Empty;

        /// 适配相关
        /// <summary>
        /// 五级车型Id  登录用户，需要传入 TId，适配出车型适配的数据
        /// </summary>
        public string TId { get; set; } = string.Empty;

        /// <summary>
        /// 排量   登录用户，需要传入 排量 ，适配出车型适配的数据
        /// </summary>
        public string PaiLiang { get; set; }

        /// <summary>
        /// 生产年份 登录用户，需要传入 生产年份 2018，适配出车型适配的数据
        /// </summary>
        public string Nian { get; set; }
    }
}
