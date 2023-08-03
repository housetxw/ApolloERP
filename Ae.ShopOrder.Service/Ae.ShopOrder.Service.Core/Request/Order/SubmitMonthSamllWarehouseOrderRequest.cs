using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.ShopOrder.Service.Core.Request.Order
{
    public class SubmitMonthSamllWarehouseOrderRequest
    {
        public List<string> OrderNos { get; set; }
        /// <summary>
        /// 渠道 1C端 2门店
        /// </summary>
        public sbyte Channel { get; set; }
        /// <summary>
        /// 终端类型（0未设置 1小程序 2Android-App 3iOS-App 4S网站 5官网 6 boss）
        /// </summary>
        public sbyte TerminalType { get; set; }
        /// <summary>
        /// 入口类型（0未设置 1轮胎 2保养 3美容 4 钣金维修 5 汽车改装 6 其他）
        /// </summary>
        public sbyte OrderType { get; set; }
        /// <summary>
        /// 产生类型（0普通产生 1购买核销码产生 2使用核销码产生 3再次购买产生 4追加下单产生）
        /// </summary>
        //[Range(0, 4, ErrorMessage = "产生类型范围错误")]
        public sbyte ProduceType { get; set; }
        /// <summary>
        /// 用户Id
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// 选择的门店Id
        /// </summary>
        public long ShopId { get; set; }

        /// <summary>
        /// 收货人姓名
        /// </summary>
        public string ReceiverName { get; set; }
        /// <summary>
        /// 收货人电话
        /// </summary>
        public string ReceiverPhone { get; set; }

        /// <summary>
        /// 下单操作人
        /// </summary>
        //[Required(ErrorMessage = "下单操作人不能为空")]
        public string CreatedBy { get; set; } = string.Empty;

        /// <summary>
        /// 代理者UserId
        /// </summary>
        public string DelegateUserId { get; set; }

        /// <summary>
        /// 代理者名称
        /// </summary>
        public string DelegateUserName { get; set; }

        /// <summary>
        /// 客户下单的门店
        /// </summary>
        public long DelegateUserShopId { get; set; }
    }
}
