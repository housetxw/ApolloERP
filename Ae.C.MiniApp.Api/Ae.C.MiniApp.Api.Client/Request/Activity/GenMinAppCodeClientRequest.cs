using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.C.MiniApp.Api.Client.Request.Activity
{
    public class GenMinAppCodeClientRequest
    {
        /// <summary>
        /// 自定义场景参数对象
        /// 示例1：new { PromoteContentType = 1, ShopId = 7 }
        /// 示例2：new { PromoteContentType = 1, ShopId = 7, EmployeeId = "46661238-70a0-482d-aa16-bbd710880257" }
        /// </summary>
        public object Scene { get; set; }
        /// <summary>
        /// 落地页
        /// 必须是已经发布的小程序存在的页面（否则报错），例如 pages/index/index, 根路径前不要填加 /,不能携带参数（参数请放在scene字段里），如果不填写这个字段，默认跳主页面
        /// </summary>
        public string Page { get; set; } = string.Empty;
        /// <summary>
        /// 是否需要透明底色
        /// 为 true 时，生成透明底色的小程序码
        /// </summary>
        public bool IsHyaline { get; set; } = false;
        /// <summary>
        /// 宽度
        /// </summary>
        public int Width { get; set; } = 430;
    }
}
