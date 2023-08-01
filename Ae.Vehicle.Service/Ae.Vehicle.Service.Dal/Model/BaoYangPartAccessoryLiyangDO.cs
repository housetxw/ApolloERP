using ApolloErp.Data.DapperExtensions.Att;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Vehicle.Service.Dal.Model
{
    [Table("bao_yang_part_accessory_liyang")]
    public class BaoYangPartAccessoryLiyangDO
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        [Column("id")]
        public int Id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("level_id")]
        public string LevelId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("volume")]
        public string Volume { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("grade")]
        public string Grade { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("level")]
        public string Level { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column("viscosity")]
        public string Viscosity { get; set; }
    }
}
