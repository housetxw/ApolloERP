using Ae.Shop.Service.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Service.Core.Response.APP
{
    /// <summary>
    /// Response
    /// </summary>
    public class ShopDetailForShopResponse
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
        /// 省名称
        /// </summary>
        public string Province { get; set; } = string.Empty;

        /// <summary>
        /// 市名称
        /// </summary>
        public string City { get; set; } = string.Empty;

        /// <summary>
        /// 区名称
        /// </summary>
        public string District { get; set; } = string.Empty;

        /// <summary>
        /// 详细地址
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 电话
        /// </summary>
        public string Telephone { get; set; }

        /// <summary>
        /// 总评分
        /// </summary>
        public decimal Score { get; set; }

        /// <summary>
        /// 总订单数量
        /// </summary>
        public int TotalOrder { get; set; }

        /// <summary>
        /// 距离（km）
        /// </summary>
        public decimal Distance { get; set; }

        /// <summary>
        /// 经度
        /// </summary>
        public double Longitude { get; set; }

        /// <summary>
        /// 维度
        /// </summary>
        public double Latitude { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 门店图片
        /// </summary>
        public List<string> Imgs { get; set; }

        /// <summary>
        /// 营业时间
        /// </summary>
        public string WorkTime { get; set; }

        /// <summary>
        /// 门店标签
        /// </summary>
        public List<string> TagNames { get; set; } = new List<string>();

        /// <summary>
        /// 店类型
        /// </summary>

        public int Type { get; set; }

        /// <summary>
        /// 押金额度
        /// </summary>
        public decimal DepositAmount { get; set; }

        /// <summary>
        /// 服务类型
        /// </summary>
        public List<ServiceTypeVo> ServiceType { get; set; }
    }
}
