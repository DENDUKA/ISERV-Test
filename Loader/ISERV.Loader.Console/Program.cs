using ISERV.API.HipolabsAPI.Entities;
using ISERV.API.HipolabsAPI.Services;
using ISERV.Persistence.EF.Services;
using Microsoft.Extensions.DependencyInjection;

namespace ISERV.Loader.Console
{
    internal class Program
    {
        private static IServiceProvider _serviceProvider;
        static void Main(string[] args)
        {
            _serviceProvider = Startup.CreateServices();

            var cancellationTokenSource = new CancellationTokenSource();

            var c =  _serviceProvider.GetRequiredService<ApplicationContext>();


            //SaveData(new List<UniversityDTO>());
             RunLoader(cancellationTokenSource.Token);

            System.Console.Read();

            cancellationTokenSource.Cancel();
        }

        private static async Task RunLoader(CancellationToken token)
        {
            var hipolabsAPI = _serviceProvider.GetRequiredService<HipolabsAPI>();

            var universities = await hipolabsAPI.GetForCountriesAsync(Resources.countriesList, token);

            System.Console.WriteLine("Загрузка завершена");

            await SaveData(universities);

            System.Console.WriteLine("Сохранение завершено");
        }

        private static async Task SaveData(List<UniversityDTO> universities)
        {
            UniversitiesRepository universityRepository = null;

            universityRepository = _serviceProvider.GetRequiredService<UniversitiesRepository>();

            await universityRepository.SaveAll(universities.Select(u => new Domain.Entities.University()
            {
                Country = u.country,
                Name = u.name,
                Sites = string.Join(';', u.web_pages)
            }).ToList()); //Automapper
        }
    }
}