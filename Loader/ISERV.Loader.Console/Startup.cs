using ISERV.API.HipolabsAPI.Entities;
using ISERV.API.HipolabsAPI.Services;
using ISERV.Loader.Console.Entities;
using ISERV.Persistence.EF.Entities;
using ISERV.Persistence.EF.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ISERV.Loader.Console
{
    public static class Startup
    {
        public static IServiceProvider CreateServices()
        {
            var configurationBuilder = new ConfigurationBuilder()
                .AddJsonFile($"appsettings.json", optional: true, reloadOnChange: true)
                .Build();

            var connectionSettings = configurationBuilder.GetSection("HipolabsAPI").Get<HipolabsConnectionSettings>();
            var databaseSettings = configurationBuilder.GetSection("Database").Get<DatabaseSettings>();

            var loadingSettings = configurationBuilder.GetSection("LoadingSettings").Get<LoadingSettings>();

            var serviceProvider = new ServiceCollection()
                .AddSingleton(new HipolabsAPI(connectionSettings, loadingSettings.MaxThreads))
                .AddSingleton(databaseSettings)
                .AddTransient<ApplicationContext>()
                .AddTransient<UniversitiesRepository>()
                .BuildServiceProvider(false);

            return serviceProvider;
        }
    }
}
