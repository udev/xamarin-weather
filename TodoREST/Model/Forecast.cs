using System;
using TodoREST.Models;

namespace Weather.Models
{
    public class Forecast
    {
        public Double latitude { get; set; }
        public Double longitude { get; set; }
        public String timezone { get; set; }
        public Currently currently { get; set; }
        // Stubbed out data. Could build a model for all of this but
        // it won't be used.
        //Minutely minutely: {},
        //Hourly hourly: {},
        //Daily daily: {},
        //Double flags: {},
        public Int32 offset { get; set; }
    }
}
