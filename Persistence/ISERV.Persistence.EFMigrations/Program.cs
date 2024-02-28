using ISERV.Persistence.EF.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace ISERV.Persistence.EFMigration
{
    internal class Program
    {
        private static IServiceProvider _serviceProvider;

        static void Main(string[] args)
        {
            _serviceProvider = Startup.CreateServices();

            var migration = _serviceProvider.GetRequiredService<IMigration>();

            migration.ReCreateDatabase();

            Console.ReadLine();
        }
    }
}
