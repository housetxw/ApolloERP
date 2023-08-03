using Newtonsoft.Json;
using ApolloErp.Redis;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ae.OrderComment.Service.Common.Utils
{
    public static class RedisHelper
    {
        public static readonly object LockPay = new object();

        public static async Task<T> GetOrSetAsync<T>(this RedisClient client, string key, Func<Task<T>> func,
            TimeSpan expiration)
        {
            if (client.Redis.KeyExists(key))
            {
                string dataJson = await client.Redis.StringGetAsync(key);
                return JsonConvert.DeserializeObject<T>(dataJson); //反序列化
            }
            else
            {
                T obj = await func();
                await client.Redis.StringSetAsync(key, JsonConvert.SerializeObject(obj), expiration);
                return obj;
            }
        }

        public static bool HitAsync(this RedisClient client, string key, TimeSpan expiration)
        {
            lock (LockPay)
            {
                if (client.Redis.KeyExists(key))
                {
                    return true;
                }
                else
                {
                    client.Redis.StringSet(key, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff"), expiration);
                    return false;
                }
            }
        }
    }
}
