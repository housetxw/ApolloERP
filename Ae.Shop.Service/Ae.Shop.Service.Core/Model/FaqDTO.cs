using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Service.Core.Model
{
    public class FaqDTO
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
        /// 渠道
        /// </summary>
        public string ChannelName { get; set; }
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
        /// 一级分类名称
        /// </summary>
        public string CategoryOneName { get; set; }
        /// <summary>
        /// 二级分类名称
        /// </summary>
        public string CategoryTwoName { get; set; }
        /// <summary>
        /// 三级分类名称
        /// </summary>
        public string CategoryThreeName { get; set; }
        /// <summary>
        /// 问题
        /// </summary>
        public string Question { get; set; } = string.Empty;
        /// <summary>
        /// 回答
        /// </summary>
        public string Answer { get; set; } = string.Empty;
        /// <summary>
        /// 是否删除 0否 1是
        /// </summary>
        public sbyte IsDeleted { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public string CreateBy { get; set; } = string.Empty;
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 修改人
        /// </summary>
        public string UpdateBy { get; set; } = string.Empty;
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime UpdateTime { get; set; } = new DateTime(1900, 1, 1);
    }
}
