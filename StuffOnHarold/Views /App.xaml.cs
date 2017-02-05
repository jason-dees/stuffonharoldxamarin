using Xamarin.Forms;

namespace StuffOnHarold.Views
{
	public partial class App : Application
	{
		public App()
		{
			InitializeComponent();

			MainPage = new NavigationPage(new StuffOnHaroldPage(true));

		}

		protected override async void OnStart()
		{
			// Handle when your app starts
			await MainPage.Navigation.PushAsync(new StuffOnHaroldPage(false));
		}

		protected override void OnSleep()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume()
		{
			// Handle when your app resumes
		}
	}
}
