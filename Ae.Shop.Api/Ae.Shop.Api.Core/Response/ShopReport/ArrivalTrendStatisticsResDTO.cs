using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Ae.Shop.Api.Common.Format;

namespace Ae.Shop.Api.Core.Response
{
    public class ArrivalTrendStatisticsResDTO
    {
        public ArrivalTrendChartStatisticsResDTO ChartStatistics { get; set; } = new ArrivalTrendChartStatisticsResDTO();
        public ArrivalTrendTableStatisticsResDTO TableStatistics { get; set; } = new ArrivalTrendTableStatisticsResDTO();
    }
    public class ArrivalTrendChartStatisticsResDTO
    {
        //[JsonConverter(typeof(FmtDTToYearMonthDayLowerWithPeriod))]
        public List<string> Dates { get; set; } = new List<string>();
        public List<int> Amount { get; set; } = new List<int>();
        public List<int> Completed { get; set; } = new List<int>();
        public List<int> Leave { get; set; } = new List<int>();
        public List<int> UnCompleted { get; set; } = new List<int>();
    }
    public class ArrivalTrendTableStatisticsResDTO
    {
        public List<ArrivalTrendTableStatisticsEntityResDTO> Items = new List<ArrivalTrendTableStatisticsEntityResDTO>();
        //public int TotalItems;
    }
    public class ArrivalTrendTableStatisticsEntityResDTO
    {
        [JsonConverter(typeof(FmtDTToYearMonthDayLowerWithStrikethrough))]
        public DateTime ArrivalTime { get; set; }
        public int Amount { get; set; }
        public int Completed { get; set; }
        public int Leave { get; set; }
        public int UnCompleted { get; set; }
    }
    public class ArrivalTrendChartEntityDTO
    {
        public DateTime ArrivalTime { get; set; }
        public int Amount { get; set; }
    }

}
