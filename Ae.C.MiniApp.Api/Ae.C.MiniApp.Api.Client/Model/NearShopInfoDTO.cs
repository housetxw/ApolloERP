using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.MiniApp.Api.Client.Model
{
    public class NearShopInfoDTO
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
        /// 门店营业状态 0正常营业 1终止营业 2暂停营业 
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// 电话
        /// </summary>
        public string Telephone { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        public string Address { get; set; }
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
        /// 开始时间
        /// </summary>
        public DateTime StartWorkTime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndWorkTime { get; set; }
        /// <summary>
        /// 是否是默认门店
        /// </summary>
        public bool IsDefaultShop { get; set; }
        /// <summary>
        /// 总评分
        /// </summary>
        public decimal Score { get; set; }
        /// <summary>
        /// 总订单数量
        /// </summary>
        public int TotalOrder { get; set; }
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
        /// 门店标签
        /// </summary>
        public List<string> TagNames { get; set; } = new List<string>();

        /// <summary>
        /// 服务项目
        /// </summary>
        public List<string> ShopServices { get; set; }

        /// <summary>
        /// 门店专修品牌
        /// </summary>
        public List<BrandDTO> BrandNames { get; set; } = new List<BrandDTO>();

        public sbyte Type { get; set; }
    }
}
