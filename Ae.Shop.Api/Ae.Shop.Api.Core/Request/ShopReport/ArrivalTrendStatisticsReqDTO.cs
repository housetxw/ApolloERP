using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Ae.Shop.Api.Core.Enums;

namespace Ae.Shop.Api.Core.Request
{
    public class ArrivalTrendStatisticsReqDTO : BasePageRequest
    {
        [Required(ErrorMessage = "请输入ShopId")]
        [Range(1, long.MaxValue, ErrorMessage = "ShopId必须大于0")]
        public long ShopId { get; set; }

        [Required(ErrorMessage = "请输入开始时间")]
        public DateTime StartTime { get; set; }

        [Required(ErrorMessage = "请输入结束时间")]
        public DateTime EndTime { get; set; }
    }

    public class ArrivalTrendStatisticsReqDO
    {
        public long ShopId { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public List<int> Status { get; set; } = new List<int>();
    }

}
