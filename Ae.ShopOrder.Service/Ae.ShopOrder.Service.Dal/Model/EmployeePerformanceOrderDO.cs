using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Ae.ShopOrder.Service.Dal.Model
{
    [Table("employee_performance_order")]
    public class EmployeePerformanceOrderDO
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        [Column("id")]
        public long Id { get; set; }
        /// <summary>
        /// 门店Id
        /// </summary>
        [Column("shop_id")]
        public long ShopId { get; set; }
        /// <summary>
        /// 订单号
        /// </summary>
        [Column("order_no")]
        public string OrderNo { get; set; } = string.Empty;
        /// <summary>
        /// 安装时间
        /// </summary>
        [Column("install_time")]
        public DateTime InstallTime { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 技师Id
        /// </summary>
        [Column("employee_id")]
        public string EmployeeId { get; set; } = string.Empty;
        /// <summary>
        /// 技师姓名
        /// </summary>
        [Column("employee_name")]
        public string EmployeeName { get; set; } = string.Empty;
        /// <summary>
        /// 技师手机
        /// </summary>
        [Column("employee_phone")]
        public string EmployeePhone { get; set; } = string.Empty;
        /// <summary>
        /// 展示类型( 1套餐卡，2次数项目，3金额项目)
        /// </summary>
        [Column("tab_type")]
        public sbyte TabType { get; set; }
        /// <summary>
        /// 订单类型（0未设置 1轮胎 2保养 3美容 4 钣金喷漆 5 维修改装 6 其他  ）
        /// </summary>
        [Column("order_type")]
        public sbyte OrderType { get; set; }
        /// <summary>
        /// 产生类型（0普通产生 1购买核销码产生 2使用核销码产生 3再次购买产生 4追加下单产生）
        /// </summary>
        [Column("produce_type")]
        public sbyte ProduceType { get; set; }
        /// <summary>
        /// 产品Id
        /// </summary>
        [Column("product_id")]
        public string ProductId { get; set; } = string.Empty;
        /// <summary>
        /// 产品名称
        /// </summary>
        [Column("product_name")]
        public string ProductName { get; set; } = string.Empty;
        /// <summary>
        /// 总数量（指乘以购买数量后）
        /// </summary>
        [Column("total_number")]
        public decimal TotalNumber { get; set; }
        /// <summary>
        /// 总金额（指乘以购买数量后）
        /// </summary>
        [Column("total_amount")]
        public decimal TotalAmount { get; set; }
        /// <summary>
        /// 分摊优惠金额（指乘以购买数量后）
        /// </summary>
        [Column("share_discount_amount")]
        public decimal ShareDiscountAmount { get; set; }
        /// <summary>
        /// 实际支付金额（指乘以购买数量后）
        /// </summary>
        [Column("actual_pay_amount")]
        public decimal ActualPayAmount { get; set; }
        /// <summary>
        /// 总成本价（实物取自库存）（指乘以购买数量后）
        /// </summary>
        [Column("total_cost_price")]
        public decimal TotalCostPrice { get; set; }
        /// <summary>
        /// 服务结算金额(只含服务产品，指乘以购买数量后)
        /// </summary>
        [Column("settlement_amount")]
        public decimal SettlementAmount { get; set; }
        /// <summary>
        /// 毛利金额
        /// </summary>
        [Column("maoli")]
        public decimal Maoli { get; set; }
        /// <summary>
        /// 派工比例
        /// </summary>
        [Column("percent")]
        public Decimal Percent { get; set; }
        /// <summary>
        /// 安装绩效
        /// </summary>
        [Column("install_point")]
        public decimal InstallPoint { get; set; }
        /// <summary>
        /// 销售绩效
        /// </summary>
        [Column("sale_point")]
        public decimal SalePoint { get; set; }
        /// <summary>
        /// 总绩效
        /// </summary>
        [Column("total_point")]
        public decimal TotalPoint { get; set; }

        /// <summary>
        /// 绩效类型(0默认， 1单品，2小品类)
        /// </summary>
        [Column("performance_type")]
        public sbyte PerformanceType { get; set; }
        /// <summary>
        /// 类型参数（多个用逗号隔开）
        /// </summary>
        [Column("type_value")]
        public string TypeValue { get; set; } 
        /// <summary>
        /// 配置类型(比例 1/金额2)
        /// </summary>
        [Column("config_type")]
        public sbyte ConfigType { get; set; }
        /// <summary>
        /// 比例/金额
        /// </summary>
        [Column("config_point")]
        public decimal ConfigPoint { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [Column("remark")]
        public string Remark { get; set; } = string.Empty;
        /// <summary>
        /// 删除标识
        /// </summary>
        [Column("is_deleted")]
        public sbyte IsDeleted { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        [Column("create_by")]
        public string CreateBy { get; set; } = string.Empty;
        /// <summary>
        /// 创建时间
        /// </summary>
        [Column("create_time")]
        public DateTime CreateTime { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 修改人
        /// </summary>
        [Column("update_by")]
        public string UpdateBy { get; set; } = string.Empty;
        /// <summary>
        /// 修改时间
        /// </summary>
        [Column("update_time")]
        public DateTime UpdateTime { get; set; } = new DateTime(1900, 1, 1);
    }
}
