using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace QQmusic.Infrastructure.Services
{
    public class RedisCacheService : ICacheService
    {
        private readonly IDatabase _cache;
        private readonly string _instance;

        public RedisCacheService(RedisCacheOptions options)
        {
            var connection = ConnectionMultiplexer.Connect(options.Configuration);
            _cache = connection.GetDatabase(options.Database);
            _instance = options.InstanceName;
        }

        /// <summary>
        /// 验证缓存项是否存在
        /// </summary>
        /// <param name="key">缓存Key</param>
        /// <returns></returns>
        public bool Exists(string key)
        {
            return _cache.KeyExists(GetKeyForRedis(key));
        }

        /// <summary>
        /// 验证缓存项是否存在
        /// </summary>
        /// <param name="key">缓存Key</param>
        /// <returns></returns>
        public async Task<bool> ExistsAsync(string key)
        {
            return await _cache.KeyExistsAsync(GetKeyForRedis(key));
        }

        /// <summary>
        /// 添加缓存
        /// </summary>
        /// <param name="key">缓存Key</param>
        /// <param name="value">缓存Value</param>
        /// <returns></returns>
        public bool Add(string key, object value)
        {
            return _cache.StringSet(GetKeyForRedis(key), ConvertJson(value));
        }

        /// <summary>
        /// 添加缓存
        /// </summary>
        /// <param name="key">缓存Key</param>
        /// <param name="value">缓存Value</param>
        /// <returns></returns>
        public async Task<bool> AddAsync(string key, object value)
        {
            return await _cache.StringSetAsync(GetKeyForRedis(key), ConvertJson(value));
        }

        /// <summary>
        /// 添加缓存
        /// </summary>
        /// <param name="key">缓存Key</param>
        /// <param name="value">缓存Value</param>
        /// <param name="expiresSliding">滑动过期时长（如果在过期时间内有操作，则以当前时间点延长过期时间，Redis中无效）</param>
        /// <param name="expiresAbsolute">绝对过期时长</param>
        /// <returns></returns>
        public bool Add(string key, object value, TimeSpan expiresSliding, TimeSpan expiresAbsolute)
        {
            return Add(key, value, expiresAbsolute);
        }

        /// <summary>
        /// 添加缓存
        /// </summary>
        /// <param name="key">缓存Key</param>
        /// <param name="value">缓存Value</param>
        /// <param name="expiresSliding">滑动过期时长（如果在过期时间内有操作，则以当前时间点延长过期时间，Redis中无效）</param>
        /// <param name="expiresAbsolute">绝对过期时长</param>
        /// <returns></returns>
        public async Task<bool> AddAsync(string key, object value, TimeSpan expiresSliding, TimeSpan expiresAbsolute)
        {
            return await AddAsync(key, value, expiresAbsolute);
        }

        /// <summary>
        /// 添加缓存
        /// </summary>
        /// <param name="key">缓存Key</param>
        /// <param name="value">缓存Value</param>
        /// <param name="expiresIn">缓存时长</param>
        /// <param name="isSliding">是否滑动过期（如果在过期时间内有操作，则以当前时间点延长过期时间，Redis中无效）</param>
        /// <returns></returns>
        public bool Add(string key, object value, TimeSpan expiresIn, bool isSliding = false)
        {
            return _cache.StringSet(GetKeyForRedis(key), ConvertJson(value), expiresIn);
        }

        /// <summary>
        /// 添加缓存
        /// </summary>
        /// <param name="key">缓存Key</param>
        /// <param name="value">缓存Value</param>
        /// <param name="expiresIn">缓存时长</param>
        /// <param name="isSliding">是否滑动过期（如果在过期时间内有操作，则以当前时间点延长过期时间，Redis中无效）</param>
        /// <returns></returns>
        public async Task<bool> AddAsync(string key, object value, TimeSpan expiresIn, bool isSliding = false)
        {
            return await _cache.StringSetAsync(GetKeyForRedis(key), ConvertJson(value), expiresIn);
        }

        /// <summary>
        /// 删除缓存
        /// </summary>
        /// <param name="key">缓存Key</param>
        /// <returns></returns>
        public bool Remove(string key)
        {
            return _cache.KeyDelete(GetKeyForRedis(key));
        }

        /// <summary>
        /// 删除缓存
        /// </summary>
        /// <param name="key">缓存Key</param>
        /// <returns></returns>
        public async Task<bool> RemoveAsync(string key)
        {
            return await _cache.KeyDeleteAsync(GetKeyForRedis(key));
        }

        /// <summary>
        /// 批量删除缓存
        /// </summary>
        /// <param name="keys">缓存Key集合</param>
        /// <returns></returns>
        public void RemoveAll(IEnumerable<string> keys)
        {
            keys.ToList().ForEach(item => Remove(item));
        }

        /// <summary>
        /// 批量删除缓存
        /// </summary>
        /// <param name="keys">缓存Key集合</param>
        /// <returns></returns>
        public async Task RemoveAllAsync(IEnumerable<string> keys)
        {
            await keys.ToAsyncEnumerable().ForEachAsync(async item => await RemoveAsync(item));
        }

        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <param name="key">缓存Key</param>
        /// <returns></returns>
        public T Get<T>(string key) where T : class
        {
            return ConvertObj<T>(_cache.StringGet(GetKeyForRedis(key)));
        }

        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <param name="key">缓存Key</param>
        /// <returns></returns>
        public async Task<T> GetAsync<T>(string key) where T : class
        {
            return ConvertObj<T>(await _cache.StringGetAsync(GetKeyForRedis(key)));
        }

        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <param name="key">缓存Key</param>
        /// <returns></returns>
        public object Get(string key)
        {
            return _cache.StringGet(GetKeyForRedis(key));
        }

        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <param name="key">缓存Key</param>
        /// <returns></returns>
        public async Task<object> GetAsync(string key)
        {
            return await _cache.StringGetAsync(GetKeyForRedis(key));
        }

        /// <summary>
        /// 获取缓存集合
        /// </summary>
        /// <param name="keys">缓存Key集合</param>
        /// <returns></returns>
        public IDictionary<string, object> GetAll(IEnumerable<string> keys)
        {
            var dict = new Dictionary<string, object>();
            keys.ToList().ForEach(item => dict.Add(item, Get(GetKeyForRedis(item))));
            return dict;
        }

        /// <summary>
        /// 获取缓存集合
        /// </summary>
        /// <param name="keys">缓存Key集合</param>
        /// <returns></returns>
        public async Task<IDictionary<string, object>> GetAllAsync(IEnumerable<string> keys)
        {
            var dict = new Dictionary<string, object>();
            await keys.ToAsyncEnumerable().ForEachAsync(async item => dict.Add(item, await GetAsync(GetKeyForRedis(item))));
            return dict;
        }

        /// <summary>
        /// 修改缓存
        /// </summary>
        /// <param name="key">缓存Key</param>
        /// <param name="value">新的缓存Value</param>
        /// <returns></returns>
        public bool Replace(string key, object value)
        {
            if (Exists(key) && !Remove(key))
            {
                return false;
            }

            return Add(key, value);
        }

        /// <summary>
        /// 修改缓存
        /// </summary>
        /// <param name="key">缓存Key</param>
        /// <param name="value">新的缓存Value</param>
        /// <returns></returns>
        public async Task<bool> ReplaceAsync(string key, object value)
        {
            if (Exists(key) && !Remove(key))
            {
                return false;
            }

            return await AddAsync(key, value);
        }

        /// <summary>
        /// 修改缓存
        /// </summary>
        /// <param name="key">缓存Key</param>
        /// <param name="value">新的缓存Value</param>
        /// <param name="expiresSliding">滑动过期时长（如果在过期时间内有操作，则以当前时间点延长过期时间，Redis中无效）</param>
        /// <param name="expiresAbsolute">绝对过期时长</param>
        /// <returns></returns>
        public bool Replace(string key, object value, TimeSpan expiresSliding, TimeSpan expiresAbsolute)
        {
            if (Exists(key) && !Remove(key))
            {
                return false;
            }

            return Add(key, value, expiresSliding, expiresAbsolute);
        }

        /// <summary>
        /// 修改缓存
        /// </summary>
        /// <param name="key">缓存Key</param>
        /// <param name="value">新的缓存Value</param>
        /// <param name="expiresSliding">滑动过期时长（如果在过期时间内有操作，则以当前时间点延长过期时间，Redis中无效）</param>
        /// <param name="expiresAbsolute">绝对过期时长</param>
        /// <returns></returns>
        public async Task<bool> ReplaceAsync(string key, object value, TimeSpan expiresSliding, TimeSpan expiresAbsolute)
        {
            if (Exists(key) && !Remove(key))
            {
                return false;
            }

            return await AddAsync(key, value, expiresSliding, expiresAbsolute);
        }

        /// <summary>
        /// 修改缓存
        /// </summary>
        /// <param name="key">缓存Key</param>
        /// <param name="value">新的缓存Value</param>
        /// <param name="expiresIn">缓存时长</param>
        /// <param name="isSliding">是否滑动过期（如果在过期时间内有操作，则以当前时间点延长过期时间，Redis中无效）</param>
        /// <returns></returns>
        public bool Replace(string key, object value, TimeSpan expiresIn, bool isSliding = false)
        {
            if (Exists(key) && !Remove(key))
            {
                return false;
            }

            return Add(key, value, expiresIn, isSliding);
        }

        /// <summary>
        /// 修改缓存
        /// </summary>
        /// <param name="key">缓存Key</param>
        /// <param name="value">新的缓存Value</param>
        /// <param name="expiresIn">缓存时长</param>
        /// <param name="isSliding">是否滑动过期（如果在过期时间内有操作，则以当前时间点延长过期时间，Redis中无效）</param>
        /// <returns></returns>
        public async Task<bool> ReplaceAsync(string key, object value, TimeSpan expiresIn, bool isSliding = false)
        {
            if (Exists(key) && !Remove(key))
            {
                return false;
            }

            return await AddAsync(key, value, expiresIn, isSliding);
        }

        /// <summary>
        /// 组合自定义键
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private string GetKeyForRedis(string key)
        {
            return _instance + key;
        }

        /// <summary>
        /// 序列化
        /// </summary>
        /// <typeparam name="T">类</typeparam>
        /// <param name="value">值</param>
        /// <returns></returns>
        private string ConvertJson<T>(T value)
        {
            string result = value is string ? value.ToString() : JsonConvert.SerializeObject(value);
            return result;
        }

        /// <summary>
        /// 反序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        private T ConvertObj<T>(RedisValue value)
        {
            return JsonConvert.DeserializeObject<T>(value);
        }
    }

    /// <summary>
    /// Redis缓存的配置项
    /// </summary>
    public class RedisCacheOptions
    {
        public string Configuration { get; set; }

        public string InstanceName { get; set; }

        public int Database { get; set; } = 0;
    }
}
