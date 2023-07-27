using Ae.Shop.Service.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.Shop.Service.Core.Request.OpeningGuide
{
    public class AddShopRegisterRequest
    {
        /// <summary>
        /// 门店名
        /// </summary>
        [Required(ErrorMessage = "门店名不能为空")]
        public string SimpleName { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        [Required(ErrorMessage = "姓名不能为空")]
        public string OwnerName { get; set; }
        /// <summary>
        /// 手机号
        /// </summary>
        [Required(ErrorMessage = "手机号不能为空")]
        public string OwnerPhone { get; set; }
        /// <summary>
        /// 分享推广门店ID
        /// </summary>
        public long ShareShopId { get; set; }
        /// <summary>
        /// 商户性质 0：门店加盟 1：平台先生2：配件改装 3：工厂直销
        /// </summary>
        public ShopNatureEnum Nature { get; set; } = ShopNatureEnum.Shop;

        /// <summary>
        /// 门店公司Id
        /// </summary>
        public long CompanyId { get; set; }

        /// <summary>
        /// 门店公司名称
        /// </summary>
        public string ShopCompanyName { get; set; }

        /// <summary>
        /// 省
        /// </summary>
        public string ProvinceId { get; set; }

        /// <summary>
        /// 市
        /// </summary>
        public string CityId { get; set; }

        /// <summary>
        /// 区
        /// </summary>
        public string DistrictId { get; set; }

        public string Province { get; set; }

        public string City { get; set; }

        public string District { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        public string Address { get; set; }
    }
}
