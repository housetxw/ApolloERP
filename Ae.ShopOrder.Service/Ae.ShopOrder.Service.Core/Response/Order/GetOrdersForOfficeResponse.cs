using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.ShopOrder.Service.Core.Response.Order
{
    public  class GetOrdersForOfficeResponse
    {
        /// <summary>
        /// 订单号（RGC前缀）
        /// </summary>
        public string OrderNo { get; set; } = string.Empty;
        /// <summary>
        /// 渠道（0未设置 1C端 2门店）
        /// </summary>
        public sbyte Channel { get; set; }
        /// <summary>
        /// 订单类型（0未设置 1轮胎 2保养 3美容 4 钣金维修 5 汽车改装 6 其他）
        /// </summary>
        public sbyte OrderType { get; set; }
        /// <summary>
        /// 订单状态（10已提交 20已确认 30已完成 40已取消）
        /// </summary>
        public sbyte OrderStatus { get; set; }

        /// <summary>
        /// 下单时间
        /// </summary>
        public DateTime OrderTime { get; set; } = new DateTime(1900, 1, 1);

        /// <summary>
        /// 商品总数
        /// </summary>
        public int TotalProductNum { get; set; }

        /// <summary>
        /// 总价
        /// </summary>
        public decimal TotalAmount => ActualAmount;
        /// <summary>
        /// 实付金额
        /// </summary>
        public decimal ActualAmount { get; set; }

        /// <summary>
        /// 支付状态（未支付 已支付）
        /// </summary>
        public sbyte PayStatus { get; set; }

        /// <summary>
        /// 签收状态（未签收 已签收）
        /// </summary>
        public sbyte SignStatus { get; set; }

        /// <summary>
        /// 安装时间
        /// </summary>
        public DateTime InstallTime { get; set; } = new DateTime(1900, 1, 1);

        /// <summary>
        /// 明细信息
        /// </summary>
        public List<GetOrdersForOfficeProductDTO> Goods { get; set; } = new List<GetOrdersForOfficeProductDTO>();
    }

    /// <summary>
    /// 产品明细
    /// </summary>
    public class GetOrdersForOfficeProductDTO
    {
        /// <summary>
        /// 产品性质（0 实物产品、1 套餐产品、2 服务产品、3 数字产品）
        /// </summary>
        public sbyte ProductAttribute { get; set; }

        /// <summary>
        /// 图片路径
        /// </summary>
        public string ImageUrl { get; set; } = string.Empty;

        /// <summary>
        /// 产品Id
        /// </summary>
        public string ProductId { get; set; } = string.Empty;

        /// <summary>
        /// 所属父级订单套餐产品Id
        /// </summary>
        public long ParentOrderPackagePid { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public int Number { get; set; }

        /// <summary>
        /// 总数量（指乘以购买数量后）
        /// </summary>
        public int TotalNumber { get; set; }

        /// <summary>
        /// 产品名称
        /// </summary>
        public string ProductName { get; set; } = string.Empty;
    }
}
