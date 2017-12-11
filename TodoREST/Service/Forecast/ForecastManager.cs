using System.Threading.Tasks;
using Weather.Models;

namespace Weather
{
    public class ForecastManager
    {
        IForecastService forecastService;

        public ForecastManager(IForecastService service)
        {
            forecastService = service;
        }

        public async Task<Forecast> GetForecastAsync (string ZipCode)
        {
            return await forecastService.GetForecastAsync(ZipCode);
        }
    }
}
