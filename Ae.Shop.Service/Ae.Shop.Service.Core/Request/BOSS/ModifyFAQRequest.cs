using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.Shop.Service.Core.Request
{
    public class ModifyFAQRequest
    {
        /// <summary>
        /// 主键
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 渠道id
        /// </summary>
        public long ChannelId { get; set; }
        /// <summary>
        /// 一级分类ID
        /// </summary>
        public long CategoryOneId { get; set; }
        /// <summary>
        /// 二级分类ID
        /// </summary>
        public long CategoryTwoId { get; set; }
        /// <summary>
        /// 三级分类ID
        /// </summary>
        public long CategoryThreeId { get; set; }
        /// <summary>
        /// 问题
        /// </summary>
        [Required(ErrorMessage = "问题不能为空")]
        public string Question { get; set; } = string.Empty;
        /// <summary>
        /// 回答
        /// </summary>
        [Required(ErrorMessage = "回答不能为空")]
        public string Answer { get; set; } = string.Empty;
        /// <summary>
        /// 创建人
        /// </summary>
        public string CreateBy { get; set; } = string.Empty;
        /// <summary>
        /// 修改人
        /// </summary>
        public string UpdateBy { get; set; } = string.Empty;
    }
}
