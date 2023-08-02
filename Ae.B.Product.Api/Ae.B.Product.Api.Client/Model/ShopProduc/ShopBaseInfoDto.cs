using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Product.Api.Client.Model.ShopProduc
{
   public class ShopBaseInfoDto
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
        /// 门店类型
        /// </summary>
        public string ShopType { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        public string Address { get; set; }
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
        /// 总评分
        /// </summary>
        public decimal Score { get; set; }
        /// <summary>
        /// 总订单数量
        /// </summary>
        public int TotalOrder { get; set; }
        /// <summary>
        /// 总评论数量
        /// </summary>
        public int ReviewCount { get; set; }
        /// <summary>
        /// 门头图片
        /// </summary>
        public string ShopImageUrl { get; set; }
 
        /// <summary>
        /// 营业时间
        /// </summary>
        public string WorkTime { get; set; }
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
        /// 门店老板姓名
        /// </summary>
        public string OwnerName { get; set; } = string.Empty;
        /// <summary>
        /// 门店老板电话
        /// </summary>
        public string OwnerPhone { get; set; } = string.Empty;
    }
}
