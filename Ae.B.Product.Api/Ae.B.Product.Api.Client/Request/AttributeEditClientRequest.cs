using Ae.B.Product.Api.Client.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Product.Api.Client.Request
{
    public class AttributeEditClientRequest
    {
        /// <summary>
        ///  属性名
        /// </summary>
        public AttributeNameDto AttributeName { get; set; }

        /// <summary>
        /// 属性值
        /// </summary>
        public List<AttributevalueDto> AttributeValues { get; set; }

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
