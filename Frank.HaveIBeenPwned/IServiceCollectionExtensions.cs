using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Frank.HaveIBeenPwned
{
    public static class IServiceCollectionExtensions
    {
        /// <summary>
        /// Add this to register dependencies for this  library. Remember to add the "HaveIBeenPwnedConfiguration" section to your appsettings.json
        /// </summary>
        /// <param name="configuration">The IConfiguration object</param>
        public static void AddHibpToolkit(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<HaveIBeenPwnedConfiguration>(conf => configuration.GetSection(nameof(HaveIBeenPwnedConfiguration)));
            //services.AddTransient<IHaveIBeenPwnedClient, HaveIBeenPwnedClient>();
            services.AddLogging();
            services.AddHttpClient();
        }
    }
}