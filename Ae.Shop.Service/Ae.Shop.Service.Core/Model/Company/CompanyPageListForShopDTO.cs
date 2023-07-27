using Ae.Shop.Service.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Service.Core.Model.Company
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
        /// 公司状态 0待提交 1审核中 2正常 3注销
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
        public DateTime RegisterTime { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 公司法人
        /// </summary>
        public string LegalPerson { get; set; } = string.Empty;

        public string CreateBy { get; set; }

        public DateTime CreateTime { get; set; } = DateTime.Now;

        public string UpdateBy { get; set; }

        public DateTime UpdateTime { get; set; } = DateTime.Now;

        public string Address { get; set; }

        public long ParentId { get; set; }
    }
}
