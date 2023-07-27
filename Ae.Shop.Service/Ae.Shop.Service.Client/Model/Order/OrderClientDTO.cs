using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Service.Client.Model
{
   public class OrderClientDTO
    {
        /// <summary>
        /// 主键
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 订单号（RGC或RGS前缀）
        /// </summary>
        public string OrderNo { get; set; } = string.Empty;
       
        /// <summary>
        /// 订单类型（0未设置 1轮胎 2保养 3美容）
        /// </summary>
        public sbyte OrderType { get; set; }
        /// <summary>
        /// 产生类型（0普通产生 1购买核销码产生 2使用核销码产生 3再次购买产生 4追加下单产生）
        /// </summary>
        public sbyte ProduceType { get; set; }
        /// <summary>
        /// 订单状态（10已提交 20已确认 30已完成 40已取消）
        /// </summary>
        public sbyte OrderStatus { get; set; }
        /// <summary>
        /// 下单时间
        /// </summary>
        public DateTime OrderTime { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 下单人用户Id
        /// </summary>
        public string UserId { get; set; } = string.Empty;
        /// <summary>
        /// 下单人姓名
        /// </summary>
        public string UserName { get; set; } = string.Empty;
        /// <summary>
        /// 下单人电话
        /// </summary>
        public string UserPhone { get; set; } = string.Empty;
        /// <summary>
        /// 联系人用户Id
        /// </summary>
        public string ContactId { get; set; } = string.Empty;
        /// <summary>
        /// 联系人姓名
        /// </summary>
        public string ContactName { get; set; } = string.Empty;
        /// <summary>
        /// 联系人电话
        /// </summary>
        public string ContactPhone { get; set; } = string.Empty;
        /// <summary>
        /// 商品总数（指单品或套餐，不含套餐明细和带出的套餐外安装服务）
        /// </summary>
        public int TotalProductNum { get; set; }
        /// <summary>
        /// 商品总价（指单品或套餐，不含套餐明细和带出的套餐外安装服务）
        /// </summary>
        public decimal TotalProductAmount { get; set; }
        /// <summary>
        /// 服务数量（指带出的安装服务，不含套餐内安装服务）
        /// </summary>
        public int ServiceNum { get; set; }
        /// <summary>
        /// 服务费（指带出的安装服务，不含套餐内安装服务）
        /// </summary>
        public decimal ServiceFee { get; set; }
        /// <summary>
        /// 运费
        /// </summary>
        public decimal DeliveryFee { get; set; }
        /// <summary>
        /// 总优惠金额
        /// </summary>
        public decimal TotalCouponAmount { get; set; }
        /// <summary>
        /// 实付款
        /// </summary>
        public decimal ActualAmount { get; set; }
        /// <summary>
        /// 支付方式（0未设置 1在线支付 2到店付款）
        /// </summary>
        public sbyte PayMethod { get; set; }
        /// <summary>
        /// 支付渠道（0未设置 1微信支付 2支付宝）
        /// </summary>
        public sbyte PayChannel { get; set; }
        /// <summary>
        /// 支付状态（0未支付 1已支付）
        /// </summary>
        public sbyte PayStatus { get; set; }
        /// <summary>
        /// 到账状态（0未到账 1已到账）
        /// </summary>
        public sbyte MoneyArriveStatus { get; set; }

        /// <summary>
        /// 门店Id
        /// </summary>
        public long ShopId { get; set; }

        /// <summary>
        /// 安装服务状态（0未安装 1已安装）
        /// </summary>
        public sbyte InstallStatus { get; set; }
        /// <summary>
        /// 点评状态（0待客户点评  1 客户已点评 2 客户已追评）
        /// </summary>
        public sbyte CommentStatus { get; set; }
       
        /// <summary>
        /// 删除标识（0否 1是）
        /// </summary>
        public sbyte IsDeleted { get; set; }
       
    }
}
