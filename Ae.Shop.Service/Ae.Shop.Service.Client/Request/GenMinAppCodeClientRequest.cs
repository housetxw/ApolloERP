using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Service.Client.Request
{
   public class GenMinAppCodeClientRequest
    {
        /// <summary>
        /// 自定义场景参数
        /// 最大32个可见字符，只支持数字，大小写英文以及部分特殊字符：!#$&'()*+,/:;=?@-._~，其它字符请自行编码为合法字符（因不支持%，中文无法使用 urlencode 处理，请使用其他编码方式）
        /// </summary>
        public object Scene { get; set; } = string.Empty;
        /// <summary>
        /// 落地页
        /// 必须是已经发布的小程序存在的页面（否则报错），例如 pages/index/index, 根路径前不要填加 /,不能携带参数（参数请放在scene字段里），如果不填写这个字段，默认跳主页面
        /// </summary>
        public string Page { get; set; } = string.Empty;
        /// <summary>
        /// 是否需要透明底色
        /// 为 true 时，生成透明底色的小程序码
        /// </summary>
        public bool IsHyaline { get; set; } = true;
        /// <summary>
        /// 宽度
        /// </summary>
        public int Width { get; set; } = 430;

    }
}
