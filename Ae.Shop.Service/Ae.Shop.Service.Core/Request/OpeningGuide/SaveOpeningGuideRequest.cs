using Ae.Shop.Service.Core.Enums;
using Ae.Shop.Service.Core.Model.OpeningGuide;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.Shop.Service.Core.Request.OpeningGuide
{
    /// <summary>
    /// 开业引导保存请求对象
    /// </summary>
    public class SaveOpeningGuideRequest
    {
        /// <summary>
        /// ShopId
        /// </summary>
        [Range(1, int.MaxValue, ErrorMessage = "门店ID必须大于0")]
        public long ShopId { get; set; }

        /// <summary>
        /// 操作者
        /// </summary>
        public string UpdateBy { get; set; } = string.Empty;
        /// <summary>
        /// 保存步骤 
        /// </summary>
        public OpeningGuideStepEnum SaveStep { get; set; }

        /// <summary>
        /// 第一步
        /// </summary>
        public ShopBaseInfoVO FirstStep { get; set; }

        /// <summary>
        /// 第二步
        /// </summary>
        public ShopAddressInfoVO SecondStep { get; set; }
        /// <summary>
        /// 第三步
        /// </summary>
        public ShopConfigInfoVO ThirdStep { get; set; }
        /// <summary>
        /// 第四步
        /// </summary>
        public ShopBankInfoVO FourthStep { get; set; }
    }
}
