using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.ConsumerOrder.Service.Client.Model
{
    /// <summary>
    /// 新增门店信息
    /// </summary>
    public class ShopDTO
    {
        /// <summary>
        /// 简单名称
        /// </summary>
        public string SimpleName { get; set; }
        /// <summary>
        /// 店全称
        /// </summary>
        public string FullName { get; set; }
        /// <summary>
        /// 门店公司名称
        /// </summary>
        public string ShopCompanyName { get; set; }
        /// <summary>
        /// 公司ID
        /// </summary>
        public long CompanyId { get; set; }
        /// <summary>
        /// 营业类型 1 快修店
        /// </summary>
        public int BusinessType { get; set; }
        /// <summary>
        /// 门店类型 1：自营
        /// </summary>
        public int Type { get; set; }
        /// <summary>
        /// 门店状态 0正常 1终止 2暂停 
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// 门店审核状态   1待审核 2已通过审核 3未通过审核
        /// </summary>
        public int CheckStatus { get; set; }
        /// <summary>
        /// 品牌
        /// </summary>
        public string Brand { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 省
        /// </summary>
        public string ProvinceId { get; set; }
        /// <summary>
        /// 市
        /// </summary>
        public string CityId { get; set; }
        /// <summary>
        /// 区
        /// </summary>
        public string DistrictId { get; set; }
        /// <summary>
        /// 省名称
        /// </summary>
        public string Province { get; set; }
        /// <summary>
        /// 市名称
        /// </summary>
        public string City { get; set; }
        /// <summary>
        /// 区名称
        /// </summary>
        public string District { get; set; }
        /// <summary>
        /// 详细地址
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 经度
        /// </summary>
        public double Longitude { get; set; }
        /// <summary>
        /// 维度
        /// </summary>
        public double Latitude { get; set; }
        /// <summary>
        /// 邮编
        /// </summary>
        public string Post { get; set; }
        /// <summary>
        /// 联系人
        /// </summary>
        public string Contact { get; set; }
        /// <summary>
        /// 电话
        /// </summary>
        public string Telephone { get; set; }
        /// <summary>
        /// 手机
        /// </summary>
        public string Mobile { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// 对外电话
        /// </summary>
        public string ExternalPhone { get; set; }
        /// <summary>
        /// 传真
        /// </summary>
        public string Fax { get; set; }
        /// <summary>
        /// 对账联系人
        /// </summary>
        public string AccountingContact { get; set; }
        /// <summary>
        /// 对账联系人电话
        /// </summary>
        public string AccountingContactPhone { get; set; }
        /// <summary>
        /// 应付账户
        /// </summary>
        public int PayableAccount { get; set; }
        /// <summary>
        /// 回款账户
        /// </summary>
        public int RecievableAccount { get; set; }
        /// <summary>
        /// 对账人
        /// </summary>
        public string AccountingPerson { get; set; }
        /// <summary>
        /// 财务账号1
        /// </summary>
        public int RebateAccountOne { get; set; }
        /// <summary>
        /// 财务账号2
        /// </summary>
        public int RebateAccountTwo { get; set; }
        /// <summary>
        /// 财务账号3
        /// </summary>
        public int RebateAccountThree { get; set; }
        /// <summary>
        /// 对账周期
        /// </summary>
        public string AccountingPeriod { get; set; }
        /// <summary>
        /// 对账周期更新时间
        /// </summary>
        public DateTime UpdateAccountingPeriodTime { get; set; }
        /// <summary>
        /// 对账方式
        /// </summary>
        public int ReconciliationMode { get; set; }
        /// <summary>
        /// 审核人
        /// </summary>
        public string Examiner { get; set; }
        /// <summary>
        /// 审核人电话
        /// </summary>
        public string ExaminerTel { get; set; }
        /// <summary>
        /// 未通过审核原因
        /// </summary>
        public string FailedExaminedReason { get; set; }
        /// <summary>
        /// 提交人
        /// </summary>
        public string Submitor { get; set; }
        /// <summary>
        /// 提交人电话
        /// </summary>
        public string SubmitorTel { get; set; }
        /// <summary>
        /// 门店老板姓名
        /// </summary>
        public string OwnerName { get; set; }
        /// <summary>
        /// 门店老板电话
        /// </summary>
        public string OwnerPhone { get; set; }
        /// <summary>
        /// 门店营业类型
        /// </summary>
        public string ShopActiveType { get; set; }
        /// <summary>
        /// 品类
        /// </summary>
        public string Category { get; set; }
        /// <summary>
        /// 服务类型
        /// </summary>
        public int ServiceType { get; set; }
        /// <summary>
        /// 门店等级
        /// </summary>
        public int Level { get; set; }
        /// <summary>
        /// 评分
        /// </summary>
        public decimal Score { get; set; }
        /// <summary>
        /// 总订单数
        /// </summary>
        public int TotalOrder { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public string CreateBy { get; set; }

        public string Head { get; set; }
    }
}
