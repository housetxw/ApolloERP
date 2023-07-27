using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Ae.Shop.Service.Dal.Model
{
    [Table("employee_group")]
    public class EmployeeGroupDO
    {
        /// <summary>
        /// 员工id
        /// </summary>
        [Key]
        [Column("id")]
        public long Id { get; set; } = 0;
        /// <summary>
        /// 
        /// </summary>
        [Column("employee_id")]
        public string EmployeeId { get; set; } = string.Empty;
        /// <summary>
        /// 姓名（登录token中已用，不可修改）
        /// </summary>
        [Column("name")]
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// 手机号
        /// </summary>
        [Column("mobile")]
        public string Mobile { get; set; } = string.Empty;
        /// <summary>
        /// 
        /// </summary>
        [Column("group_name")]
        public string GroupName { get; set; } = string.Empty;
        /// <summary>
        /// 
        /// </summary>
        [Column("group_leader")]
        public string GroupLeader { get; set; } = string.Empty;
        /// <summary>
        /// 门店Id
        /// </summary>
        [Column("shop_id")]
        public long ShopId { get; set; }
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
