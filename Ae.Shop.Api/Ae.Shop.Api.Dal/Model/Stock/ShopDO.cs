using System;
using System.Collections.Generic;
using System.Text;
using ApolloErp.Data.DapperExtensions.Att;
namespace Ae.Shop.Api.Dal.Model
{
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
        /// 简单名称
        /// </summary>
        [Column("simple_name")]
        public string SimpleName { get; set; } = string.Empty;
        /// <summary>
        /// 店全称
        /// </summary>
        [Column("full_name")]
        public string FullName { get; set; } = string.Empty;
        /// <summary>
        /// 店公司名称
        /// </summary>
        [Column("shop_company_name")]
        public string ShopCompanyName { get; set; } = string.Empty;
        /// <summary>
        /// 公司id
        /// </summary>
        [Column("company_id")]
        public long CompanyId { get; set; }
        /// <summary>
        /// 营业类型 1 4S店，2 快修店，3修理厂
        /// </summary>
        [Column("business_type")]
        public byte BusinessType { get; set; }
        /// <summary>
        /// 门店类型 1合作店 2直营店 4上门养车 8认证店 16工作室
        /// </summary>
        [Column("type")]
        public int Type { get; set; }
        /// <summary>
        /// 门店状态 0正常 1终止 2暂停
        /// </summary>
        [Column("status")]
        public sbyte Status { get; set; }
        /// <summary>
        /// 上下架状态：0下架，1上架
        /// </summary>
        [Column("online")]
        public sbyte Online { get; set; }
        /// <summary>
        /// 品牌
        /// </summary>
        [Column("brand")]
        public string Brand { get; set; } = string.Empty;
        /// <summary>
        /// 描述
        /// </summary>
        [Column("description")]
        public string Description { get; set; } = string.Empty;
        /// <summary>
        /// 省
        /// </summary>
        [Column("province_id")]
        public string ProvinceId { get; set; } = string.Empty;
        /// <summary>
        /// 市
        /// </summary>
        [Column("city_id")]
        public string CityId { get; set; } = string.Empty;
        /// <summary>
        /// 区
        /// </summary>
        [Column("district_id")]
        public string DistrictId { get; set; } = string.Empty;
        /// <summary>
        /// 省名称
        /// </summary>
        [Column("province")]
        public string Province { get; set; } = string.Empty;
        /// <summary>
        /// 市名称
        /// </summary>
        [Column("city")]
        public string City { get; set; } = string.Empty;
        /// <summary>
        /// 区县名称
        /// </summary>
        [Column("district")]
        public string District { get; set; } = string.Empty;
        /// <summary>
        /// 详细地址
        /// </summary>
        [Column("address")]
        public string Address { get; set; } = string.Empty;
        /// <summary>
        /// 经度
        /// </summary>
        [Column("longitude")]
        public double Longitude { get; set; }
        /// <summary>
        /// 维度
        /// </summary>
        [Column("latitude")]
        public double Latitude { get; set; }
        /// <summary>
        /// 邮编
        /// </summary>
        [Column("post")]
        public string Post { get; set; } = string.Empty;
        /// <summary>
        /// 联系人
        /// </summary>
        [Column("contact")]
        public string Contact { get; set; } = string.Empty;
        /// <summary>
        /// 电话
        /// </summary>
        [Column("telephone")]
        public string Telephone { get; set; } = string.Empty;
        /// <summary>
        /// 手机
        /// </summary>
        [Column("mobile")]
        public string Mobile { get; set; } = string.Empty;
        /// <summary>
        /// 邮箱
        /// </summary>
        [Column("email")]
        public string Email { get; set; } = string.Empty;
        /// <summary>
        /// 对外电话
        /// </summary>
        [Column("external_phone")]
        public string ExternalPhone { get; set; } = string.Empty;
        /// <summary>
        /// 传真
        /// </summary>
        [Column("fax")]
        public string Fax { get; set; } = string.Empty;
        /// <summary>
        /// 运营负责人（专职顾问）
        /// </summary>
        [Column("head")]
        public string Head { get; set; } = string.Empty;
        /// <summary>
        /// 运营负责人电话
        /// </summary>
        [Column("head_phone")]
        public string HeadPhone { get; set; } = string.Empty;
        /// <summary>
        /// 运营负责人邮箱
        /// </summary>
        [Column("head_email")]
        public string HeadEmail { get; set; } = string.Empty;
        /// <summary>
        /// 对账联系人
        /// </summary>
        [Column("accounting_contact")]
        public string AccountingContact { get; set; } = string.Empty;
        /// <summary>
        /// 对账联系人电话
        /// </summary>
        [Column("accounting_contact_phone")]
        public string AccountingContactPhone { get; set; } = string.Empty;
        /// <summary>
        /// 对账人
        /// </summary>
        [Column("accounting_person")]
        public string AccountingPerson { get; set; } = string.Empty;
        /// <summary>
        /// 对账周期
        /// </summary>
        [Column("accounting_period")]
        public string AccountingPeriod { get; set; } = string.Empty;
        /// <summary>
        /// 对账周期更新时间
        /// </summary>
        [Column("update_accounting_period_time")]
        public DateTime UpdateAccountingPeriodTime { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 应付账户
        /// </summary>
        [Column("payable_account")]
        public int PayableAccount { get; set; }
        /// <summary>
        /// 回款账户
        /// </summary>
        [Column("recievable_account")]
        public int RecievableAccount { get; set; }
        /// <summary>
        /// 财务账号（月返）
        /// </summary>
        [Column("rebate_account_one")]
        public int RebateAccountOne { get; set; }
        /// <summary>
        /// 财务账号（季返）
        /// </summary>
        [Column("rebate_account_two")]
        public int RebateAccountTwo { get; set; }
        /// <summary>
        /// 财务账号（年返）
        /// </summary>
        [Column("rebate_account_three")]
        public int RebateAccountThree { get; set; }
        /// <summary>
        /// 对账方式
        /// </summary>
        [Column("reconciliation_mode")]
        public sbyte ReconciliationMode { get; set; }
        /// <summary>
        /// 门店审核状态   1待审核 2已通过审核 3未通过审核
        /// </summary>
        [Column("check_status")]
        public sbyte CheckStatus { get; set; }
        /// <summary>
        /// 审核人
        /// </summary>
        [Column("examiner")]
        public string Examiner { get; set; } = string.Empty;
        /// <summary>
        /// 审核人电话
        /// </summary>
        [Column("examiner_tel")]
        public string ExaminerTel { get; set; } = string.Empty;
        /// <summary>
        /// 未通过审核原因
        /// </summary>
        [Column("failed_examined_reason")]
        public string FailedExaminedReason { get; set; } = string.Empty;
        /// <summary>
        /// 提交人
        /// </summary>
        [Column("submitor")]
        public string Submitor { get; set; } = string.Empty;
        /// <summary>
        /// 提交人电话
        /// </summary>
        [Column("submitor_tel")]
        public string SubmitorTel { get; set; } = string.Empty;
        /// <summary>
        /// 门店老板姓名
        /// </summary>
        [Column("owner_name")]
        public string OwnerName { get; set; } = string.Empty;
        /// <summary>
        /// 门店老板电话
        /// </summary>
        [Column("owner_phone")]
        public string OwnerPhone { get; set; } = string.Empty;
        /// <summary>
        /// 身份证正面
        /// </summary>
        [Column("id_card_front")]
        public string IdCardFront { get; set; } = string.Empty;
        /// <summary>
        /// 身份证背面
        /// </summary>
        [Column("id_card_back")]
        public string IdCardBack { get; set; } = string.Empty;
        /// <summary>
        /// 主管
        /// </summary>
        [Column("charge")]
        public string Charge { get; set; } = string.Empty;
        /// <summary>
        /// 主管电话
        /// </summary>
        [Column("charge_phone")]
        public string ChargePhone { get; set; } = string.Empty;
        /// <summary>
        /// 品类
        /// </summary>
        [Column("category")]
        public string Category { get; set; } = string.Empty;
        /// <summary>
        /// 服务类型
        /// </summary>
        [Column("service_type")]
        public int ServiceType { get; set; }
        /// <summary>
        /// 门店等级
        /// </summary>
        [Column("level")]
        public int Level { get; set; }
        /// <summary>
        /// 门店总评分
        /// </summary>
        [Column("score")]
        public decimal Score { get; set; }
        /// <summary>
        /// 总订单数
        /// </summary>
        [Column("total_order")]
        public int TotalOrder { get; set; }
        /// <summary>
        /// 好评率
        /// </summary>
        [Column("applause_rate")]
        public decimal ApplauseRate { get; set; }
        /// <summary>
        /// 快速排队小程序码
        /// </summary>
        [Column("applet_code")]
        public string AppletCode { get; set; } = string.Empty;
        /// <summary>
        /// 门店小程序码
        /// </summary>
        [Column("shop_applet_code")]
        public string ShopAppletCode { get; set; } = string.Empty;
        /// <summary>
        /// 门店标签
        /// </summary>
        [Column("tag_name")]
        public string TagName { get; set; } = string.Empty;
        /// <summary>
        /// 商户性质 0：门店加盟 1：技师先生 2：配件改装 3：工厂直销
        /// </summary>
        [Column("nature")]
        public sbyte Nature { get; set; }
        /// <summary>
        /// 支付押金额度
        /// </summary>
        [Column("deposit_amount")]
        public decimal DepositAmount { get; set; }
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
