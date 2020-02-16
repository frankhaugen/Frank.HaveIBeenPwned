using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HIBP.Toolkit
{
    public static class IServiceCollectionExtensions
    {
        /// <summary>
        /// Add this to register dependencies for this  library. Remember to add the "HibpConfiguration" section to your appsettings.json
        /// </summary>
        /// <param name="configuration">The IConfiguration object</param>
        public static void AddHibpToolkit(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<HibpConfiguration>(conf => configuration.GetSection(nameof(HibpConfiguration)));
            services.AddTransient<IHibpClient, HibpClient>();
            services.AddLogging();
            services.AddHttpClient();
        }
    }
}