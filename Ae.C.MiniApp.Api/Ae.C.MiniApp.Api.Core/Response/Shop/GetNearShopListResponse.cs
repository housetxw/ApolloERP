using Ae.C.MiniApp.Api.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.MiniApp.Api.Core.Response.Shop
{
    public class GetNearShopListResponse
    {
        /// <summary>
        /// 门店id
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 简单名称
        /// </summary>
        public string SimpleName { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 营业时间
        /// </summary>
        public string WorkTime { get; set; }
        /// <summary>
        /// 总评分
        /// </summary>
        public decimal Score { get; set; }
        /// <summary>
        /// 总订单数量
        /// </summary>
        public int OrderTotalCount { get; set; }
        /// <summary>
        /// 经度
        /// </summary>
        public double Longitude { get; set; }
        /// <summary>
        /// 维度
        /// </summary>
        public double Latitude { get; set; }
        /// <summary>
        /// 距离（km）
        /// </summary>
        public decimal Distance { get; set; }
        /// <summary>
        /// 门店图片
        /// </summary>
        public string Img { get; set; }
        /// <summary>
        /// 洗车工位状态 1空闲 2适中 3繁忙
        /// </summary>
        public int CarWashStatus { get; set; }
        /// <summary>
        /// 保养工位状态 1空闲 2适中 3繁忙
        /// </summary>
        public int BaoYangStatus { get; set; }
        /// <summary>
        /// 美容工位状态 1空闲 2适中 3繁忙
        /// </summary>
        public int MeiRongStatus { get; set; }
        /// <summary>
        /// 服务项目
        /// </summary>
        public string[] ShopServices { get; set; }
        /// <summary>
        /// 是否是默认门店
        /// </summary>
        public bool IsDefaultShop { get; set; }
        /// <summary>
        /// 门店技师
        /// </summary>
        public List<TechVO> Techs { get; set; }
    }
}
