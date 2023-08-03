using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ae.C.MiniApp.Api.Common.Extension
{
    public static class ObjectExtensions
    {
        /// <summary>
        /// 获取属性值 只支持简单类型
        /// </summary> 
        /// <returns></returns>
        public static SortedDictionary<string, string> GetProperties(this object obj)
        {
            //将所有属性存入集合 以默认排序方式排序
            var queryParameters = new SortedDictionary<string, string>(StringComparer.Ordinal);

            foreach (var property in obj.GetType().GetProperties())
            {
                if (string.IsNullOrEmpty(property.Name))
                {
                    continue;
                }

                var propertyValue = property.GetValue(obj);

                var value = "";
                if (propertyValue is IEnumerable)
                {
                    var enumerator = ((IEnumerable)propertyValue).GetEnumerator();

                    var listValue = new List<string>();

                    while (enumerator.MoveNext())
                    {
                        if (enumerator.Current != null)
                        {
                            listValue.Add(enumerator.Current.ToString());
                        }
                    }
                    value = string.Join("|", listValue.OrderBy(m => m));
                }
                else
                {
                    value = propertyValue?.ToString().Trim() ?? "";
                }

                queryParameters.Add(property.Name.ToLower(), value);
            }

            return queryParameters;
        }
    }
}
