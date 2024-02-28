using ISERV.API.HipolabsAPI.Entities;
using ISERV.API.HipolabsAPI.Exceptions;
using Newtonsoft.Json;
using RestSharp;

namespace ISERV.API.HipolabsAPI.Services
{
    public class HipolabsAPI
    {
        private readonly HipolabsConnectionSettings _hipolabsConnectionSettings;
        private readonly int _maxThreads;
        private readonly SemaphoreSlim semaphoreSlim;

        public HipolabsAPI(HipolabsConnectionSettings hipolabsConnectionSettings, int maxThreads = 1)
        {
            _hipolabsConnectionSettings = hipolabsConnectionSettings;
            _maxThreads = maxThreads;
            semaphoreSlim = new SemaphoreSlim(maxThreads);
        }

        public async Task<UniversityDTO[]> GetAllAsync(CancellationToken token)
        {
            var options = new RestClientOptions(_hipolabsConnectionSettings.URLSearch);

            var client = new RestClient(options);
            var request = new RestRequest();

            var response = await client.GetAsync(request, token);

            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<UniversityDTO[]>(response.Content);
            }

            throw new SiteUnavailableException(response.ResponseUri.AbsoluteUri, response.StatusDescription);
        }

        public async Task<List<UniversityDTO>> GetForCountriesAsync(string[] countryList, CancellationToken token)
        {
            var requests = countryList.Select(async cntry =>
            {
                return await Task.Run<UniversityDTO[]>(() =>
                {
                    return GetForCountry(cntry, token);
                });
            }).ToArray();

            await Task.WhenAll(requests);

            List<UniversityDTO> result = new();

            foreach (var t in requests)
            {
                if (t.IsCompletedSuccessfully)
                {
                    result.AddRange(t.Result);
                }
            }

            return result;
        }

        private async Task<UniversityDTO[]> GetForCountry(string countryName, CancellationToken token)
        {
            await semaphoreSlim.WaitAsync(token);

            Console.WriteLine($"{countryName} ENTER");

            var options = new RestClientOptions($"{_hipolabsConnectionSettings.URLSearchCountry}{countryName}");
            var client = new RestClient(options);
            var request = new RestRequest();

            var response = await client.GetAsync(request, token);

            UniversityDTO[] result;

            if (response.IsSuccessStatusCode)
            {
                result = JsonConvert.DeserializeObject<UniversityDTO[]>(response.Content);
            }
            else
            {
                throw new SiteUnavailableException(response.ResponseUri.AbsoluteUri, response.StatusDescription);
            }

            Console.WriteLine($"{countryName} EXIT");

            semaphoreSlim.Release(1);

            return result;
        }
    }
}