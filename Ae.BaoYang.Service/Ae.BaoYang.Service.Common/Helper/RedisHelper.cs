using Newtonsoft.Json;
using ApolloErp.Redis;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.BaoYang.Service.Common.Helper
{
    public static class RedisHelper
    {
        public static async Task<T> GetOrSetAsync<T>(this RedisClient client, string key, Func<Task<T>> func,
            TimeSpan expiration)
        {
            //txw 2022-11-21 先不用缓存
            return await func();

            if (client.Redis.KeyExists(key))
            {
                string dataJson = await client.Redis.StringGetAsync(key);
                return JsonConvert.DeserializeObject<T>(dataJson);//反序列化
            }
            else
            {
                T obj = await func();
                await client.Redis.StringSetAsync(key, JsonConvert.SerializeObject(obj), expiration);
                return obj;
            }
        }
    }
}
