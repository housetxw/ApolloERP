using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Api.Core.Model
{
    public class EmployeePerformanceOrderDTO
    {
        /// <summary>
        /// 主键
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 门店Id
        /// </summary>
        public long ShopId { get; set; }
        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderNo { get; set; } = string.Empty;
        /// <summary>
        /// 技师Id
        /// </summary>
        public string EmployeeId { get; set; } = string.Empty;
        /// <summary>
        /// 技师姓名
        /// </summary>
        public string EmployeeName { get; set; } = string.Empty;
        /// <summary>
        /// 技师手机
        /// </summary>
        public string EmployeePhone { get; set; } = string.Empty;
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
        /// 产品Id
        /// </summary>
        public string ProductId { get; set; } = string.Empty;
        /// <summary>
        /// 产品名称
        /// </summary>
        public string ProductName { get; set; } = string.Empty;
        /// <summary>
        /// 数量
        /// </summary>
        public decimal Number { get; set; }
        /// <summary>
        /// 毛利金额
        /// </summary>
        public decimal Amount { get; set; }
        /// <summary>
        /// 安装绩效
        /// </summary>
        public decimal InstallPoint { get; set; }
        /// <summary>
        /// 销售绩效
        /// </summary>
        public decimal SalePoint { get; set; }
        /// <summary>
        /// 总绩效
        /// </summary>
        public decimal TotalPoint { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; } = string.Empty;
        /// <summary>
        /// 创建人
        /// </summary>
        public string CreateBy { get; set; } = string.Empty;
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 修改人
        /// </summary>
        public string UpdateBy { get; set; } = string.Empty;
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime UpdateTime { get; set; } = new DateTime(1900, 1, 1);
    }
}
