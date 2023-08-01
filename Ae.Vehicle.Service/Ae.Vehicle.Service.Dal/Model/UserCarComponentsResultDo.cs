using ApolloErp.Data.DapperExtensions.Att;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Vehicle.Service.Dal.Model
{
    /// <summary>
    /// 用户车辆配件检查状况
    /// </summary>
    [Table("user_car_components_result")]
    public class UserCarComponentsResultDo
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        [Column("id")]
        public long Id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("car_id")]
        public Guid CarId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("part_id")]
        public long PartId { get; set; }
        /// <summary>
        /// 结果 0 正常  1异常
        /// </summary>
        [Column("result_value")]
        public int ResultValue { get; set; }

        /// <summary>
        /// 检查Id
        /// </summary>
        [Column("check_id")]
        public long CheckId { get; set; }

        /// <summary>
        /// 分类Id
        /// </summary>
        [Column("category_id")]
        public int CategoryId { get; set; }

        /// <summary>
        /// 异常项Id ,分割
        /// </summary>
        [Column("property_id")]
        public string PropertyId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column("is_deleted")]
        public sbyte IsDeleted { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("create_by")]
        public string CreateBy { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("create_time")]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("update_by")]
        public string UpdateBy { get; set; } = string.Empty;
        /// <summary>
        /// 
        /// </summary>
        [Column("update_time")]
        public DateTime UpdateTime { get; set; } = new DateTime(1900, 1, 1);
    }
}
