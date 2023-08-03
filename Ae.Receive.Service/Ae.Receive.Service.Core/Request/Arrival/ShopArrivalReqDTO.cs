using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.Receive.Service.Core.Request.Arrival
{
    public class TodayArrivalShopStatisticsReqDTO
    {
        [Required(ErrorMessage = "请输入ShopId")]
        [Range(1, long.MaxValue, ErrorMessage = "ShopId必须大于0")]
        public long ShopId { get; set; }
    }

    public class MonthArrivalShopStatisticsReqDTO
    {
        [Required(ErrorMessage = "请输入ShopId")]
        [Range(1, long.MaxValue, ErrorMessage = "ShopId必须大于0")]
        public long ShopId { get; set; }

        public DateTime StartTime { get; set; }

        [Required(ErrorMessage = "请输入结束时间")]
        public DateTime EndTime { get; set; }
    }

    public class NewCustomerStatisticsReqDTO
    {
        public string Key { get; set; } = string.Empty;

        public List<string> UserIds { get; set; } = new List<string>();
    }

    public class NewCustomerArrivalShopReqDTO
    {
        [Required(ErrorMessage = "请输入ShopId")]
        [Range(1, long.MaxValue, ErrorMessage = "ShopId必须大于0")]
        public long ShopId { get; set; }

        public List<NewCustomerStatisticsReqDTO> Params { get; set; } = new List<NewCustomerStatisticsReqDTO>();
    }

    public class NewCustomerArrivalShopSaleroomReqDTO
    {
        [Required(ErrorMessage = "请输入ShopId")]
        [Range(1, long.MaxValue, ErrorMessage = "ShopId必须大于0")]
        public long ShopId { get; set; }

        public List<NewCustomerStatisticsReqDTO> Params { get; set; } = new List<NewCustomerStatisticsReqDTO>();
    }

}
