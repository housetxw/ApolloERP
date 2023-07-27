using System;
using System.Collections.Generic;
using System.Text;
using ApolloErp.Data.DapperExtensions.Att;
using MySqlX.XDevAPI.Relational;
 
namespace Ae.Shop.Service.Dal.Model
{
    [Table("business_daily")]
    public class BusinessDailyDO
    {
        /// <summary>
        /// 经营日报主键id
        /// </summary>
        [Key]
        [Column("id")]
        public long Id { get; set; } 
        /// <summary>
        /// 门店id
        /// </summary>
        [Column("shop_id")]
        public long ShopId { get; set; } 
        /// <summary>
        /// 今日收益
        /// </summary>
        [Column("today_earn")]
        public decimal TodayEarn { get; set; } 
        /// <summary>
        /// 本月收益
        /// </summary>
        [Column("month_earn")]
        public decimal MonthEarn { get; set; } 
        /// <summary>
        /// 今日到店客户数量
        /// </summary>
        [Column("customer_count")]
        public int CustomerCount { get; set; } 
        /// <summary>
        /// 新客户数量
        /// </summary>
        [Column("new_customer_count")]
        public int NewCustomerCount { get; set; } 
        /// <summary>
        /// 老客户数量
        /// </summary>
        [Column("old_customer_count")]
        public int OldCustomerCount { get; set; } 
        /// <summary>
        /// 平均收益
        /// </summary>
        [Column("average_earn")]
        public decimal AverageEarn { get; set; } 
        /// <summary>
        /// 新客户收益
        /// </summary>
        [Column("new_customer_earn")]
        public decimal NewCustomerEarn { get; set; } 
        /// <summary>
        /// 老客户收益
        /// </summary>
        [Column("old_customer_earn")]
        public decimal OldCustomerEarn { get; set; } 
        /// <summary>
        /// 门店评分
        /// </summary>
        [Column("shop_score")]
        public decimal ShopScore { get; set; } 
        /// <summary>
        /// 5星好评数
        /// </summary>
        [Column("good_score_count")]
        public int GoodScoreCount { get; set; } 
        /// <summary>
        /// 4星及以下评论数
        /// </summary>
        [Column("general_score_count")]
        public int GeneralScoreCount { get; set; } 
        /// <summary>
        /// 出勤人数
        /// </summary>
        [Column("attendance_count")]
        public int AttendanceCount { get; set; } 
        /// <summary>
        /// 车友人数
        /// </summary>
        [Column("rider_count")]
        public int RiderCount { get; set; } 
        /// <summary>
        /// 早会记录
        /// </summary>
        [Column("early_meeting")]
        public string EarlyMeeting { get; set; } = string.Empty;
        /// <summary>
        /// 是否删除 0否 1是
        /// </summary>
        [Column("is_deleted")]
        public sbyte IsDeleted { get; set; } 
        /// <summary>
        /// 创建人
        /// </summary>
        [Column("create_by")]
        public string CreateBy { get; set; } = string.Empty;
        /// <summary>
        /// 创建时间
        /// </summary>
        [Column("create_time")]
        public DateTime CreateTime { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 修改人
        /// </summary>
        [Column("update_by")]
        public string UpdateBy { get; set; } = string.Empty;
        /// <summary>
        /// 修改时间
        /// </summary>
        [Column("update_time")]
        public DateTime UpdateTime { get; set; } = new DateTime(1900, 1, 1);
    }
}