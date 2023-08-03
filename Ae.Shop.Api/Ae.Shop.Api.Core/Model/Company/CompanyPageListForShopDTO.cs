using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Api.Core.Model.Company
{
    public class CompanyPageListForShopDTO
    {
        /// <summary>
        /// 公司ID
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 公司简称
        /// </summary>
        public string SimpleName { get; set; } = string.Empty;
        /// <summary>
        /// 公司全称
        /// </summary>
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// 公司状态 1待提交 2审核中 3正常 4注销 5审核未通过
        /// </summary>
        public sbyte Status { get; set; }
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
        /// 门店总数
        /// </summary>
        public int ShopTotalCount { get; set; }
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
        public string RegisterTime { get; set; } = string.Empty;
        /// <summary>
        /// 公司法人
        /// </summary>
        public string LegalPerson { get; set; } = string.Empty;
    }
}
