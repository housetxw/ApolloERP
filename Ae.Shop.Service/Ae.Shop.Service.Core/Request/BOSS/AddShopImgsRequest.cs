using Ae.Shop.Service.Core.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.Shop.Service.Core.Request
{
    public class AddShopImgsRequest
    {
        /// <summary>
        /// 门店id
        /// </summary>
        [Range(1, int.MaxValue, ErrorMessage = "门店ID必须大于0")]
        public long ShopId { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public string CreateBy { get; set; }

        /// <summary>
        /// 门头照片
        /// </summary>
        public List<ShopImgDTO> HeadImg { get; set; } = new List<ShopImgDTO>();

        /// <summary>
        /// 正面图片
        /// </summary>
        public List<ShopImgDTO> FrontImg { get; set; } = new List<ShopImgDTO>();

        /// <summary>
        /// 门店照片
        /// </summary>
        public List<ShopImgDTO> ShopImgs { get; set; } = new List<ShopImgDTO>();

        /// <summary>
        /// 门店资质证明照片
        /// </summary>
        public List<ShopImgDTO> ShopProofImgs { get; set; } = new List<ShopImgDTO>();

        /// <summary>
        /// 营业执照
        /// </summary>
        public List<ShopImgDTO> BusinessLicenseImgs { get; set; } = new List<ShopImgDTO>();

        /// <summary>
        /// 经营许可证
        /// </summary>
        public List<ShopImgDTO> ManagementLicenseImgs { get; set; } = new List<ShopImgDTO>();
    }
}
