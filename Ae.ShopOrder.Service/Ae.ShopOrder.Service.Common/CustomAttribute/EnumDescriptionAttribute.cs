using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.ShopOrder.Service.Common.CustomAttribute
{
    /// <summary>
    /// 自定义枚举属性
    /// </summary>
    public class EnumDescriptionAttribute : Attribute
    {
        public string Name { get; }

        public EnumDescriptionAttribute(string name)
        {
            this.Name = name;
        }
    }
}
