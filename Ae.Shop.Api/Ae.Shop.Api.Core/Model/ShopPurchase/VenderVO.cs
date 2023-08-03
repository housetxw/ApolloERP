using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Api.Core.Model.ShopPurchase
{
    public class VenderVO
    {
        /// <summary>
        /// 主键
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 供应商简称
        /// </summary>
        public string VenderShortName { get; set; } = string.Empty;
        /// <summary>
        /// 供应商全称
        /// </summary>
        public string VenderName { get; set; } = string.Empty;
        /// <summary>
        /// 区分类型(1普通 2高级)
        /// </summary>
        public string ClassType { get; set; } = string.Empty;
        /// <summary>
        /// 供应类型(1轮胎 2保养 3车品 4轮毂 5美容)
        /// </summary>
        public string SupplyType { get; set; } = string.Empty;
        /// <summary>
        /// 办公地址
        /// </summary>
        public string OfficeAddress { get; set; } = string.Empty;
        /// <summary>
        /// 合作品牌
        /// </summary>
        public string CooperativeBrand { get; set; } = string.Empty;
        /// <summary>
        /// 资质编号
        /// </summary>
        public string Qualification { get; set; } = string.Empty;
        /// <summary>
        /// 营业执照编号
        /// </summary>
        public string BusinessLicense { get; set; } = string.Empty;
        /// <summary>
        /// 组织机构代码
        /// </summary>
        public string OrganizationCode { get; set; } = string.Empty;
        /// <summary>
        /// 企业名称
        /// </summary>
        public string EnterpriseName { get; set; } = string.Empty;
        /// <summary>
        /// 企业电话
        /// </summary>
        public string EnterpriseTel { get; set; } = string.Empty;
        /// <summary>
        /// 银行账号
        /// </summary>
        public string Account { get; set; } = string.Empty;
        /// <summary>
        /// 银行名称
        /// </summary>
        public string Bank { get; set; } = string.Empty;
        /// <summary>
        /// 注册地址
        /// </summary>
        public string RegisterAddress { get; set; } = string.Empty;
        /// <summary>
        /// 收款人
        /// </summary>
        public string Payee { get; set; } = string.Empty;
        /// <summary>
        /// 税号
        /// </summary>
        public string Tex { get; set; } = string.Empty;
        /// <summary>
        /// 联系电话
        /// </summary>
        public string TelNum { get; set; } = string.Empty;
        /// <summary>
        /// 财务收款人
        /// </summary>
        public string FinanceName { get; set; } = string.Empty;
        /// <summary>
        /// 财务联系方式
        /// </summary>
        public string FinanceTel { get; set; } = string.Empty;
        /// <summary>
        /// 合作类型(1三方 2加盟)
        /// </summary>
        public string CooperationType { get; set; } = string.Empty;
        /// <summary>
        /// 返利类型(1月返 2季返 3年返)
        /// </summary>
        public string RebateType { get; set; } = string.Empty;
        /// <summary>
        /// 主体人
        /// </summary>
        public string BizName { get; set; } = string.Empty;
        /// <summary>
        /// 主体人联系方式
        /// </summary>
        public string BizTel { get; set; } = string.Empty;
        /// <summary>
        /// 付款有效期（账期天数）
        /// </summary>
        public int PaymentDay { get; set; }
        /// <summary>
        /// 售后联系人
        /// </summary>
        public string OverSaleName { get; set; } = string.Empty;
        /// <summary>
        /// 售后联系电话
        /// </summary>
        public string OverSaleTel { get; set; } = string.Empty;
        /// <summary>
        /// 售后服务地址
        /// </summary>
        public string OverSaleAddress { get; set; } = string.Empty;
        /// <summary>
        /// 是否激活
        /// </summary>
        public sbyte IsActive { get; set; }
        /// <summary>
        /// 主体人
        /// </summary>
        public string BulkBizName { get; set; } = string.Empty;
        /// <summary>
        /// 主体电话
        /// </summary>
        public string BulkBizTel { get; set; } = string.Empty;
        /// <summary>
        /// 邮件地址
        /// </summary>
        public string EmailAddress { get; set; } = string.Empty;
        /// <summary>
        /// 传真
        /// </summary>
        public string Fax { get; set; } = string.Empty;
        /// <summary>
        /// 税点
        /// </summary>
        public int TaxPoint { get; set; }
        /// <summary>
        /// 发货方式(1供应商送货 2自提)
        /// </summary>
        public string ShipmentType { get; set; } = string.Empty;
        /// <summary>
        /// 省编号
        /// </summary>
        public string ProvinceId { get; set; } = string.Empty;
        /// <summary>
        /// 市编号
        /// </summary>
        public string CityId { get; set; } = string.Empty;
        /// <summary>
        /// 区编号
        /// </summary>
        public string DistrictId { get; set; } = string.Empty;
        /// <summary>
        /// 省名称
        /// </summary>
        public string ProvinceName { get; set; } = string.Empty;
        /// <summary>
        /// 市名称
        /// </summary>
        public string CityName { get; set; } = string.Empty;
        /// <summary>
        /// 区名称
        /// </summary>
        public string DistrictName { get; set; } = string.Empty;
        /// <summary>
        /// 其他类型
        /// </summary>
        public string OtherSupplyType { get; set; } = string.Empty;
        /// <summary>
        /// 其他供应商品类
        /// </summary>
        public string OtherSupplyCategory { get; set; } = string.Empty;
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
        /// <summary>
        /// 是否删除 1 是 0 否 默认 0
        /// </summary>
        public int IsDeleted { get; set; }
        /// <summary>
        /// 门店ID
        /// </summary>
        public long ShopId { get; set; }
        /// <summary>
        /// 官网地址
        /// </summary>
        public string OffcialWebsite { get; set; } = string.Empty;
        /// <summary>
        /// 联系人
        /// </summary>
        public string Contact { get; set; } = string.Empty;
        /// <summary>
        /// 开票类型（0未设置 1无需发票 2普通发票 3增值税发票）
        /// </summary>
        public sbyte InvoiceType { get; set; }
        /// <summary>
        /// 结算方式（0未设置 1现结  2账期(废弃) 3月结）
        /// </summary>
        public sbyte SettlementMethod { get; set; }
        /// <summary>
        /// 分类标注（例如：供应机油）
        /// </summary>
        public string ClassifyMark { get; set; } = string.Empty;
        /// <summary>
        /// 开户银行
        /// </summary>
        public string OpeningBank { get; set; } = string.Empty;

        public string ShopName { get; set; }

        public string ReceiveBankName { get; set; }
    }
}
