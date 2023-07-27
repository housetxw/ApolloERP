using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.Shop.Service.Core.Model
{
    /// <summary>
    /// 新增门店信息
    /// </summary>
    public class ShopDTO
    {
        /// <summary>
        /// 门店id
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 简单名称
        /// </summary>
        [Required(ErrorMessage = "简单名称不能为空")]
        public string SimpleName { get; set; }
        /// <summary>
        /// 门店全称
        /// </summary>
        public string FullName { get; set; }
        /// <summary>
        /// 公司名称
        /// </summary>
        public string ShopCompanyName { get; set; } = string.Empty;
        /// <summary>
        /// 公司ID
        /// </summary>
        public long CompanyId { get; set; }
        /// <summary>
        /// 营业类型 1 快修店
        /// </summary>
        public int BusinessType { get; set; }
        /// <summary>
        /// 门店类型 -1：仓库 1：合作门店,2：自营门店,4：上门养车
        /// </summary>
        //[Range(1, int.MaxValue, ErrorMessage = "门店类型必须大于0")]
        public int Type { get; set; }

        /// <summary>
        /// 系统版本
        /// </summary>
        public int SystemType { get; set; }
        /// <summary>
        /// 门店状态 0正常 1终止 2暂停 
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// 门店审核状态   1待审核 2已通过审核 3未通过审核
        /// </summary>
        public int CheckStatus { get; set; } 
        /// <summary>
        /// 上下架状态：0下架，1上架
        /// </summary>
        public sbyte Online { get; set; }
        /// <summary>
        /// 品牌
        /// </summary>
        public string Brand { get; set; } = string.Empty;
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; } = string.Empty;
        /// <summary>
        /// 省
        /// </summary>
        public string ProvinceId { get; set; } = string.Empty;
        /// <summary>
        /// 市
        /// </summary>
        public string CityId { get; set; } = string.Empty;
        /// <summary>
        /// 区
        /// </summary>
        public string DistrictId { get; set; } = string.Empty;
        /// <summary>
        /// 省名称
        /// </summary>
        public string Province { get; set; } = string.Empty;
        /// <summary>
        /// 市名称
        /// </summary>
        public string City { get; set; } = string.Empty;
        /// <summary>
        /// 区名称
        /// </summary>
        public string District { get; set; } = string.Empty;
        /// <summary>
        /// 详细地址
        /// </summary>
        public string Address { get; set; } = string.Empty;
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
        public string Post { get; set; } = string.Empty;
        /// <summary>
        /// 联系人
        /// </summary>
        public string Contact { get; set; } = string.Empty;
        /// <summary>
        /// 电话
        /// </summary>
        public string Telephone { get; set; } = string.Empty;
        /// <summary>
        /// 手机
        /// </summary>
        public string Mobile { get; set; } = string.Empty;
        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; } = string.Empty;
        /// <summary>
        /// 对外电话
        /// </summary>
        public string ExternalPhone { get; set; } = string.Empty;
        /// <summary>
        /// 传真
        /// </summary>
        public string Fax { get; set; } = string.Empty;
        /// <summary>
        /// 负责人
        /// </summary>
        public string Head { get; set; } = string.Empty;
        /// <summary>
        /// 负责人电话
        /// </summary>
        public string HeadPhone { get; set; } = string.Empty;
        /// <summary>
        /// 负责人邮箱
        /// </summary>
        public string HeadEmail { get; set; } = string.Empty;
        /// <summary>
        /// 对账联系人
        /// </summary>
        public string AccountingContact { get; set; } = string.Empty;
        /// <summary>
        /// 对账联系人电话
        /// </summary>
        public string AccountingContactPhone { get; set; } = string.Empty;
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
        public string AccountingPerson { get; set; } = string.Empty;
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
        public string AccountingPeriod { get; set; } = string.Empty;
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
        public string Examiner { get; set; } = string.Empty;
        /// <summary>
        /// 审核人电话
        /// </summary>
        public string ExaminerTel { get; set; } = string.Empty;
        /// <summary>
        /// 未通过审核原因
        /// </summary>
        public string FailedExaminedReason { get; set; } = string.Empty;
        /// <summary>
        /// 提交人
        /// </summary>
        public string Submitor { get; set; } = string.Empty;
        /// <summary>
        /// 提交人电话
        /// </summary>
        public string SubmitorTel { get; set; } = string.Empty;
        /// <summary>
        /// 门店老板姓名
        /// </summary>
        public string OwnerName { get; set; } = string.Empty;
        /// <summary>
        /// 门店老板电话
        /// </summary>
        public string OwnerPhone { get; set; } = string.Empty;
        /// <summary>
        /// 主管
        /// </summary>
        public string Charge { get; set; } = string.Empty;
        /// <summary>
        /// 主管电话
        /// </summary>
        public string ChargePhone { get; set; } = string.Empty;
        /// <summary>
        /// 品类
        /// </summary>
        public string Category { get; set; } = string.Empty;
        /// <summary>
        /// 服务类型
        /// </summary>
        public int ServiceType { get; set; }
        /// <summary>
        /// 门店等级
        /// </summary>
        public int Level { get; set; }
        /// <summary>
        /// 总评分
        /// </summary>
        public decimal Score { get; set; }
        /// <summary>
        /// 总订单数
        /// </summary>
        public int TotalOrder { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public string CreateBy { get; set; } = string.Empty;
        /// <summary>
        /// 正面图片
        /// </summary>
        public List<ShopImgDTO> FrontImg { get; set; } = new List<ShopImgDTO>();
        /// <summary>
        /// 门头照片
        /// </summary>
        public List<ShopImgDTO> HeadImg { get; set; } = new List<ShopImgDTO>();
        /// <summary>
        /// 门店照片
        /// </summary>
        public List<ShopImgDTO> ShopImgs { get; set; } = new List<ShopImgDTO>();
        /// <summary>
        /// 门店资质证明照片
        /// </summary>
        public List<ShopImgDTO> ShopProofImgs { get; set; } = new List<ShopImgDTO>();

        /// <summary>
        /// 营业执照
        /// </summary>
        public List<ShopImgDTO> BusinessLienseImgs { get; set; } = new List<ShopImgDTO>();

        /// <summary>
        /// 经营许可证
        /// </summary>
        public List<ShopImgDTO> ManagementLicenseImgs { get; set; } = new List<ShopImgDTO>();

        /// <summary>
        /// 门店标签
        /// </summary>
        public string TagName { get; set; } = string.Empty;

        /// <summary>
        /// 小程序码
        /// </summary>
        public string AppletCode { get; set; } = string.Empty;

        /// <summary>
        /// 门店信息码
        /// </summary>
        public string ShopAppletCode { get; set; } = string.Empty;
        /// <summary>
        /// 标签
        /// </summary>
        public List<string> TagNames { get; set; } = new List<string>();
        /// <summary>
        /// 专修品牌
        /// </summary>
        public List<string> BrandNames { get; set; } = new List<string>();

        /// <summary>
        /// 押金
        /// </summary>
        public decimal DepositAmount { get; set; }

        public string CompanyName { get; set; } = string.Empty;





    }
}
