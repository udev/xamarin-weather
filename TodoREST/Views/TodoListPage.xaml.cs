using System;
using System.Threading.Tasks;
using Weather.Models;
using Xamarin.Forms;

namespace Weather
{
    public partial class TodoListPage : ContentPage
	{
        //bool alertShown = false;

		public TodoListPage ()
		{
			InitializeComponent ();

            // https://forums.xamarin.com/discussion/comment/241948/#Comment_241948
            listView.ItemTapped += (sender, e) =>
            {
                var todoItem = e.Item as TodoItem;
                var todoPage = new TodoItemPage();
                todoPage.BindingContext = todoItem;
                Navigation.PushAsync(todoPage);
                var list = sender as ListView;
                list.SelectedItem = null;
            };
		}

		protected async override void OnAppearing ()
		{
			base.OnAppearing ();

            listView.ItemsSource = null;
			listView.ItemsSource = await App.TodoManager.GetTasksAsync ();

            searchBar.SearchCommand =
                         new Command(async () => {
                             var forecast = await App.ForecastManager.GetForecastAsync (searchBar.Text);
                             AddItem(forecast);
                         });
		}

		void OnAddItemClicked (object sender, EventArgs e)
		{
			var todoItem = new TodoItem () {
				ID = Guid.NewGuid ().ToString ()
			};
			var todoPage = new TodoItemPage (true);
			todoPage.BindingContext = todoItem;
			Navigation.PushAsync (todoPage);
		}

        private void AddItem (Forecast Forecast)
        {
            var todoItem = new TodoItem(Forecast)
            {
                ID = Guid.NewGuid().ToString()
            };
            var todoPage = new TodoItemPage(true);
            todoPage.BindingContext = todoItem;
            Navigation.PushAsync(todoPage); 
        }
	}
}
