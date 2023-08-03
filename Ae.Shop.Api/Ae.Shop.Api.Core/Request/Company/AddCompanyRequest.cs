using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.Shop.Api.Core.Request.Company
{
    /// <summary>
    /// 添加公司
    /// </summary>
    public class AddCompanyRequest
    {
        /// <summary>
        /// 公司id
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 公司简称
        /// </summary>
        [Required(ErrorMessage = "公司简称不能为空")]
        public string SimpleName { get; set; } = string.Empty;
        /// <summary>
        /// 公司全称
        /// </summary>
        [Required(ErrorMessage = "公司全称不能为空")]
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// 父级公司id
        /// </summary>
        public long ParentId { get; set; }
        /// <summary>
        /// 公司编号
        /// </summary>
        public string Code { get; set; } = string.Empty;
        /// <summary>
        /// 公司状态 0待提交 1审核中 2正常 3注销
        /// </summary>
        public sbyte Status { get; set; }
        /// <summary>
        /// 类型；0：公司；1.门店；2：仓库；
        /// </summary>
        public sbyte Type { get; set; }
        /// <summary>
        /// 公司等级 0一级公司 1二级公司
        /// </summary>
        public sbyte Level { get; set; }
        /// <summary>
        /// 公司所在省Id
        /// </summary>
        public string ProvinceId { get; set; } = string.Empty;
        /// <summary>
        /// 公司所在市Id
        /// </summary>
        public string CityId { get; set; } = string.Empty;
        /// <summary>
        /// 公司所在区Id
        /// </summary>
        public string DistrictId { get; set; } = string.Empty;
        /// <summary>
        /// 省名
        /// </summary>
        public string Province { get; set; } = string.Empty;
        /// <summary>
        /// 市名
        /// </summary>
        public string City { get; set; } = string.Empty;
        /// <summary>
        /// 区名
        /// </summary>
        public string District { get; set; } = string.Empty;
        /// <summary>
        /// 地址
        /// </summary>
        public string Address { get; set; } = string.Empty;
        /// <summary>
        /// 联系电话
        /// </summary>
        public string Mobile { get; set; } = string.Empty;
        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; } = string.Empty;
        /// <summary>
        /// 公司负责人
        /// </summary>
        public string Head { get; set; } = string.Empty;
        /// <summary>
        /// 负责人电话
        /// </summary>
        public string HeadPhone { get; set; } = string.Empty;
        /// <summary>
        /// 注册资金
        /// </summary>
        public decimal RegisterMoney { get; set; }
        /// <summary>
        /// 工商注册日期
        /// </summary>
        public DateTime RegisterTime { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 公司法人
        /// </summary>
        public string LegalPerson { get; set; } = string.Empty;
        /// <summary>
        /// 法人联系电话
        /// </summary>
        public string LegalPersonPhone { get; set; } = string.Empty;
        /// <summary>
        /// 营业执照
        /// </summary>
        public string BusinessLicense { get; set; } = string.Empty;
        /// <summary>
        /// 开户银行
        /// </summary>
        public string OpeningBank { get; set; } = string.Empty;
        /// <summary>
        /// 银行账号
        /// </summary>
        public string BankAccount { get; set; } = string.Empty;
        /// <summary>
        /// 开户许可证
        /// </summary>
        public string AccountOpeningLicense { get; set; } = string.Empty;
        /// <summary>
        /// 公司介绍
        /// </summary>
        public string Introduction { get; set; } = string.Empty;
        /// <summary>
        /// 创建人
        /// </summary>
        public string CreateBy { get; set; } = string.Empty;

        public int SystemType { get; set; }
    }
}
