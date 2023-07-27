using System;
using System.Collections.Generic;
using System.Text;
using ApolloErp.Data.DapperExtensions.Att;
using Ae.Shop.Service.Core.Enums;

namespace Ae.Shop.Service.Dal.Model
{
    [Table("technician")]
    public class TechnicianDO
    {
        /// <summary>
        /// 技师id
        /// </summary>
        [Key]
        [Column("id")]
        public string Id { get; set; }

        /// <summary>
        /// 账号Id
        /// </summary>
        [Column("account_id")]
        public string AccountId { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        [Column("name")]
        public string Name { get; set; }

        /// <summary>
        /// 邮箱地址
        /// </summary>
        [Column("email")]
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// 手机号
        /// </summary>
        [Column("mobile")]
        public string Mobile { get; set; }
        /// <summary>
        /// 性别 （0保密 1男 2女）
        /// </summary>
        [Column("gender")]
        public sbyte Gender { get; set; }

        /// <summary>
        /// 微信号
        /// </summary>
        [Column("we_chat")]
        public string WeChat { get; set; } = string.Empty;

        /// <summary>
        /// QQ号
        /// </summary>
        [Column("qq")]
        public string QQ { get; set; } = string.Empty;

        /// <summary>
        /// 员工号
        /// </summary>
        [Column("number")]
        public string Number { get; set; } = string.Empty;

        /// <summary>
        /// 类型；0：公司；1：门店；2：仓库；
        /// </summary>
        [Column("type")]
        public sbyte Type { get; set; }

        /// <summary>
        /// 所属单位Id
        /// </summary>
        [Column("organization_id")]
        public long OrganizationId { get; set; }

        /// <summary>
        /// 部门id
        /// </summary>
        [Column("department_id")]
        public int DepartmentId { get; set; } 
        /// <summary>
        /// 职级
        /// </summary>
        [Column("level")]
        public int Level { get; set; } 
        /// <summary>
        /// 职级名称
        /// </summary>
        [Column("level_name")]
        public string LevelName { get; set; } = string.Empty;
        /// <summary>
        /// 工种id
        /// </summary>
        [Column("work_kind_id")]
        public int WorkKindId { get; set; } 
        /// <summary>
        /// 工种级别
        /// </summary>
        [Column("work_kind_level")]
        public int WorkKindLevel { get; set; } 
        /// <summary>
        /// 职位id
        /// </summary>
        [Column("job_id")]
        public int JobId { get; set; }

        /// <summary>
        /// 身份证号码
        /// </summary>
        [Column("identity")]
        public string Identity { get; set; } = string.Empty;

        /// <summary>
        /// 地址
        /// </summary>
        [Column("address")]
        public string Address { get; set; } = string.Empty;

        /// <summary>
        /// 头像
        /// </summary>
        [Column("avatar")]
        public string Avatar { get; set; } = string.Empty;

        /// <summary>
        /// 评分
        /// </summary>
        [Column("score")]
        public decimal Score { get; set; }
        /// <summary>
        /// 员工证书照片地址，多张以英文 ‘;’ 隔开
        /// </summary>
        [Column("qualification_certificate")]
        public string QualificationCertificate { get; set; } = string.Empty;
        /// <summary>
        /// 简介
        /// </summary>
        [Column("description")]
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// 离职类型 0：在职 1：自动离职；2：辞退
        /// </summary>
        [Column("dimission_type")]
        public sbyte DimissionType { get; set; }

        /// <summary>
        /// 离职原因
        /// </summary>
        [Column("dimission_cause")]
        public string DimissionCause { get; set; } = string.Empty;

        /// <summary>
        /// 删除标识 0未删除 1已删除
        /// </summary>
        [Column("is_deleted")]
        public sbyte IsDeleted { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        [Column("create_by")]
        public string CreateBy { get; set; }

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
