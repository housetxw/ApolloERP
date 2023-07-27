using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Service.Core.Model
{
    public class EmployeeGroupDTO
    {
        /// <summary>
        /// 员工id
        /// </summary>
        public long Id { get; set; } = 0;
        /// <summary>
        /// 
        /// </summary>
        public string EmployeeId { get; set; } = string.Empty;
        /// <summary>
        /// 姓名（登录token中已用，不可修改）
        /// </summary>
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// 手机号
        /// </summary>
        public string Mobile { get; set; } = string.Empty;
        /// <summary>
        /// 
        /// </summary>
        public string GroupName { get; set; } = string.Empty;
        /// <summary>
        /// 
        /// </summary>
        public string GroupLeader { get; set; } = string.Empty;
        /// <summary>
        /// 门店Id
        /// </summary>
        public long ShopId { get; set; }
        /// <summary>
        /// 删除标识 0未删除 1已删除
        /// </summary>
        public sbyte IsDeleted { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public string CreateBy { get; set; } = string.Empty;
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; } = new DateTime(1900, 1, 1);
        /// <summary>
        /// 修改人
        /// </summary>
        public string UpdateBy { get; set; } = string.Empty;
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime UpdateTime { get; set; } = new DateTime(1900, 1, 1);
    }
}
