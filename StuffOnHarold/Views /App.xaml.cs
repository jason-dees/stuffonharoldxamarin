using StuffOnHarold.ViewModels;
using Xamarin.Forms;

namespace StuffOnHarold.Views
{
	public partial class App : Application
	{
		private readonly BasePage _basePage;

		public App()
		{
			InitializeComponent();

			_basePage = new BasePage();

			MainPage = new NavigationPage(_basePage);

		}

		protected override void OnStart()
		{
			// Handle when your app starts

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
