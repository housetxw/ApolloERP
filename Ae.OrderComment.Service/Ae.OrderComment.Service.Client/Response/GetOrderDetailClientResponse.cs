using Ae.OrderComment.Service.Client.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.OrderComment.Service.Client.Response
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
        #region 订单信息
        /// <summary>
        /// 订单编号
        /// </summary>
        public string OrderNo { get; set; }
        /// <summary>
        /// 订单渠道
        /// </summary>
        public sbyte OrderChannel { get; set; }
        /// <summary>
        /// 显示订单状态（待仓库发货...）
        /// </summary>
        public string DisplayOrderStatus { get; set; }

        public sbyte OrderType { get; set; }
        #endregion
        #region 商品信息
        /// <summary>
        /// 商品金额
        /// </summary>
        public decimal ProductAmount { get; set; }
        /// <summary>
        /// 套餐或单品信息集合
        /// </summary>
        public List<OrderDetailPackageProductClientDTO> Products { get; set; }
        /// <summary>
        /// 服务信息集合
        /// </summary>
        public List<OrderDetailProductDTO> Services { get; set; }
        #endregion
    }
}
