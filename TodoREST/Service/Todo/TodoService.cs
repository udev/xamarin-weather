using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Weather
{
	public class TodoService : ITodoService
	{
		HttpClient client;

		public List<TodoItem> Items { get; private set; }

		public TodoService ()
		{
            // This should be handled by RefreshDataAsync but we're creating
            // and storing items locally only
            Items = new List<TodoItem>();

			var authData = string.Format("{0}:{1}", Constants.Username, Constants.Password);
			var authHeaderValue = Convert.ToBase64String(Encoding.UTF8.GetBytes(authData));

            client = new HttpClient (new LoggingHandler(new HttpClientHandler()));
			client.MaxResponseContentBufferSize = 256000;
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authHeaderValue);
		}

		public async Task<List<TodoItem>> RefreshDataAsync ()
		{
            // Don't throw away locally stored items.
			//Items = new List<TodoItem> ();

			// RestUrl = http://developer.xamarin.com:8081/api/todoitems
			var uri = new Uri (string.Format (Constants.RestUrl, string.Empty));

			try {
				var response = await client.GetAsync (uri);
				if (response.IsSuccessStatusCode) {
					var content = await response.Content.ReadAsStringAsync ();
					//Items = JsonConvert.DeserializeObject <List<TodoItem>> (content);
				}
			} catch (Exception ex) {
                System.Diagnostics.Debug.WriteLine(@"ERROR {0}", ex.Message);
			}

			return Items;
		}

		public async Task SaveTodoItemAsync (TodoItem item, bool isNewItem = false)
        {
            // HACK: add items locally
            var i = Items.Find((obj) => (obj as TodoItem).ID == item.ID);
            if (i != null)
            {
                Items.Remove(item);
            }
            Items.Add(item);

			// RestUrl = http://developer.xamarin.com:8081/api/todoitems
			var uri = new Uri (string.Format (Constants.RestUrl, string.Empty));

			try {
				var json = JsonConvert.SerializeObject (item);
				var content = new StringContent (json, Encoding.UTF8, "application/json");

				HttpResponseMessage response = null;
				if (isNewItem) {
					response = await client.PostAsync (uri, content);
				} else {
					response = await client.PutAsync (uri, content);
				}
				
				if (response.IsSuccessStatusCode) {
                    System.Diagnostics.Debug.WriteLine(@"TodoItem successfully saved.");
				}
			} catch (Exception ex) {
                System.Diagnostics.Debug.WriteLine(@"ERROR {0}", ex.Message);
            }
		}

		public async Task DeleteTodoItemAsync (string id)
        {
            // HACK: remove items locally
            var item = Items.Find((obj) => (obj as TodoItem).ID == id);
            if (item != null)
            {
                Items.Remove(item);
            }

			// RestUrl = http://developer.xamarin.com:8081/api/todoitems/{0}
			var uri = new Uri (string.Format (Constants.RestUrl, id));

			try {
				var response = await client.DeleteAsync (uri);

				if (response.IsSuccessStatusCode) {
                    System.Diagnostics.Debug.WriteLine (@"TodoItem successfully deleted.");	
				}
			} catch (Exception ex) {
                System.Diagnostics.Debug.WriteLine (@"ERROR {0}", ex.Message);
			}
		}
	}
}
