using Ae.Shop.Service.Core.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.Shop.Service.Core.Request
{
    public class ModifyShopBaseInfoRequest
    {
        /// <summary>
        /// 门店ID
        /// </summary>
        /// </summary>
        [Range(1, int.MaxValue, ErrorMessage = "门店ID必须大于0")]
        public long ShopId { get; set; }
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
        /// 公司名称
        /// </summary>
        public long CompanyId { get; set; }
        /// <summary>
        /// 营业类型 1 快修店
        /// </summary>
        public int BusinessType { get; set; }
        /// <summary>
        /// 门店类型 
        /// </summary>
        [Range(1, int.MaxValue, ErrorMessage = "门店类型必须大于0")]
        public int Type { get; set; }
        /// <summary>
        /// 门店状态 0营业中 1终止营业 2暂停营业 
        /// </summary>
        public int Status { get; set; }
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
        /// 门店老板姓名
        /// </summary>
        public string OwnerName { get; set; }
        /// <summary>
        /// 门店老板电话
        /// </summary>
        public string OwnerPhone { get; set; }
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
        /// 修改人
        /// </summary>
        public string UpdateBy { get; set; }

        /// <summary>
        /// 标签名
        /// </summary>
        public string TagName { get; set; } = string.Empty;
        /// <summary>
        /// 标签
        /// </summary>
        public List<string> TagNames { get; set; } = new List<string>();
        /// <summary>
        /// 专修品牌
        /// </summary>
        public List<string> BrandNames { get; set; } = new List<string>();


        /// <summary>
        /// 系统类型
        /// </summary>
        public int SystemType { get; set; }

        public int IsCreateAccount { get; set; }

        public  int IsSendMessage { get; set; }

     

       

    }
}
