using ApolloErp.Data.DapperExtensions.Att;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Vehicle.Service.Dal.Model
{
    [Table("user_car_property")]
    public class UserCarPropertyDO
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        [Column("id")]
        public int Id { get; set; }
        /// <summary>
        /// 车id
        /// </summary>
        [Column("car_id")]
        public Guid CarId { get; set; }
        /// <summary>
        /// 五级属性名
        /// </summary>
        [Column("property_key")]
        public string PropertyKey { get; set; }
        /// <summary>
        /// 五级属性值
        /// </summary>
        [Column("property_value")]
        public string PropertyValue { get; set; }
        /// <summary>
        /// 标记删除
        /// </summary>
        [Column("is_deleted")]
        public bool IsDeleted { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        [Column("create_by")]
        public string CreateBy { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        [Column("create_time")]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 更新人
        /// </summary>
        [Column("update_by")]
        public string UpdateBy { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        [Column("update_time")]
        public DateTime UpdateTime { get; set; }
    }
}
