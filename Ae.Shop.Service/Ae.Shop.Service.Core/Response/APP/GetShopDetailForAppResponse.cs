using Ae.Shop.Service.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Service.Core.Response
{
    public class GetShopDetailForAppResponse
    {
        /// <summary>
        /// 联系人
        /// </summary>
        public string Contact { get; set; }
        /// <summary>
        /// 联系人电话
        /// </summary>
        public string Telephone { get; set; }
        /// <summary>
        /// 营业时间
        /// </summary>
        public string WorkTime { get; set; }
        /// <summary>
        /// 门店地址
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 门头照片
        /// </summary>
        public string HeadImg { get; set; }
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
        /// 描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 快速排队小程序码
        /// </summary>
        public string AppletCode { get; set; } = string.Empty;

        /// <summary>
        /// 门店小程序码
        /// </summary>        
        public string ShopAppletCode { get; set; } = string.Empty;
        /// <summary>
        /// 门店照片
        /// </summary>
        public List<ImgDTO> ShopImgs { get; set; }
        /// <summary>
        /// 门店资质证明照片
        /// </summary>
        public List<ImgDTO> ShopProofImgs { get; set; }
        /// <summary>
        /// 设施信息
        /// </summary>
        public List<ShopSetDTO> ShopFacility { get; set; }

        public List<string> TagNames { get; set; } = new List<string>();

        public List<ShopServiceBrandDTO> BrandNames { get; set; } = new List<ShopServiceBrandDTO>();
        /// <summary>
        /// 门店老板姓名
        /// </summary>
        public string OwnerName { get; set; } = string.Empty;
        /// <summary>
        /// 门店老板电话
        /// </summary>
        public string OwnerPhone { get; set; } = string.Empty;

        /// <summary>
        /// 开户银行
        /// </summary>
        
        public string OpeningBank { get; set; } = string.Empty;

        /// <summary>
        /// 开户支行
        /// </summary>
      
        public string OpeningBranch { get; set; } = string.Empty;
       
        /// <summary>
        /// 开户人名称
        /// </summary>
       
        public string OpeningUserName { get; set; } = string.Empty;
        /// <summary>
        /// 银行卡号
        /// </summary>
      
        public string BankCardNo { get; set; } = string.Empty;
    }
}
