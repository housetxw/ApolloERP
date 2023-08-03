using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.Login.Api.Core.Model
{
    public class ReferrerInfo
    {

        /// <summary>
        /// 微信二维码ID
        /// </summary>
        public string SceneId { get; set; }

        /// <summary>
        /// 推荐人Id
        /// </summary>
        public string ShareUserId { get; set; }
    }
}
