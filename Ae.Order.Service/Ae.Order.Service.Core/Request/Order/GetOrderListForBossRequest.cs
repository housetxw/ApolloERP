using Ae.Order.Service.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Order.Service.Core.Request
{
    public class GetOrderListForBossRequest : BasePageRequest
    {
        /// <summary>
        /// 模糊搜索条件类型（0未设置 1订单编号 2商品名称 3客户姓名）
        /// </summary>
        public sbyte LikeType { get; set; }
        /// <summary>
        /// 模糊搜索条件值
        /// </summary>
        public string LikeValue { get; set; }

        /// <summary>
        /// 订单状态（0全部 10已提交 20已确认 30已完成 40已取消）
        /// </summary>
        public sbyte OrderStatus { get; set; }
        /// <summary>
        /// 订单渠道（0全部 1C端 2门店）
        /// </summary>
        public sbyte OrderChannel { get; set; }
        /// <summary>
        /// 配送方式（0未设置 1自配 2快递）
        /// </summary>
        public sbyte DeliveryMethod { get; set; }
        /// <summary>
        /// 配送状态（-1全部 0未配送 1已配送）
        /// </summary>
        public sbyte DeliveryStatus { get; set; } = -1;
        /// <summary>
        /// 是否需要安装服务（-1全部 0不需要 1需要）
        /// </summary>
        public sbyte IsNeedInstall { get; set; } = -1;
        /// <summary>
        /// 安装服务状态（-1全部 0未安装 1已安装）
        /// </summary>
        public sbyte InstallStatus { get; set; } = -1;
        /// <summary>
        /// 支付方式（0全部 1在线支付 2到店付款）
        /// </summary>
        public sbyte PayMethod { get; set; }
        /// <summary>
        /// 支付状态（-1全部 0未支付 1已支付）
        /// </summary>
        public sbyte PayStatus { get; set; } = -1;
        /// <summary>
        /// 订单类型（0全部 1轮胎 2保养 3美容）
        /// </summary>
        public sbyte OrderType { get; set; }

        /// <summary>
        /// 日期时间类型（0未设置 1创建时间）
        /// </summary>
        public sbyte DateTimeType { get; set; }
        /// <summary>
        /// 开始日期时间
        /// </summary>
        public DateTime StartDateTime { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 截止日期时间
        /// </summary>
        public DateTime EndDateTime { get; set; } = new DateTime(1900, 1, 1);

        /// <summary>
        /// 联系电话
        /// </summary>
        public string ContactPhone { get; set; }

        /// <summary>
        /// 订单分类
        /// </summary>
        public int ProductType { get; set; } = -1;

        public int ShopId { get; set; }

        public string InstallStartTime { get; set; }

        public string InstallEndTime { get; set; }
    }
}
