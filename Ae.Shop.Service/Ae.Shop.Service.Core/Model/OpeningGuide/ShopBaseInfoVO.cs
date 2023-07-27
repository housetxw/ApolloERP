using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Service.Core.Model.OpeningGuide
{
    public class ShopBaseInfoVO
    {
        /// <summary>
        /// 操作者
        /// </summary>
        public string UpdateBy { get; set; } = string.Empty;

        /// <summary>
        /// 店公司名称
        /// </summary>
        public string ShopCompanyName { get; set; } = string.Empty;
        /// <summary>
        /// 门店简单名称
        /// </summary>
        public string SimpleName { get; set; } = string.Empty;
        /// <summary>
        /// 门店老板姓名
        /// </summary>
        public string OwnerName { get; set; } = string.Empty;
        /// <summary>
        /// 门店老板电话
        /// </summary>
        public string OwnerPhone { get; set; } = string.Empty;

        /// <summary>
        /// 运营负责人
        /// </summary>
        public string Head { get; set; } = string.Empty;

        /// <summary>
        /// 负责人电话
        /// </summary>
        public string HeadPhone { get; set; } = string.Empty;

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// 门头照片
        /// </summary>
        public List<ImgVO> DoorHeaderImgs { get; set; }

        /// <summary>
        /// 正面照片
        /// </summary>
        public List<ImgVO> FrontImgs { get; set; }

        /// <summary>
        /// 环境照片(门店照片）
        /// </summary>
        public List<ImgVO> EnvironmentImgs { get; set; }

        /// <summary>
        /// 营业执照照片
        /// </summary>
        public List<ImgVO> BusinessLicenseImgs { get; set; }

        /// <summary>
        /// 经营许可证
        /// </summary>
        public List<ImgVO> ManagementLicenseImgs { get; set; }

        /// <summary>
        /// 其他资质证明
        /// </summary>
        public List<ImgVO> OtherLicenseImgs { get; set; }

        /// <summary>
        /// 门店标签1
        /// </summary>
        public string TagNameOne { get; set; } = string.Empty;
        /// <summary>
        /// 门店标签2
        /// </summary>
        public string TagNameTwo { get; set; } = string.Empty;
        /// <summary>
        /// 门店标签3
        /// </summary>
        public string TagNameThree { get; set; } = string.Empty;
        /// <summary>
        /// 门店专修品牌
        /// </summary>
        public List<GetVehicleBrandVo> Brands { get; set; } = new List<GetVehicleBrandVo>();
    }
}
