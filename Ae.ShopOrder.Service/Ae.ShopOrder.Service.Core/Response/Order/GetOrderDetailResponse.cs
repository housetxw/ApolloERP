using System;
using System.Collections.Generic;
using System.Text;
using Ae.ShopOrder.Service.Core.Enums;
using Ae.ShopOrder.Service.Core.Model.Order;

namespace Ae.ShopOrder.Service.Core.Response.Order
{ /// <summary>
  /// 获取订单详情返回参数
  /// </summary>
    public class GetOrderDetailResponse
    {
        /// <summary>
        /// 摘要
        /// </summary>
        public string Summary { get; set; }
        /// <summary>
        /// 副摘要
        /// </summary>
        public string SubSummary { get; set; }

        #region 配送信息
        /// <summary>
        /// 是否需要配送
        /// </summary>
        public sbyte IsNeedDelivery { get; set; }
        /// <summary>
        /// 配送类型（0未设置 1配送到店 2配送到家）
        /// </summary>
        public sbyte DeliveryType { get; set; }

        /// <summary>
        /// 门店Id
        /// </summary>
        public long ShopId { get; set; }
        /// <summary>
        /// 门店名称
        /// </summary>
        public string ShopName { get; set; }
        /// <summary>
        /// 门店地址
        /// </summary>
        public string ShopAddress { get; set; }
        public string ShopPhone { get; set; }
        /// <summary>
        /// 经度
        /// </summary>
        public double Longitude { get; set; }
        /// <summary>
        /// 维度
        /// </summary>
        public double Latitude { get; set; }

        /// <summary>
        /// 收货人姓名
        /// </summary>
        public string ReceiverName { get; set; }
        /// <summary>
        /// 收货人电话
        /// </summary>
        public string ReceiverPhone { get; set; }

        /// <summary>
        /// 下单人姓名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 下单人电话
        /// </summary>
        public string UserPhone { get; set; } = string.Empty;
        #endregion

        #region 车辆信息

        /// <summary>
        /// 显示车系名称（格式：品牌Brand - 车系Vehicle）
        /// </summary>
        public string DisplayVehicleName { get; set; } = string.Empty;

        /// <summary>
        /// 显示款型名称（格式：年份Nian - 排量PaiLiang - 款型SalesName）
        /// </summary>
        public string DisplayModelName { get; set; } = string.Empty;

        public string CarId { get; set; }
        #endregion

        /// <summary>
        /// 套餐或单品信息集合
        /// </summary>
        public List<OrderDetailPackageProductDTO> Products { get; set; }

        /// <summary>
        /// 服务信息集合
        /// </summary>
        public List<OrderDetailProductDTO> Services { get; set; }

        #region 订单信息
        /// <summary>
        /// 订单编号
        /// </summary>
        public string OrderNo { get; set; }
        public long ReserveId { get; set; }
        /// <summary>
        /// 订单渠道
        /// </summary>
        public ChannelEnum OrderChannel { get; set; }
        /// <summary>
        /// 订单类型
        /// </summary>
        public OrderTypeEnum OrderType { get; set; }
        /// <summary>
        /// 订单时间
        /// </summary>
        public DateTime OrderTime { get; set; }
        /// <summary>
        /// 显示订单渠道（微信小程序...）
        /// </summary>
        public string DisplayOrderChannel { get; set; }
        /// <summary>
        /// 显示服务类型
        /// </summary>
        public string DisplayServiceCategory { get; set; }
        /// <summary>
        /// 显示支付方式（微信支付 到店支付）
        /// </summary>
        public string DisplayPayMethod { get; set; }
        /// <summary>
        /// 显示配送方式（送货到店...）
        /// </summary>
        public string DisplayDeliveryMethod { get; set; }
        /// <summary>
        /// 显示订单状态（待仓库发货...）
        /// </summary>
        public string DisplayOrderStatus { get; set; }

        /// <summary>
        /// 显示订单类型
        /// </summary>
        public string DisplayOrderType { get; set; }

        #endregion

        #region 金额信息
        /// <summary>
        /// 商品金额
        /// </summary>
        public decimal ProductAmount { get; set; }
        /// <summary>
        /// 服务金额
        /// </summary>
        public decimal ServiceAmount { get; set; }
        /// <summary>
        /// 运费
        /// </summary>
        public decimal DeliveryFee { get; set; }
        /// <summary>
        /// 优惠
        /// </summary>
        public decimal TotalCouponAmount { get; set; }
        /// <summary>
        /// 实付款
        /// </summary>
        public decimal ActualAmount { get; set; }
        #endregion

        /// <summary>
        /// 订单用户当前可执行操作集合
        /// </summary>
        public List<OrderUserOperationDTO> OrderUserOperations { get; set; }

        /// <summary>
        /// 服务时间(预约时间)
        /// </summary>
        public string ReserverTime { get; set; }

        /// <summary>
        /// 订单的派工信息
        /// </summary>
        public OrderDispatchDTO OrderDispatch { get; set; }

        /// <summary>
        /// 产品类型
        /// </summary>
        public sbyte ProductType { get; set; }


        /// <summary>
        /// 订单安装码信息集合（现在最多只会有一个安装码）
        /// </summary>
        public List<OrderInstallCodeInfoDTO> OrderInstallCodeInfos { get; set; }

        /// <summary>
        /// 订单核销码信息集合（现在只返回一个默认的，其他从查看更多获取）
        /// </summary>
        public List<OrderVerificationCodeInfoDTO> OrderVerificationCodeInfos { get; set; }

        public List<ShopArrivalVideoResponse> OrderVideos { get; set; }

       
        public string ContactName { get; set; }

        public string ContactPhone { get; set; }
    }

    
}
