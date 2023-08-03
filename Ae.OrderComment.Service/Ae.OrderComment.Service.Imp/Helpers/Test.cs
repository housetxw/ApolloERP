using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Ae.OrderComment.Service.Imp.Helpers
{
    public  class Test
    {
        public Dictionary<string, string> GetPropertyValue<T>(T obj)
        {
            if (obj != null)
            {
                Dictionary<string, string> propertyValue = new Dictionary<string, string>();
                Type type = obj.GetType();
                PropertyInfo[] propertyInfos = type.GetProperties();

                foreach (PropertyInfo item in propertyInfos)
                {
                    propertyValue.Add(item.Name, (item.GetValue(obj, null) == null ? "" : item.GetValue(obj, null)).ToString());
                }

                return propertyValue;
            }
            return null;
        }

        public string GetUri(string path, string query = null)
        {
            Uri uri = new UriBuilder()
            {
                Path = path,
                Query = (query ?? "")
            }.Uri;
            string url = uri.PathAndQuery.TrimStart('/');
            return url;
        }

        public string GetUrlByObject<T>(T obj)
        {
            Dictionary<string, string> dicRequest = GetPropertyValue(obj);
            StringBuilder queryStr = new StringBuilder();
            if (dicRequest != null)
            {
                foreach (var dic in dicRequest)
                {
                    if (!string.IsNullOrEmpty(dic.Value))
                    {
                        queryStr.Append($"{dic.Key}={dic.Value}&");
                    }
                }
            }
            string query = queryStr.ToString();
            string url = query == "" ? null : query.Substring(0, query.Length - 1);
            return url;
        }
    }
}
