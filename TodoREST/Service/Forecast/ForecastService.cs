using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using Weather.Models;

namespace Weather
{
    public class ForecastService : IForecastService
    {
        public async Task<Forecast> GetForecastAsync (string ZipCode) =>
            await Task.Run(() =>
            {
                // Prevent cancellation token error with a using block
                // https://stackoverflow.com/a/38017863
                using (var client = PrepareClient())
                {
                    var location = GetLocation(ZipCode).Result;
                    // RestUrl = https://api.darksky.net/forecast/{ApiKey}/{Geolocation}
                    var url = Constants.BaseUrl + Constants.ApiKey + "/{0},{1}";
                    var uri = new Uri(string.Format(url, location.Latitude, location.Longitude));

                    try
                    {
                        var response = client.GetAsync(uri);
                        var content = response.Result.Content.ReadAsStringAsync().Result;
                        client.Dispose();
                        return JsonConvert.DeserializeObject<Forecast>(content);
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine(ex.Message);
                    }

                    // Definitely not ideal. In this case there was an error
                    // that we can't recover from and the correct course of
                    // action is to halt execution and prompt the user.
                    // In the interest of time I'm returning an empty object.
                    return new Forecast();
                }
            });

        // Location should definitely be retrieved from its own service.
        // Just converting zip code to latitude and longitude out of curiosity.
        // Also, I already have a darksky.net account so why not use it?
        // https://jamesmontemagno.github.io/GeolocatorPlugin/Geocoding.html
        private async Task<Position> GetLocation(string zipCode)
        {
            var locator = CrossGeolocator.Current;
            locator.DesiredAccuracy = 100;

            try
            {
                var addresses = await locator.GetPositionsForAddressAsync(zipCode, null);
                var address = addresses.FirstOrDefault();

                if (address == null)
                {
                    throw new ArgumentException(
                        string.Format("No location found for zip code: {0}", zipCode));
                }

                return address;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("GetLocation: " + ex.Message);
            }

            // Ideally, prompt the user with error or instruction.
            // Default to home sweet home for demonstrational purposes.
            return new Position(30.3074624, -98.0335911);
        }

        // Optional client configuration would typically be included as arguments
        // https://aspnetmonsters.com/2016/08/2016-08-27-httpclientwrong/
        private HttpClient PrepareClient()
        {
            HttpClient client = new HttpClient();
            client.MaxResponseContentBufferSize = 256000;
            return client;
        }
    }
}
