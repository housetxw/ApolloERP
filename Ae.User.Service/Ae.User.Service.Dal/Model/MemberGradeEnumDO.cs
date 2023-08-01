using ApolloErp.Data.DapperExtensions.Att;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.User.Service.Dal.Model
{
    [Table("member_grade_enum")]
    public class MemberGradeEnumDO
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        [Column("id")]
        public int Id { get; set; }
        /// <summary>
        /// 会员等级
        /// </summary>
        [Column("member_grade")]
        public int MemberGrade { get; set; }

        /// <summary>
        /// 会员等级图片
        /// </summary>
        [Column("member_url")]
        public string MemberUrl { get; set; }
        /// <summary>
        /// 等级显示
        /// </summary>
        [Column("description")]
        public string Description { get; set; }
        /// <summary>
        /// 成长开始值
        /// </summary>
        [Column("start_value")]
        public int StartValue { get; set; }
        /// <summary>
        /// 成长结束值
        /// </summary>
        [Column("end_value")]
        public int EndValue { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [Column("remark")]
        public string Remark { get; set; }
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
