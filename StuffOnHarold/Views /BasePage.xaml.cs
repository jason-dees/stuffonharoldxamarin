using StuffOnHarold.ViewModels;
using Xamarin.Forms;

namespace StuffOnHarold.Views {
	
	public partial class BasePage : ContentPage {
		
		private readonly InitialViewModel _initialViewModel;

		public BasePage() {
			InitializeComponent();

			_initialViewModel = new InitialViewModel();

			NavigationPage.SetHasNavigationBar(this, false);
		}

		protected override void OnAppearing(){
			base.OnAppearing();

			_initialViewModel.FillInHaroldListCommand.Execute(null);
		}
	}
}
