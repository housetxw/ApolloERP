using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Service.Dal.Model
{
    public class ShopEmployeeDO
    {
        /// <summary>
        /// ID
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// 手机号
        /// </summary>
        public string Mobile { get; set; }
        /// <summary>
        /// 员工号
        /// </summary>
        public string Number { get; set; }
        /// <summary>
        /// 职位
        /// </summary>
        public string JobName { get; set; }
        /// <summary>
        /// 照片
        /// </summary>
        public string Avatar { get; set; }
        /// <summary>
        /// 评分
        /// </summary>
        public decimal Score { get; set; }

        public string Description { get; set; }

        public int Level { get; set; }

        public string WorkName { get; set; }
    }
}
