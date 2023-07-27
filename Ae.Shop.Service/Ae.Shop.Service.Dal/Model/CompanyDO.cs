using System;
using System.Collections.Generic;
using ApolloErp.Data.DapperExtensions.Att;
using MySqlX.XDevAPI.Relational;
using System.Text;

namespace Ae.Shop.Service.Dal.Model
{
    [Table("company")]
    public class CompanyDO
    {
        /// <summary>
        /// 公司id
        /// </summary>
        [Key]
        [Column("id")]
        public long Id { get; set; }
        /// <summary>
        /// 公司简称
        /// </summary>
        [Column("simple_name")]
        public string SimpleName { get; set; } = string.Empty;
        /// <summary>
        /// 公司全称
        /// </summary>
        [Column("name")]
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// 父级公司id
        /// </summary>
        [Column("parent_id")]
        public long ParentId { get; set; }
        /// <summary>
        /// 公司编号
        /// </summary>
        [Column("code")]
        public string Code { get; set; } = string.Empty;
        /// <summary>
        /// 公司状态 0待提交 1审核中 2正常 3注销 4审核未通过
        /// </summary>
        [Column("status")]
        public sbyte Status { get; set; }
        /// <summary>
        /// 类型；0：公司；1.门店；2：仓库；
        /// </summary>
        [Column("type")]
        public sbyte Type { get; set; }
        /// <summary>
        /// 公司等级 0一级公司 1二级公司
        /// </summary>
        [Column("level")]
        public sbyte Level { get; set; }
        /// <summary>
        /// 公司所在省Id
        /// </summary>
        [Column("province_id")]
        public string ProvinceId { get; set; } = string.Empty;
        /// <summary>
        /// 公司所在市Id
        /// </summary>
        [Column("city_id")]
        public string CityId { get; set; } = string.Empty;
        /// <summary>
        /// 公司所在区Id
        /// </summary>
        [Column("district_id")]
        public string DistrictId { get; set; } = string.Empty;
        /// <summary>
        /// 省名
        /// </summary>
        [Column("province")]
        public string Province { get; set; } = string.Empty;
        /// <summary>
        /// 市名
        /// </summary>
        [Column("city")]
        public string City { get; set; } = string.Empty;
        /// <summary>
        /// 区名
        /// </summary>
        [Column("district")]
        public string District { get; set; } = string.Empty;
        /// <summary>
        /// 地址
        /// </summary>
        [Column("address")]
        public string Address { get; set; } = string.Empty;
        /// <summary>
        /// 联系电话
        /// </summary>
        [Column("mobile")]
        public string Mobile { get; set; } = string.Empty;
        /// <summary>
        /// 邮箱
        /// </summary>
        [Column("email")]
        public string Email { get; set; } = string.Empty;
        /// <summary>
        /// 公司负责人
        /// </summary>
        [Column("head")]
        public string Head { get; set; } = string.Empty;
        /// <summary>
        /// 负责人电话
        /// </summary>
        [Column("head_phone")]
        public string HeadPhone { get; set; } = string.Empty;
        /// <summary>
        /// 注册资金
        /// </summary>
        [Column("register_money")]
        public decimal RegisterMoney { get; set; }
        /// <summary>
        /// 工商注册日期
        /// </summary>
        [Column("register_time")]
        public DateTime RegisterTime { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 公司法人
        /// </summary>
        [Column("legal_person")]
        public string LegalPerson { get; set; } = string.Empty;
        /// <summary>
        /// 法人联系电话
        /// </summary>
        [Column("legal_person_phone")]
        public string LegalPersonPhone { get; set; } = string.Empty;
        /// <summary>
        /// 营业执照
        /// </summary>
        [Column("business_license")]
        public string BusinessLicense { get; set; } = string.Empty;
        /// <summary>
        /// 开户银行
        /// </summary>
        [Column("opening_bank")]
        public string OpeningBank { get; set; } = string.Empty;
        /// <summary>
        /// 银行账号
        /// </summary>
        [Column("bank_account")]
        public string BankAccount { get; set; } = string.Empty;
        /// <summary>
        /// 开户许可证
        /// </summary>
        [Column("account_opening_license")]
        public string AccountOpeningLicense { get; set; } = string.Empty;
        /// <summary>
        /// 公司介绍
        /// </summary>
        [Column("introduction")]
        public string Introduction { get; set; } = string.Empty;
        /// <summary>
        /// 执照 （废弃）
        /// </summary>
        [Column("license")]
        public sbyte License { get; set; }
        /// <summary>
        /// 审核人
        /// </summary>
        [Column("examiner")]
        public string Examiner { get; set; } = string.Empty;
        /// <summary>
        /// 未通过审核原因
        /// </summary>
        [Column("failed_examined_reason")]
        public string FailedExaminedReason { get; set; } = string.Empty;

        /// <summary>
        /// 支付押金额度
        /// </summary>
        [Column("deposit_amount")]
        public decimal DepositAmount { get; set; }

        /// <summary>
        /// 加盟等级
        /// </summary>
        [Column("service_level")]
        public string ServiceLevel { get; set; } = string.Empty;

        /// <summary>
        /// 供应商编号
        /// </summary>
        [Column("vender_id")]
        public long VenderId { get; set; } = 0;

        /// <summary>
        /// 1平台公司，2普通公司，3区域合伙公司
        /// </summary>
        [Column("system_type")]
        public int SystemType { get; set; }

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
