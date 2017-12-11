using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Weather
{
	public class App : Application
	{
        public static TodoItemManager TodoManager { get; private set; }
        public static ForecastManager ForecastManager { get; private set; }

		public static ITextToSpeech Speech { get; set; }

		public App ()
		{
			TodoManager = new TodoItemManager (new TodoService ());
            ForecastManager = new ForecastManager(new ForecastService ());
			MainPage = new NavigationPage (new TodoListPage ());
		}

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}

