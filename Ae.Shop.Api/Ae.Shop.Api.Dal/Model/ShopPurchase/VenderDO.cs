using ApolloErp.Data.DapperExtensions.Att;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Api.Dal.Model.ShopPurchase
{
    [Table("vender")]
    public class VenderDO
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        [Column("id")]
        public long Id { get; set; }
        /// <summary>
        /// 供应商简称
        /// </summary>
        [Column("vender_short_name")]
        public string VenderShortName { get; set; } = string.Empty;
        /// <summary>
        /// 供应商全称
        /// </summary>
        [Column("vender_name")]
        public string VenderName { get; set; } = string.Empty;
        /// <summary>
        /// 区分类型(1普通 2高级)
        /// </summary>
        [Column("class_type")]
        public string ClassType { get; set; } = string.Empty;
        /// <summary>
        /// 供应类型(1轮胎 2保养 3车品 4轮毂 5美容)
        /// </summary>
        [Column("supply_type")]
        public string SupplyType { get; set; } = string.Empty;
        /// <summary>
        /// 办公地址
        /// </summary>
        [Column("office_address")]
        public string OfficeAddress { get; set; } = string.Empty;
        /// <summary>
        /// 合作品牌
        /// </summary>
        [Column("cooperative_brand")]
        public string CooperativeBrand { get; set; } = string.Empty;
        /// <summary>
        /// 资质编号
        /// </summary>
        [Column("qualification")]
        public string Qualification { get; set; } = string.Empty;
        /// <summary>
        /// 营业执照编号
        /// </summary>
        [Column("business_license")]
        public string BusinessLicense { get; set; } = string.Empty;
        /// <summary>
        /// 组织机构代码
        /// </summary>
        [Column("organization_code")]
        public string OrganizationCode { get; set; } = string.Empty;
        /// <summary>
        /// 企业名称
        /// </summary>
        [Column("enterprise_name")]
        public string EnterpriseName { get; set; } = string.Empty;
        /// <summary>
        /// 企业电话
        /// </summary>
        [Column("enterprise_tel")]
        public string EnterpriseTel { get; set; } = string.Empty;
        /// <summary>
        /// 银行账号
        /// </summary>
        [Column("account")]
        public string Account { get; set; } = string.Empty;
        /// <summary>
        /// 银行名称
        /// </summary>
        [Column("bank")]
        public string Bank { get; set; } = string.Empty;
        /// <summary>
        /// 注册地址
        /// </summary>
        [Column("register_address")]
        public string RegisterAddress { get; set; } = string.Empty;
        /// <summary>
        /// 收款人
        /// </summary>
        [Column("payee")]
        public string Payee { get; set; } = string.Empty;
        /// <summary>
        /// 税号
        /// </summary>
        [Column("tex")]
        public string Tex { get; set; } = string.Empty;
        /// <summary>
        /// 联系电话
        /// </summary>
        [Column("tel_num")]
        public string TelNum { get; set; } = string.Empty;
        /// <summary>
        /// 财务收款人
        /// </summary>
        [Column("finance_name")]
        public string FinanceName { get; set; } = string.Empty;
        /// <summary>
        /// 财务联系方式
        /// </summary>
        [Column("finance_tel")]
        public string FinanceTel { get; set; } = string.Empty;
        /// <summary>
        /// 合作类型(1三方 2加盟)
        /// </summary>
        [Column("cooperation_type")]
        public string CooperationType { get; set; } = string.Empty;
        /// <summary>
        /// 返利类型(1月返 2季返 3年返)
        /// </summary>
        [Column("rebate_type")]
        public string RebateType { get; set; } = string.Empty;
        /// <summary>
        /// 主体人
        /// </summary>
        [Column("biz_name")]
        public string BizName { get; set; } = string.Empty;
        /// <summary>
        /// 主体人联系方式
        /// </summary>
        [Column("biz_tel")]
        public string BizTel { get; set; } = string.Empty;
        /// <summary>
        /// 付款有效期（账期天数）
        /// </summary>
        [Column("payment_day")]
        public int PaymentDay { get; set; }
        /// <summary>
        /// 售后联系人
        /// </summary>
        [Column("over_sale_name")]
        public string OverSaleName { get; set; } = string.Empty;
        /// <summary>
        /// 售后联系电话
        /// </summary>
        [Column("over_sale_tel")]
        public string OverSaleTel { get; set; } = string.Empty;
        /// <summary>
        /// 售后服务地址
        /// </summary>
        [Column("over_sale_address")]
        public string OverSaleAddress { get; set; } = string.Empty;
        /// <summary>
        /// 是否激活
        /// </summary>
        [Column("is_active")]
        public sbyte IsActive { get; set; }
        /// <summary>
        /// 主体人
        /// </summary>
        [Column("bulk_biz_name")]
        public string BulkBizName { get; set; } = string.Empty;
        /// <summary>
        /// 主体电话
        /// </summary>
        [Column("bulk_biz_tel")]
        public string BulkBizTel { get; set; } = string.Empty;
        /// <summary>
        /// 邮件地址
        /// </summary>
        [Column("email_address")]
        public string EmailAddress { get; set; } = string.Empty;
        /// <summary>
        /// 传真
        /// </summary>
        [Column("fax")]
        public string Fax { get; set; } = string.Empty;
        /// <summary>
        /// 税点
        /// </summary>
        [Column("tax_point")]
        public int TaxPoint { get; set; }
        /// <summary>
        /// 发货方式(1供应商送货 2自提)
        /// </summary>
        [Column("shipment_type")]
        public string ShipmentType { get; set; } = string.Empty;
        /// <summary>
        /// 省编号
        /// </summary>
        [Column("province_id")]
        public string ProvinceId { get; set; } = string.Empty;
        /// <summary>
        /// 市编号
        /// </summary>
        [Column("city_id")]
        public string CityId { get; set; } = string.Empty;
        /// <summary>
        /// 区编号
        /// </summary>
        [Column("district_id")]
        public string DistrictId { get; set; } = string.Empty;
        /// <summary>
        /// 省名称
        /// </summary>
        [Column("province_name")]
        public string ProvinceName { get; set; } = string.Empty;
        /// <summary>
        /// 市名称
        /// </summary>
        [Column("city_name")]
        public string CityName { get; set; } = string.Empty;
        /// <summary>
        /// 区名称
        /// </summary>
        [Column("district_name")]
        public string DistrictName { get; set; } = string.Empty;
        /// <summary>
        /// 其他类型
        /// </summary>
        [Column("other_supply_type")]
        public string OtherSupplyType { get; set; } = string.Empty;
        /// <summary>
        /// 其他供应商品类
        /// </summary>
        [Column("other_supply_category")]
        public string OtherSupplyCategory { get; set; } = string.Empty;
        /// <summary>
        /// 创建人
        /// </summary>
        [Column("create_by")]
        [NotUpdate]
        public string CreateBy { get; set; } = string.Empty;
        /// <summary>
        /// 创建时间
        /// </summary>
        [Column("create_time")]
        [NotUpdate]
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
        /// <summary>
        /// 是否删除 1 是 0 否 默认 0
        /// </summary>
        [Column("is_deleted")]
        [NotGenWhereCondition]
        public int IsDeleted { get; set; }
        /// <summary>
        /// 门店ID
        /// </summary>
        [Column("shop_id")]
        public long ShopId { get; set; }
        /// <summary>
        /// 官网地址
        /// </summary>
        [Column("offcial_website")]
        public string OffcialWebsite { get; set; } = string.Empty;
        /// <summary>
        /// 联系人
        /// </summary>
        [Column("contact")]
        public string Contact { get; set; } = string.Empty;
        /// <summary>
        /// 开票类型（0未设置 1无需发票 2普通发票 3增值税发票）
        /// </summary>
        [Column("invoice_type")]
        public sbyte InvoiceType { get; set; }
        /// <summary>
        /// 结算方式（0未设置 1现结  2账期(废弃) 3月结）
        /// </summary>
        [Column("settlement_method")]
        public sbyte SettlementMethod { get; set; }
        /// <summary>
        /// 分类标注（例如：供应机油）
        /// </summary>
        [Column("classify_mark")]
        public string ClassifyMark { get; set; } = string.Empty;
        /// <summary>
        /// 开户银行
        /// </summary>
        [Column("opening_bank")]
        public string OpeningBank { get; set; } = string.Empty;

        /// <summary>
        /// 收款户名
        /// </summary>
        [Column("receive_bank_name")]
        public string ReceiveBankName { get; set; } = string.Empty;
    }
}
