using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QQmusic.Api.Services;
using QQmusic.Infrastructure.Services;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace QQmusic.Api.Extensions
{
    public static class CacheExtensions
    {
        public static void AddCaches(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMemoryCache();

            if (bool.Parse(configuration["Caching:UseRedis"]))
            {
                //Use Redis
                services.AddSingleton(typeof(ICacheService), new RedisCacheService(new RedisCacheOptions
                {
                    Configuration = configuration["Caching:Redis_connectionString"],
                    InstanceName = configuration["Caching:Redis_instanceName"],
                    Database = int.Parse(configuration["Caching:Redis_dbNum"])
                }));
            }
            else
            {
                //Use MemoryCache
                services.AddSingleton<IMemoryCache>(factory =>
                {
                    var cache = new MemoryCache(new MemoryCacheOptions());
                    return cache;
                });
                services.AddSingleton<ICacheService, MemoryCacheService>();
            }
        }
    }
}
