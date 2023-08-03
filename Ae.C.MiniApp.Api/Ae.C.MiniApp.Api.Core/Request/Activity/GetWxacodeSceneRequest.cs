using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.C.MiniApp.Api.Core.Request.Activity
{
    public class GetWxacodeSceneRequest : BaseGetRequest
    {
        /// <summary>
        /// 场景ID
        /// </summary>
        [Required(ErrorMessage = "场景ID不可为空")]
        public string SceneId { get; set; }
    }
}
