using Ae.Shop.Service.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Service.Core.Response
{
    public class GetShopSimpleInfoResponse
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
        public int Type { get; set; }
        /// <summary>
        /// 门店类型
        /// </summary>
        public string ShopType { get; set; }
        /// <summary>
        /// 门店审核状态   1待审核 2已通过审核 3未通过审核
        /// </summary>
        public int CheckStatus { get; set; }
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
        /// 门店图片
        /// </summary>
        public List<ImgDTO> Imgs { get; set; }
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
        /// <summary>
        /// 商户性质 0：门店加盟 1：平台先生2：配件改装 3：工厂直销
        /// </summary>
        public sbyte Nature { get; set; }

        /// <summary>
        /// 市
        /// </summary>
        public string CityId { get; set; }
        /// <summary>
        /// 区
        /// </summary>
        public string DistrictId { get; set; }

        public long CompanyId { get; set; }

        public int SystemType { get; set; }

        /// <summary>
        /// 总评论数量
        /// </summary>
        public int TotalComment { get; set; }
    }
}
