using System;
namespace TodoREST.Models
{
    public class Currently
    {
        public long time { get; set; }
        public string summary { get; set; }
        public string icon { get; set; }
        public UInt32 nearestStormDistance { get; set; }
        public UInt32 nearestStormBearing { get; set; }
        public float precipIntensity { get; set; }
        public float precipProbability { get; set; }
        public float temperature { get; set; }
        public float apparentTemperature { get; set; }
        public float dewPoint { get; set; }
        public float humidity { get; set; }
        public float pressure { get; set; }
        public UInt32 windSpeed { get; set; }
        public UInt32 windGust { get; set; }
        public UInt32 windBearing { get; set; }
        public float cloudCover { get; set; }
        public float uvIndex { get; set; }
        public UInt32 visibility { get; set; }
        public float ozone { get; set; }
    }
}
