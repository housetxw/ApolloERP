using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Service.Dal.Model
{
    public class NoticeInfoDO
    {
        /// <summary>
        /// 公告主键id
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; } = string.Empty;


        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; } = string.Empty;
        /// <summary>
        /// 发布部门
        /// </summary>
        public string Department { get; set; } = string.Empty;
        /// <summary>
        /// 创建人
        /// </summary>
        public string CreateBy { get; set; } = string.Empty;
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; } = new DateTime(1900, 1, 1);
    }
}
