using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.DependencyInjection;

namespace QQmusic.Api.Extensions
{
    public static class MediaTypeExtensions
    {
        public static void AddMediaTypes(this IServiceCollection services)
        {
            services.Configure<MvcOptions>(options =>
            {
                var intputFormatter = options.OutputFormatters.OfType<JsonInputFormatter>().FirstOrDefault();
                if (intputFormatter != null)
                {
                    InputMediaTypes.ForEach(x => intputFormatter.SupportedMediaTypes.Add(x));
                }
                var outputFormatter = options.OutputFormatters.OfType<JsonOutputFormatter>().FirstOrDefault();
                if (outputFormatter != null)
                {
                    OutputMediaTypes.ForEach(x => outputFormatter.SupportedMediaTypes.Add(x));
                }
            });
        }

        private static readonly List<string> InputMediaTypes = new List<string>
        {
            "application/x-www-form-urlencoded",
            //"application/vnd.naoh.hateoas.create+json",
            //"application/vnd.naoh.hateoas.update+json"
        };

        private static readonly List<string> OutputMediaTypes = new List<string>
        {
            //"application/vnd.naoh.hateoas+json"
        };
    }
}
