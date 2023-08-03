using Ae.C.Login.Api.Core.Enums;
using Ae.C.Login.Api.Core.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.C.Login.Api.Core.Request
{
    /// <summary>
    /// 三方登录
    /// </summary>
    public class ThirtyLoginRequest : ReferrerInfo
    {
        /// <summary>
        /// 三方登录类型
        /// </summary>
        [Required(ErrorMessage = "三方登录类型不能为空")]
        public ThirtyLoginType LoginType { get; set; }
        /// <summary>
        /// 平台openid
        /// </summary>
        public string OpenId { get; set; }

        /// <summary>
        /// 三方用户唯一标识
        /// </summary>
        public string UnionId { get; set; }

        /// <summary>
        /// 用户昵称
        /// </summary>
        public string NickName { get; set; }
        /// <summary>
        /// 头像
        /// </summary>
        public string HeadUrl { get; set; }
        /// <summary>
        /// 加密数据
        /// </summary>
        [Required(ErrorMessage = "手机号不能为空")]
        public string EncryptedData { get; set; }
        /// <summary>
        /// 加密算法的初始向量
        /// </summary>
        [Required(ErrorMessage = "加密算法的初始向量不能为空")]
        public string Iv { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public int Gender { get; set; }

        ///// <summary>
        ///// 加密数据Unionid
        ///// </summary>
        //[Required(ErrorMessage = "手机号不能为空")]
        //public string UnionIdEncryptedData { get; set; }
        ///// <summary>
        ///// 加密算法的初始向量Unionid
        ///// </summary>
        //[Required(ErrorMessage = "加密算法的初始向量不能为空")]
        //public string UnionIdIv { get; set; }

    }
}
