using ApolloErp.Web.WebApi;
using Ae.Shop.Service.Core.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Service.Core.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class ShopListPageResponse
    {
        /// <summary>
        /// 门店区域
        /// </summary>
        public ShopServiceRegion ShopRegion { get; set; }

        /// <summary>
        /// 服务类型
        /// </summary>
        public List<ServiceTypeVo> ServiceType { get; set; }

        /// <summary>
        /// 门店列表
        /// </summary>
        public ApiPagedResultData<ShopListPageVo> ShopList { get; set; }
    }

    /// <summary>
    /// 门店
    /// </summary>
    public class ShopListPageVo
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
        /// 距离（km）
        /// </summary>
        public decimal Distance { get; set; }

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
        /// 描述
        /// </summary>
        public string Description { get; set; }
    }

    /// <summary>
    /// 服务类型
    /// </summary>
    public class ServiceTypeVo
    {
        /// <summary>
        /// 分类Id
        /// </summary>
        public long CategoryId { get; set; }

        /// <summary>
        /// 服务类型
        /// </summary>
        public string ServiceType { get; set; }

        /// <summary>
        /// 服务类型 - 显示名
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// 订单类型（历史原因 产品大类与订单类型设置的不一致）
        /// </summary>
        public long OrderType { get; set; }
    }
}
