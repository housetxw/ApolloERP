using ApolloErp.Data.DapperExtensions.Att;
using System;
using System.Collections.Generic;
using System.Text;
using MySqlX.XDevAPI.Relational;
using Ae.Shop.Service.Common.Helper;

namespace Ae.Shop.Service.Dal.Model
{
    /// <summary>
    /// 门店信息
    /// </summary>
    [Table("shop")]
    public class ShopDO
    {
        /// <summary>
        /// 主键id
        /// </summary>
        [Key]
        [Column("id")]
        public long Id { get; set; }

        /// <summary>
        /// 门店简单名称
        /// </summary>
        [Column("simple_name")]
        [CompareDiffAttribute(Name = "门店简单名称")]
        public string SimpleName { get; set; } = string.Empty;

        /// <summary>
        /// 门店全称
        /// </summary>
        [Column("full_name")]
        [CompareDiffAttribute(Name = "门店全称")]
        public string FullName { get; set; } = string.Empty;

        /// <summary>
        /// 店公司名称
        /// </summary>
        [Column("shop_company_name")]
        [CompareDiffAttribute(Name = "店公司名称")]
        public string ShopCompanyName { get; set; } = string.Empty;

        /// <summary>
        /// 所属公司ID
        /// </summary>
        [Column("company_id")]
        [CompareDiffAttribute(Name = "所属公司ID")]
        public long CompanyId { get; set; }

        /// <summary>
        /// 营业类型 1 快修店
        /// </summary>
        [Column("business_type")]
        [CompareDiffAttribute(Name = "营业类型")]
        public int BusinessType { get; set; }

        /// <summary>
        /// 门店类型 1合作店 2自营店 4上门养车
        /// </summary>
        [Column("type")]
        [CompareDiffAttribute(Name = "门店类型")]
        public int Type { get; set; }

        /// <summary>
        /// 系统版本0 ：精简 1：旗舰 2：个人
        /// </summary>
        [Column("system_type")]
        [CompareDiffAttribute(Name = "系统版本")]
        public int SystemType { get; set; }


        /// <summary>
        /// 门店状态 0正常 1终止 2暂停 
        /// </summary>
        [Column("status")]
        [CompareDiffAttribute(Name = "门店状态")]
        public int Status { get; set; }

        /// <summary>
        /// 门店审核状态 0新建  1待审核 2已通过审核 3未通过审核
        /// </summary>
        [Column("check_status")]
        [CompareDiffAttribute(Name = "门店审核状态")]
        public int CheckStatus { get; set; }

        /// <summary>
        /// 上下架状态：0下架，1上架
        /// </summary>
        [Column("online")]
        public sbyte Online { get; set; }

        /// <summary>
        /// 品牌
        /// </summary>
        [Column("brand")]
        [CompareDiffAttribute(Name = "品牌")]
        public string Brand { get; set; } = string.Empty;

        /// <summary>
        /// 描述
        /// </summary>
        [Column("description")]
        [CompareDiffAttribute(Name = "描述")]
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// 省
        /// </summary>
        [Column("province_id")]
        [CompareDiffAttribute(Name = "省")]
        public string ProvinceId { get; set; } = string.Empty;

        /// <summary>
        /// 市
        /// </summary>
        [Column("city_id")]
        [CompareDiffAttribute(Name = "市")]
        public string CityId { get; set; } = string.Empty;

        /// <summary>
        /// 区
        /// </summary>
        [Column("district_id")]
        [CompareDiffAttribute(Name = "区")]
        public string DistrictId { get; set; } = string.Empty;

        /// <summary>
        /// 省名称
        /// </summary>
        [Column("province")]
        [CompareDiffAttribute(Name = "省名称")]
        public string Province { get; set; } = string.Empty;

        /// <summary>
        /// 市名称
        /// </summary>
        [Column("city")]
        [CompareDiffAttribute(Name = "市名称")]
        public string City { get; set; } = string.Empty;

        /// <summary>
        /// 区名称
        /// </summary>
        [Column("district")]
        [CompareDiffAttribute(Name = "区名称")]
        public string District { get; set; } = string.Empty;

        /// <summary>
        /// 详细地址
        /// </summary>
        [Column("address")]
        [CompareDiffAttribute(Name = "详细地址")]
        public string Address { get; set; } = string.Empty;

        /// <summary>
        /// 经度
        /// </summary>
        [Column("longitude")]
        [CompareDiffAttribute(Name = "经度")]
        public double Longitude { get; set; }

        /// <summary>
        /// 维度
        /// </summary>
        [Column("latitude")]
        [CompareDiffAttribute(Name = "维度")]
        public double Latitude { get; set; }

        /// <summary>
        /// 邮编
        /// </summary>
        [Column("post")]
        [CompareDiffAttribute(Name = "邮编")]
        public string Post { get; set; } = string.Empty;

        /// <summary>
        /// 联系人
        /// </summary>
        [Column("contact")]
        [CompareDiffAttribute(Name = "联系人")]
        public string Contact { get; set; } = string.Empty;

        /// <summary>
        /// 电话
        /// </summary>
        [Column("telephone")]
        [CompareDiffAttribute(Name = "电话")]
        public string Telephone { get; set; } = string.Empty;

        /// <summary>
        /// 手机
        /// </summary>
        [Column("mobile")]
        [CompareDiffAttribute(Name = "手机")]
        public string Mobile { get; set; } = string.Empty;

        /// <summary>
        /// 邮箱
        /// </summary>
        [Column("email")]
        [CompareDiffAttribute(Name = "邮箱")]
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// 对外电话
        /// </summary>
        [Column("external_phone")]
        [CompareDiffAttribute(Name = "对外电话")]
        public string ExternalPhone { get; set; } = string.Empty;

        /// <summary>
        /// 传真
        /// </summary>
        [Column("fax")]
        [CompareDiffAttribute(Name = "传真")]
        public string Fax { get; set; } = string.Empty;

        /// <summary>
        /// 负责人
        /// </summary>
        [Column("head")]
        [CompareDiffAttribute(Name = "负责人")]
        public string Head { get; set; } = string.Empty;

        /// <summary>
        /// 负责人电话
        /// </summary>
        [Column("head_phone")]
        [CompareDiffAttribute(Name = "负责人电话")]
        public string HeadPhone { get; set; } = string.Empty;

        /// <summary>
        /// 负责人邮箱
        /// </summary>
        [Column("head_email")]
        [CompareDiffAttribute(Name = "负责人邮箱")]
        public string HeadEmail { get; set; } = string.Empty;

        /// <summary>
        /// 对账联系人
        /// </summary>
        [Column("accounting_contact")]
        [CompareDiffAttribute(Name = "对账联系人")]
        public string AccountingContact { get; set; } = string.Empty;

        /// <summary>
        /// 对账联系人电话
        /// </summary>
        [Column("accounting_contact_phone")]
        [CompareDiffAttribute(Name = "对账联系人电话")]
        public string AccountingContactPhone { get; set; } = string.Empty;

        /// <summary>
        /// 应付账户
        /// </summary>
        [Column("payable_account")]
        [CompareDiffAttribute(Name = "应付账户")]
        public int PayableAccount { get; set; }

        /// <summary>
        /// 回款账户
        /// </summary>
        [Column("recievable_account")]
        [CompareDiffAttribute(Name = "回款账户")]
        public int RecievableAccount { get; set; }

        /// <summary>
        /// 对账人
        /// </summary>
        [Column("accounting_Person")]
        [CompareDiffAttribute(Name = "对账人")]
        public string AccountingPerson { get; set; } = string.Empty;

        /// <summary>
        /// 财务账号1
        /// </summary>
        [Column("rebate_account_one")]
        [CompareDiffAttribute(Name = "财务账号1")]
        public int RebateAccountOne { get; set; }

        /// <summary>
        /// 财务账号2
        /// </summary>
        [Column("rebate_account_two")]
        [CompareDiffAttribute(Name = "财务账号2")]
        public int RebateAccountTwo { get; set; }

        /// <summary>
        /// 财务账号3
        /// </summary>
        [Column("rebate_account_three")]
        [CompareDiffAttribute(Name = "财务账号3")]
        public int RebateAccountThree { get; set; }

        /// <summary>
        /// 对账周期
        /// </summary>
        [Column("accounting_period")]
        [CompareDiffAttribute(Name = "对账周期")]
        public string AccountingPeriod { get; set; } = string.Empty;

        /// <summary>
        /// 对账周期更新时间
        /// </summary>
        [Column("update_accounting_period_time")]
        [CompareDiffAttribute(Name = "对账周期更新时间")]
        public DateTime UpdateAccountingPeriodTime { get; set; } = new DateTime(1900, 1, 1);

        /// <summary>
        /// 对账方式
        /// </summary>
        [Column("reconciliation_mode")]
        [CompareDiffAttribute(Name = "对账方式")]
        public int ReconciliationMode { get; set; }

        /// <summary>
        /// 审核人
        /// </summary>
        [Column("examiner")]
        [CompareDiffAttribute(Name = "审核人")]
        public string Examiner { get; set; } = string.Empty;

        /// <summary>
        /// 审核人电话
        /// </summary>
        [Column("examiner_tel")]
        [CompareDiffAttribute(Name = "审核人电话")]
        public string ExaminerTel { get; set; } = string.Empty;

        /// <summary>
        /// 未通过审核原因
        /// </summary>
        [Column("failed_examined_reason")]
        [CompareDiffAttribute(Name = "未通过审核原因")]
        public string FailedExaminedReason { get; set; } = string.Empty;

        /// <summary>
        /// 提交人
        /// </summary>
        [Column("submitor")]
        [CompareDiffAttribute(Name = "提交人")]
        public string Submitor { get; set; } = string.Empty;

        /// <summary>
        /// 提交人电话
        /// </summary>
        [Column("submitor_tel")]
        [CompareDiffAttribute(Name = "提交人电话")]
        public string SubmitorTel { get; set; } = string.Empty;

        /// <summary>
        /// 门店老板姓名
        /// </summary>
        [Column("owner_name")]
        [CompareDiffAttribute(Name = "门店老板姓名")]
        public string OwnerName { get; set; } = string.Empty;

        /// <summary>
        /// 门店老板电话
        /// </summary>
        [Column("owner_phone")]
        [CompareDiffAttribute(Name = "门店老板电话")]
        public string OwnerPhone { get; set; } = string.Empty;

        /// <summary>
        /// 身份证正面
        /// </summary>
        [Column("id_card_front")]
        [CompareDiffAttribute(Name = "门店老板身份证正面")]
        public string IdCardFront { get; set; } = string.Empty;

        /// <summary>
        /// 身份证反面
        /// </summary>
        [Column("id_card_back")]
        [CompareDiffAttribute(Name = "门店老板身份证反面")]
        public string IdCardBack { get; set; } = string.Empty;

        /// <summary>
        /// 主管
        /// </summary>
        [Column("charge")]
        [CompareDiffAttribute(Name = "主管")]
        public string Charge { get; set; } = string.Empty;

        /// <summary>
        /// 主管电话
        /// </summary>
        [Column("charge_phone")]
        [CompareDiffAttribute(Name = "主管电话")]
        public string ChargePhone { get; set; } = string.Empty;

        /// <summary>
        /// 品类
        /// </summary>
        [Column("category")]
        [CompareDiffAttribute(Name = "品类")]
        public string Category { get; set; } = string.Empty;

        /// <summary>
        /// 服务类型
        /// </summary>
        [Column("service_type")]
        [CompareDiffAttribute(Name = "服务类型")]
        public int ServiceType { get; set; }

        /// <summary>
        /// 门店等级
        /// </summary>
        [Column("level")]
        [CompareDiffAttribute(Name = "门店等级")]
        public int Level { get; set; }

        /// <summary>
        /// 总评分
        /// </summary>
        [Column("score")]
        [CompareDiffAttribute(Name = "总评分")]
        public decimal Score { get; set; }

        /// <summary>
        /// 总订单数
        /// </summary>
        [Column("total_order")]
        [CompareDiffAttribute(Name = "总订单数")]
        public int TotalOrder { get; set; }

        /// <summary>
        /// 好评率（单位为%）
        /// </summary>
        [Column("applause_rate")]
        [CompareDiffAttribute(Name = "好评率")]
        public decimal ApplauseRate { get; set; }

        /// <summary>
        /// 快速排队小程序码
        /// </summary>
        [Column("applet_code")]
        [CompareDiffAttribute(Name = "快速排队小程序码")]
        public string AppletCode { get; set; } = string.Empty;

        /// <summary>
        /// 门店小程序码
        /// </summary>
        [Column("shop_applet_code")]
        [CompareDiffAttribute(Name = "门店小程序码")]
        public string ShopAppletCode { get; set; } = string.Empty;

        /// <summary>
        /// 门店标签
        /// </summary>
        [Column("tag_name")]
        [CompareDiffAttribute(Name = "门店标签")]
        public string TagName { get; set; } = string.Empty;

        /// <summary>
        /// 商户性质 0：门店加盟 1：平台先生2：配件改装 3：工厂直销
        /// </summary>
        [Column("nature")]
        [CompareDiffAttribute(Name = "商户性质")]
        public sbyte Nature { get; set; }

        /// <summary>
        /// 押金额度
        /// </summary>
        [Column("deposit_amount")]
        [CompareDiffAttribute(Name = "押金额度")]
        public decimal DepositAmount { get; set; }

        /// <summary>
        /// 是否删除 0未删除 1已删除
        /// </summary>
        [Column("is_deleted")]
        [CompareDiffAttribute(Name = "是否删除")]
        public int IsDeleted { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        [Column("create_by")]
        [CompareDiffAttribute(Name = "创建人")]
        public string CreateBy { get; set; } = string.Empty;

        /// <summary>
        /// 创建时间
        /// </summary>
        [Column("create_time")]
        [CompareDiffAttribute(Name = "创建时间")]
        public DateTime CreateTime { get; set; } = new DateTime(1900, 1, 1);

        /// <summary>
        /// 修改人
        /// </summary>
        [Column("update_by")]
        [CompareDiffAttribute(Name = "修改人")]
        public string UpdateBy { get; set; } = string.Empty;

        /// <summary>
        /// 修改时间
        /// </summary>
        [Column("update_time")]
        [CompareDiffAttribute(Name = "修改时间")]
        public DateTime UpdateTime { get; set; } = new DateTime(1900, 1, 1);

 
    }
}

