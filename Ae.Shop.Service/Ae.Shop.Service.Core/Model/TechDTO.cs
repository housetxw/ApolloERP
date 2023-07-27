using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Service.Core.Model
{
    public class TechDTO
    {
        /// <summary>
        /// 技师ID
        /// </summary>
        public string TechId { get; set; }
        /// <summary>
        /// 技师头像
        /// </summary>
        public string HeadImg { get; set; }
        /// <summary>
        /// 技师名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 技师等级
        /// </summary>
        public string TechLevel { get; set; }
    }
}
