using System;
using System.Collections.Generic;
using System.Text;
using QQmusic.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using QQmusic.Infrastructure.Resources;

namespace QQmusic.Infrastructure.Extensions
{
    public static class PropertyMappingExtensions
    {
        public static void AddPropertyMappings(this IServiceCollection services)
        {
            //注册排序的映射关系 QueryParameters.OrderBy
            var propertyMappingContainer = new PropertyMappingContainer();
            propertyMappingContainer.Register<SongPropertyMapping>();
            services.AddSingleton<IPropertyMappingContainer>(propertyMappingContainer);

            //注册塑形的映射关系 Fields
            services.AddTransient<ITypeHelperService, TypeHelperService>();
        }
    }
}
