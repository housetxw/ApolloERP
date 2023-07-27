using System;
using System.Collections.Generic;
using System.Text;
using ApolloErp.Data.DapperExtensions.Att;
using MySqlX.XDevAPI.Relational;

namespace Ae.Shop.Service.Dal.Model
{
    [Table("employee_performance_report")]
    public class EmployeePerformanceReportDO
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        [Column("id")]
        public long Id { get; set; }
        /// <summary>
        /// 门店编号
        /// </summary>
        [Column("shop_id")]
        public long ShopId { get; set; }
        /// <summary>
        /// 员工编号
        /// </summary>
        [Column("employee_id")]
        public string EmployeeId { get; set; } = string.Empty;
        /// <summary>
        /// 员工名
        /// </summary>
        [Column("employee_name")]
        public string EmployeeName { get; set; } = string.Empty;
        /// <summary>
        /// 员工手机号
        /// </summary>
        [Column("employee_phone")]
        public string EmployeePhone { get; set; } = string.Empty;
        /// <summary>
        /// 安装绩效
        /// </summary>
        [Column("install_point")]
        public decimal InstallPoint { get; set; }
        /// <summary>
        /// 新客绩效
        /// </summary>
        [Column("pull_new_point")]
        public decimal PullNewPoint { get; set; }
        /// <summary>
        /// 新客首消费绩效
        /// </summary>
        [Column("cunsume_point")]
        public decimal CunsumePoint { get; set; }
        /// <summary>
        /// 总绩效
        /// </summary>
        [Column("total_point")]
        public decimal TotalPoint { get; set; }
        /// <summary>
        /// 生成记录时间
        /// </summary>
        [Column("report_time")]
        public DateTime ReportTime { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 备注
        /// </summary>
        [Column("remark")]
        public string Remark { get; set; } = string.Empty;
        /// <summary>
        /// 删除标识 0未删除 1已删除
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