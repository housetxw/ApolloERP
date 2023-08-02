using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Product.Api.Core.Model
{
    public class UnitVo
    {
        /// <summary>
        /// 主键
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 单位名称
        /// </summary>
        public string UnitName { get; set; } = string.Empty;
        /// <summary>
        /// 标记删除
        /// </summary>
        public sbyte IsDeleted { get; set; }
   
    }
}
