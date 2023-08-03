using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.C.MiniApp.Api.Common.Extension
{
    public static class RedisExtension
    {
        ///// <summary>
        ///// 获取一个key的对象
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="cacheKey"></param>
        ///// <returns></returns>
        //public static T GetStringKey<T>(this IDatabase database, string cacheKey)
        //{
        //    var cacheStr = database.StringGet(cacheKey);
        //    if (string.IsNullOrEmpty(cacheStr))
        //    {
        //        return default(T);
        //    }
        //    return JsonConvert.DeserializeObject<T>(cacheStr);
        //}

        ///// <summary>
        ///// 保存一个对象
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="key"></param>
        ///// <param name="obj"></param>
        ///// <returns></returns>
        //public static bool SetStringKey<T>(this IDatabase database, string key, T obj, TimeSpan? expiry = default(TimeSpan?))
        //{
        //    if (string.IsNullOrEmpty(key))
        //    {
        //        return false;
        //    }
        //    string json = JsonConvert.SerializeObject(obj);
        //    return database.StringSet(key, json, expiry);
        //}
    }
}
