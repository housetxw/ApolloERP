using Ae.Shop.Service.Common.Extension;
using Ae.Shop.Service.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Service.Core.Model
{
    public class ShopSimpleInfoForBOSSDTO
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
        public string ShopCompanyName { get; set; }
        /// <summary>
        /// 门店类型
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// 门店状态 0正常 1终止 2暂停 
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// 品牌
        /// </summary>
        public string Brand { get; set; }
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
        /// 负责人
        /// </summary>
        public string Head { get; set; } = string.Empty;
        /// <summary>
        /// 负责人电话
        /// </summary>
        public string HeadPhone { get; set; } = string.Empty;
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

        /// <summary>
        /// 审核状态
        /// </summary>
        public int CheckStatus { get; set; }

        /// <summary>
        /// 驳回原因
        /// </summary>
        public string FailedExaminedReason { get; set; }

        /// <summary>
        /// 上下架状态
        /// </summary>
        public int Online { get; set; }
        /// <summary>
        /// 门店老板姓名
        /// </summary>
        public string OwnerName { get; set; } = string.Empty;
        /// <summary>
        /// 门店老板电话
        /// </summary>
        public string OwnerPhone { get; set; } = string.Empty;


        public DateTime CreateTime { get; set; } = DateTime.Now;

        public DateTime UpdateTime { get; set; } = DateTime.Now;

        /// <summary>
        /// 系统版本
        /// </summary>
        public int SystemType { get; set; }

        public string ShowSystemType
        {
            get
            {
                try
                {
                    return ((SystemTypeEnum)SystemType).GetEnumDescription();
                }
                catch
                {
                    return string.Empty;
                }
            }
        }
    }
}
