using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.ShopOrder.Service.Core.Request
{
    public class EmployeePerformanceRequest
    {
        //[Required(ErrorMessage = "门店ID不能为空")]
        public long ShopId { get; set; }

        public List<long> ShopIds { get; set; } = new List<long>();
        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderNo { get; set; } = string.Empty;

        /// <summary>
        /// 搜索关机键字 持搜索员工姓名（模糊搜索）+手机号（精确搜索）
        /// </summary>
        public string Key { get; set; } = string.Empty;

        /// <summary>
        /// 展示类型( 1套餐卡，2次数项目，3金额项目)
        /// </summary>
        public sbyte TabType { get; set; }
        /// <summary>
        /// 订单类型（0未设置 1轮胎 2保养 3美容 4 钣金喷漆 5 维修改装 6 其他  ）
        /// </summary>
        public sbyte OrderType { get; set; }
        /// <summary>
        /// 产生类型（0普通产生 1购买核销码产生 2使用核销码产生 3再次购买产生 4追加下单产生）
        /// </summary>
        public sbyte ProduceType { get; set; }
        /// <summary>
        /// 产品名称
        /// </summary>
        public string ProductName { get; set; } = string.Empty;

        /// <summary>
        ///  开始日期
        /// </summary>
        //[Required(ErrorMessage = "开始日期不能为空")]
        public DateTime StartDate { get; set; }

        /// <summary>
        ///  结束日期
        /// </summary>
        //[Required(ErrorMessage = "结束日期不能为空")]
        public DateTime EndDate { get; set; }
    }

}
