using Ae.Receive.Service.Client.Model;
using Ae.Receive.Service.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Receive.Service.Client.Response
{
    public class GetOrderDetailClientResponse
    {
        /// <summary>
        /// 门店Id
        /// </summary>
        public long ShopId { get; set; }
        /// <summary>
        /// 门店名称
        /// </summary>
        public string ShopName { get; set; }
        /// <summary>
        /// 订单编号
        /// </summary>
        public string OrderNo { get; set; }

        /// <summary>
        /// 订单类型
        /// </summary>
        public OrderTypeEnum OrderType { get; set; }

        /// <summary>
        /// 显示订单状态（待仓库发货...）
        /// </summary>
        public string DisplayOrderStatus { get; set; }
        /// <summary>
        /// 商品金额
        /// </summary>
        public decimal ProductAmount { get; set; }
        /// <summary>
        /// 实付款
        /// </summary>
        public decimal ActualAmount { get; set; }
        /// <summary>
        /// 套餐或单品信息集合
        /// </summary>
        public List<OrderDetailPackageProductClientDTO> Products { get; set; }
        /// <summary>
        /// 服务信息集合
        /// </summary>
        public List<OrderDetailProductDTO> Services { get; set; }

        #region 车辆信息
        /// <summary>
        /// 显示车系名称（格式：品牌Brand - 车系Vehicle）
        /// </summary>
        public string DisplayVehicleName { get; set; }
        /// <summary>
        /// 显示款型名称（格式：年份Nian - 排量PaiLiang - 款型SalesName）
        /// </summary>
        public string DisplayModelName { get; set; }
        #endregion
    }
}
