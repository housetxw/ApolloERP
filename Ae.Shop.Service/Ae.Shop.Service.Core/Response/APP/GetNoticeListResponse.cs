using Ae.Shop.Service.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Service.Core.Response
{
    public class GetNoticeListResponse
    {
        /// <summary>
        /// 列表
        /// </summary>
        public List<NoticeDTO> Items { get; set; }
        /// <summary>
        /// 总数量
        /// </summary>
        public int TotalItems { get; set; }
        /// <summary>
        /// 已读数量
        /// </summary>
        public int ReadTotal { get; set; }
        /// <summary>
        /// 未读数量
        /// </summary>
        public int UnReadTotal { get; set; }
    }
}
