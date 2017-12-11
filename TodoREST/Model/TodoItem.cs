using System;
using Weather.Models;

namespace Weather
{
	public class TodoItem
	{
        public Forecast Forecast { get; set; }
        
		public string ID { get; set; }

		public string Name { get; set; }

		public string Notes { get; set; }

		public bool Done { get; set; }

        public TodoItem(Forecast Forecast)
        {
            this.Forecast = Forecast;
        }

        public TodoItem()
        {
        }
    }
}
