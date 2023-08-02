using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Product.Api.Client.Model
{
    public class AttributevalueDto
    {
        /// <summary>
        /// 主键
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 属性名称ID
        /// </summary>
        public int AttributeNameId { get; set; }
        /// <summary>
        /// 属性值
        /// </summary>
        public string AttributeValue { get; set; }

        /// <summary>
        /// 是否 删除 0 否  1是  
        /// </summary>
        public sbyte IsDeleted { get; set; } = 0;
    }
}
