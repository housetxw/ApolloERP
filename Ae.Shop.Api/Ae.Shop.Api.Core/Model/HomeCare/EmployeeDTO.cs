using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Api.Core.Model
{
    public class EmployeeDTO
    {
        /// <summary>
        /// 员工id
        /// </summary>
        public string Id { get; set; } = string.Empty;
        /// <summary>
        /// 账号Id
        /// </summary>
        public string AccountId { get; set; } = string.Empty;
        /// <summary>
        /// 姓名（登录token中已用，不可修改）
        /// </summary>
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// 邮箱地址
        /// </summary>
        public string Email { get; set; } = string.Empty;
        /// <summary>
        /// 手机号
        /// </summary>
        public string Mobile { get; set; } = string.Empty;
        /// <summary>
        /// 性别（0保密 1男 2女）
        /// </summary>
        public sbyte Gender { get; set; }
        /// <summary>
        /// 员工号
        /// </summary>
        public string Number { get; set; } = string.Empty;
        /// <summary>
        /// 类型；0：公司；1：门店；2：仓库；
        /// </summary>
        public sbyte Type { get; set; }
        /// <summary>
        /// 所属单位Id
        /// </summary>
        public uint OrganizationId { get; set; }
        /// <summary>
        /// 部门id

        /// </summary>
        public int DepartmentId { get; set; }
        /// <summary>
        /// 职位id

        /// </summary>
        public int JobId { get; set; }
        /// <summary>
        /// 职级 0初级 1中级 2高级
        /// </summary>
        public sbyte Level { get; set; }
        /// <summary>
        /// 工种id
        /// </summary>
        public long WorkKindId { get; set; }
        /// <summary>
        /// 身份证号码
        /// </summary>
        public string Identity { get; set; } = string.Empty;
        /// <summary>
        /// 微信号
        /// </summary>
        public string WeChat { get; set; } = string.Empty;
        /// <summary>
        /// QQ号
        /// </summary>
        public string Qq { get; set; } = string.Empty;
        /// <summary>
        /// 地址
        /// </summary>
        public string Address { get; set; } = string.Empty;
        /// <summary>
        /// 头像
        /// </summary>
        public string Avatar { get; set; } = string.Empty;
        /// <summary>
        /// 评分
        /// </summary>
        public decimal Score { get; set; }
        /// <summary>
        /// 员工证书照片地址，多张以英文 ‘;’ 隔开
        /// </summary>
        public string QualificationCertificate { get; set; } = string.Empty;
        /// <summary>
        /// 简介
        /// </summary>
        public string Description { get; set; } = string.Empty;
        /// <summary>
        /// 离职类型 0：在职 1：自动离职；2：辞退
        /// </summary>
        public sbyte DimissionType { get; set; }
        /// <summary>
        /// 离职原因
        /// </summary>
        public string DimissionCause { get; set; } = string.Empty;
        /// <summary>
        /// 删除标识 0未删除 1已删除
        /// </summary>
        public sbyte IsDeleted { get; set; }
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