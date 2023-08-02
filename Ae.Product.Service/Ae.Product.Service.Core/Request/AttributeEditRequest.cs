using Ae.Product.Service.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Product.Service.Core.Request
{
    public class AttributeEditRequest
    {
        /// <summary>
        ///  属性名
        /// </summary>
        public AttributeNameVo  AttributeName { get; set; }

        /// <summary>
        /// 属性值
        /// </summary>
        public List<AttributevalueVo> AttributeValues { get; set; }

        /// <summary>
        /// 操作人
        /// </summary>
        /// <returns></returns>
        public string UserId { get; set; }
        /// <summary>
        /// 是否编辑
        /// </summary>
        public bool IsEdit { get; set; }
    }
}
