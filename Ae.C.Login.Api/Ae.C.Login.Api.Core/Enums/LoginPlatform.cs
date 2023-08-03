using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.C.Login.Api.Core.Enums
{
    /// <summary>
    /// 登录平台
    /// </summary>
    public enum LoginPlatform
    {
        /// <summary>
        /// 养车
        /// </summary>
        [Display(Name = "养车")]
        YangChe = 1,
        /// <summary>
        /// 车邦
        /// </summary>
        [Display(Name = "车邦")]
        CheBang = 2,
        /// <summary>
        /// 汽配
        /// </summary>
        [Display(Name = "汽配")]
        QiPei = 3,
    }
}
