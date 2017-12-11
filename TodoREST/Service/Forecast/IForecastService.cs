using System.Threading.Tasks;
using Weather.Models;

namespace Weather
{
    public interface IForecastService
    {
        Task<Forecast> GetForecastAsync(string ZipCode);
    }
}
