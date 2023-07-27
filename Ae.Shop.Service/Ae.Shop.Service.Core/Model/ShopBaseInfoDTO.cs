using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Service.Core.Model
{
    public class ShopBaseInfoDTO
    {
        /// <summary>
        /// 门店id
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 简单名称
        /// </summary>
        public string SimpleName { get; set; }
        /// <summary>
        /// 店全称
        /// </summary>
        public string FullName { get; set; }
        /// <summary>
        /// 公司名称
        /// </summary>
        public string CompanyName { get; set; }
        /// <summary>
        /// 门店类型
        /// </summary>
        public int Type { get; set; }
        /// <summary>
        /// 门店状态online:上架 offline：下架
        /// </summary>
        public string Status { get; set; }
        /// <summary>
        /// 品牌
        /// </summary>
        public string Brand { get; set; }
        /// <summary>
        /// 短地址（省市区）
        /// </summary>
        public string ShortAddress { get; set; }
        /// <summary>
        /// 详细地址
        /// </summary>
        public string Address { get; set; }
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
        /// 审核人
        /// </summary>
        public string Examiner { get; set; }
        /// <summary>
        /// 提交人
        /// </summary>
        public string Submitor { get; set; }
        /// <summary>
        /// 轮保负责人
        /// </summary>
        public string LunbaoResponsiblePerson { get; set; }
        /// <summary>
        /// 暂停营业 开始时间
        /// </summary>
        public string SuspendStartDateTime { get; set; }
        /// <summary>
        /// 暂停营业 结束时间
        /// </summary>
        public string SuspendEndDateTime { get; set; }
    }
}
