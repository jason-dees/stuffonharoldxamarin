using StuffOnHarold.ViewModels;
using Xamarin.Forms;

namespace StuffOnHarold.Views {
	public partial class StuffPage : ContentPage {

		private readonly StuffViewModel _stuffViewModel;

		public StuffPage() {
			InitializeComponent();
			_stuffViewModel = new StuffViewModel();
			BindingContext = _stuffViewModel;

			NavigationPage.SetHasNavigationBar(this, false);
		}

		protected override void OnAppearing() {
			base.OnAppearing();

			_stuffViewModel.FillInStuffListCommand.Execute(null);
		}

		public void SetUpGrid(){


		}
	}
}
