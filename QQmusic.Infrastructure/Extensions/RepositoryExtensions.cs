using System;
using System.Collections.Generic;
using System.Text;
using QQmusic.Core.Interfaces;
using QQmusic.Infrastructure.Database;
using Microsoft.Extensions.DependencyInjection;
using QQmusic.Infrastructure.Repositories;

namespace QQmusic.Infrastructure.Extensions
{
    public static class RepositoryExtensions
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<SongRepository>();
            //services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}
