using ISERV.Persistence.EF.Entities;
using ISERV.Persistence.EF.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ISERV.Persistence.EFMigration
{
    public static class Startup
    {
        public static IServiceProvider CreateServices()
        {
            var configurationBuilder = new ConfigurationBuilder()
                .AddJsonFile($"appsettings.json", optional: true, reloadOnChange: true)
                .Build();

            var databaseSettings = configurationBuilder.GetSection("Database").Get<DatabaseSettings>();

            var serviceProvider = new ServiceCollection()
                .AddSingleton(databaseSettings)
                .AddTransient<IMigration, ApplicationContext>()
                .BuildServiceProvider(false);

            return serviceProvider;
        }
    }
}