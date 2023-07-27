using System;
using System.Collections.Generic;
using System.Text;
using ApolloErp.Data.DapperExtensions.Att;

namespace Ae.BasicData.Service.Dal.Model
{
    [Table("app_version")]
    public class AppVersionDO
    {
        /// <summary>
        /// 版本号
        /// </summary>
        [Key]
        [Column("code")]
        public long Code { get; set; }

        /// <summary>
        /// 版本号名称
        /// </summary>
        [Column("name")]
        public string Name { get; set; }

        /// <summary>
        /// 版本信息
        /// </summary>
        [Column("info")]
        public string Info { get; set; }

        /// <summary>
        /// 版本URL
        /// </summary>
        [Column("url")]
        public string Url { get; set; }

        /// <summary>
        /// 版本配置
        /// </summary>
        [Column("config")]
        public string Config { get; set; }

        /// <summary>
        /// 版本时间
        /// </summary>
        [Column("time")]
        public DateTime Time { get; set; } = DateTime.Now;

        /// <summary>
        /// 标识此版本，是否让灰度门店升级
        /// </summary>
        [Column("is_force")]
        public bool IsForce { get; set; }

        /// <summary>
        /// 标识此版本，正式版是否发布
        /// </summary>
        [Column("is_release")]
        public bool IsRelease { get; set; }

        /// <summary>
        /// 是否删除
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
        public DateTime CreateTime { get; set; } = DateTime.Now;

        /// <summary>
        /// 修改人
        /// </summary>
        [Column("update_by")]
        public string UpdateBy { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        [Column("update_time")]
        public DateTime UpdateTime { get; set; } = DateTime.Now;
    }
}
